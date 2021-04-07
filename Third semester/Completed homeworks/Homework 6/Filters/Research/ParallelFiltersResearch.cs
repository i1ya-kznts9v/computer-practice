using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Filters;

namespace Research
{
    class ParallelFiltersResearch
    {
        string filtredImages;
        Bitmap toyotaSupra;

        ParallelFilters filters;
        Converter converter;
        Stopwatch stopwatch;

        Color<Byte>[,] image;
        Bitmap imageBitmap;

        public ParallelFiltersResearch()
        {
            filtredImages = $"{AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", String.Empty)}\\FiltredImages";
            toyotaSupra = Images.ToyotaSupra;

            Directory.CreateDirectory(filtredImages);

            filters = new ParallelFilters();
            converter = new Converter();
            stopwatch = new Stopwatch();

            image = converter.ToTwoDimArray(toyotaSupra);
        }

        public void WorkTime()
        {
            Console.WriteLine("Comparison of different filter versions by the number of threads started\n");
            Console.WriteLine("------------------------------------------------------------------------");

            Grey();
            Median3x3();
            Average3x3();
            Gauss3x3();
            SobelX();
            SobelY();
        }

        void Grey()
        {
            stopwatch.Start();
            image = filters.Grey(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nGrey work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Grey.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void Median3x3()
        {
            stopwatch.Start();
            image = filters.Median3x3(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nMedian3x3 work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Median3x3.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void Average3x3()
        {
            stopwatch.Start();
            image = filters.Average3x3(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nAverage3x3 work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Average3x3.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void Gauss3x3()
        {
            stopwatch.Start();
            image = filters.Gauss3x3(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nGauss3x3 work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Gauss3x3.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void SobelX()
        {
            stopwatch.Start();
            image = filters.SobelAny(image, "SobelX");
            stopwatch.Stop();
            Console.WriteLine($"\n\nSobelX work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\SobelX.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void SobelY()
        {
            stopwatch.Start();
            image = filters.SobelAny(image, "SobelY");
            stopwatch.Stop();
            Console.WriteLine($"\n\nSobelY work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\SobelY.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }
    }
}