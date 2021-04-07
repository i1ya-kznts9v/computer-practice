using System.Drawing;

namespace Filters
{
    public class Converter
    {
        public Color<byte>[,] ToTwoDimArray(Bitmap unconvertedImage)
        {
            if (unconvertedImage == null)
            {
                return null;
            }

            Color<byte>[,] convertedImage = new Color<byte>[unconvertedImage.Height, unconvertedImage.Width];

            for(int i = 0; i < unconvertedImage.Height; i++)
            {
                for(int j = 0; j < unconvertedImage.Width; j++)
                {
                    convertedImage[i, j] = ToRGB(unconvertedImage.GetPixel(j, i));
                }
            }

            return convertedImage;
        }

        Color<byte> ToRGB(Color argb)
        {
            Color<byte> rgb = new Color<byte>();

            rgb.Red = argb.R;
            rgb.Green = argb.G;
            rgb.Blue = argb.B;

            return rgb;
        }

        public Bitmap ToBitmap(Color<byte>[,] unconvertedImage)
        {
            if(unconvertedImage == null)
            {
                return null;
            }

            int height = unconvertedImage.GetLength(0);
            int width = unconvertedImage.GetLength(1);
            Bitmap convertedImage = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Color temp = ToARGB(unconvertedImage, i, j);

                    convertedImage.SetPixel(j, i, temp);
                }
            }

            return convertedImage;
        }

        Color ToARGB(Color<byte>[,] rgb, int i, int j)
        {
            return Color.FromArgb(rgb[i, j].Red, rgb[i, j].Green, rgb[i, j].Blue);
        }
    }
}