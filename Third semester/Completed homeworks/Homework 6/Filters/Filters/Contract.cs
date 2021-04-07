using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Filters
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class Contract : IContract
    {
        public void ApplyFilter(string filterType, byte[] bytes)
        {
            if(!GetAvailableFilters().Contains(filterType))
            {
                Console.WriteLine("Cannot apply an unavailable filter");

                return;
            }

            Color<byte>[,] image;
            Converter converter = new Converter();
            Filters filters = new Filters();

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                image = converter.ToTwoDimArray((Bitmap)Image.FromStream(memoryStream));
            }
            bytes = null;

            if(image == null)
            {
                Console.WriteLine("Filter application was interrupted due to invalid image");

                return;
            }
            
            switch (filterType)
            {
                case "Grey":
                    {
                        image = filters.Grey(image);

                        break;
                    }
                case "Median3x3":
                    {
                        image = filters.Median3x3(image);

                        break;
                    }
                case "Average3x3":
                    {
                        image = filters.Average3x3(image);

                        break;
                    }
                case "Gauss3x3":
                    {
                        image = filters.Gauss3x3(image);
                        
                        break;
                    }
                case "SobelX":
                    {
                        image = filters.SobelAny(image, filterType);

                        break;
                    }
                case "SobelY":
                    {
                        image = filters.SobelAny(image, filterType);

                        break;
                    }
                default:
                    {
                        Console.WriteLine("No such filter is implemented");

                        return;
                    }
            }

            if(image == null)
            {
                return;
            }
            
            using(MemoryStream memoryStream = new MemoryStream())
            {
                converter.ToBitmap(image).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);

                bytes = memoryStream.GetBuffer();
            }

            ImageCallback(bytes);
        }

        void ImageCallback(byte[] bytes)
        {
            try
            {
                OperationContext.Current.GetCallbackChannel<ICallbackContract>().ImageCallback(bytes);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public string[] GetAvailableFilters()
        {
            return ConfigurationManager.AppSettings.AllKeys;
        }
    }
}