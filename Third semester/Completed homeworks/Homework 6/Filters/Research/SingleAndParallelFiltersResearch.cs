using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Filters;

namespace Research
{
    class SingleAndParallelFiltersResearch
    {
        string filtredImages;
        Bitmap toyotaSupra;

        SingleAndParallelFilters filters;
        Converter converter;
        Stopwatch stopwatch;

        Color<Byte>[,] image;
        Bitmap imageBitmap;

        public SingleAndParallelFiltersResearch()
        {
            filtredImages = $"{AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\", String.Empty)}\\FiltredImages";
            toyotaSupra = Images.ToyotaSupra;

            Directory.CreateDirectory(filtredImages);

            filters = new SingleAndParallelFilters();
            converter = new Converter();
            stopwatch = new Stopwatch();

            image = converter.ToTwoDimArray(toyotaSupra);
        }

        public void WorkTime()
        {
            Console.WriteLine("Comparison of different filter versions by the number of threads started\n");
            Console.WriteLine("------------------------------------------------------------------------");

            Median3x3Single();
            Median3x3ParallelCancelleration();
            Median3x3ParallelLoopState();
        }

        void Median3x3Single()
        {
            stopwatch.Start();
            image = filters.Median3x3Single(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nMedian3x3Single work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Median3x3Single.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void Median3x3ParallelCancelleration()
        {
            stopwatch.Start();
            image = filters.Median3x3ParallelCancel(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nMedian3x3ParallelCancelleration work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Median3x3ParallelCancelleration.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }

        void Median3x3ParallelLoopState()
        {
            stopwatch.Start();
            image = filters.Median3x3ParallelLoopState(image);
            stopwatch.Stop();
            Console.WriteLine($"\n\nMedian3x3ParallelLoopState work time: {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("------------------------------------------------------------------------");
            imageBitmap = converter.ToBitmap(image);
            if (imageBitmap != null)
            {
                imageBitmap.Save($"{filtredImages}\\Median3x3ParallelLoopState.bmp");
            }
            image = converter.ToTwoDimArray(toyotaSupra);
            stopwatch.Reset();
        }
    }
}