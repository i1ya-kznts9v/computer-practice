using System;
using System.Threading;
using System.Threading.Tasks;
using Filters;

namespace Research
{
    class ParallelFilters
    {
        public Color<byte>[,] Grey(Color<byte>[,] imageUnfiltred)
        {
            string name = "Grey";
            int progress = 0;

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            ParallelLoopResult loopResult = Parallel.For
            (0,
            height,
            (i, loopState) =>
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                    for (int j = 0; j < width; j++)
                    {
                        if (loopState.LowestBreakIteration.HasValue)
                        {
                            break;
                        }

                        int newColor = Convolution(imageUnfiltred, i, j);

                        imageFiltred[i, j].Red = (byte)newColor;
                        imageFiltred[i, j].Green = (byte)newColor;
                        imageFiltred[i, j].Blue = (byte)newColor;
                    }

                    Interlocked.Increment(ref progress);

                    if (!ProgressCallback(progress, height))
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                            "canceling");

                        loopState.Break();
                    }
                }
            );

            if (loopResult.IsCompleted)
            {
                Console.WriteLine($"{name} is succesfully applicated");
            }
            else
            {
                Console.WriteLine($"{name} application has been canceled");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        int Convolution(Color<byte>[,] imageUnfiltred, int i, int j)
        {
            return ((imageUnfiltred[i, j].Red + imageUnfiltred[i, j].Green + imageUnfiltred[i, j].Blue) / 3);
        }

        public Color<byte>[,] Median3x3(Color<byte>[,] imageUnfiltred)
        {
            string name = "Median3x3";
            int progress = 2;

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            ParallelLoopResult loopResult = Parallel.For
            (1,
            height - 1,
            (i, loopState) =>
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                    for (int j = 1; j < width - 1; j++)
                    {
                        if (loopState.LowestBreakIteration.HasValue)
                        {
                            break;
                        }

                        Color<int[]> newColors = new Color<int[]>
                        {
                            Red = new int[9],
                            Green = new int[9],
                            Blue = new int[9]
                        };

                        newColors = Convolution(imageUnfiltred, i, j, newColors);

                        Array.Sort(newColors.Red);
                        Array.Sort(newColors.Green);
                        Array.Sort(newColors.Blue);

                        imageFiltred[i, j].Red = (byte)(newColors.Red[4]);
                        imageFiltred[i, j].Green = (byte)(newColors.Green[4]);
                        imageFiltred[i, j].Blue = (byte)(newColors.Blue[4]);
                    }

                    Interlocked.Increment(ref progress);

                    if (!ProgressCallback(progress, height))
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                            "canceling");

                        loopState.Break();
                    }
                }
            );

            if (loopResult.IsCompleted)
            {
                Console.WriteLine($"{name} is succesfully applicated");
            }
            else
            {
                Console.WriteLine($"{name} application has been canceled");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        Color<int[]> Convolution(Color<byte>[,] imageUnfiltred, int i, int j, Color<int[]> newColors)
        {
            int l = 0;

            for (int k = -1; k < 2; k++)
            {
                for (int m = -1; m < 2; m++)
                {
                    newColors.Red[l] = imageUnfiltred[i + k, j + m].Red;
                    newColors.Green[l] = imageUnfiltred[i + k, j + m].Green;
                    newColors.Blue[l] = imageUnfiltred[i + k, j + m].Blue;
                    l++;
                }
            }

            return (newColors);
        }

        public Color<byte>[,] Average3x3(Color<byte>[,] imageUnfiltred)
        {
            string name = "Average3x3";
            int progress = 2;

            int[,] matrix = { { 1, 1, 1 },
                              { 1, 1, 1 },
                              { 1, 1, 1 } };

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            ParallelLoopResult loopResult = Parallel.For
            (1,
            height - 1,
            (i, loopState) =>
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                    for (int j = 1; j < width - 1; j++)
                    {
                        if (loopState.LowestBreakIteration.HasValue)
                        {
                            break;
                        }

                        Color<int> newColor = new Color<int>();
                        newColor = Convolution(imageUnfiltred, i, j, newColor, matrix);

                        imageFiltred[i, j].Red = (byte)(newColor.Red / 9);
                        imageFiltred[i, j].Green = (byte)(newColor.Green / 9);
                        imageFiltred[i, j].Blue = (byte)(newColor.Blue / 9);
                    }

                    Interlocked.Increment(ref progress);

                    if (!ProgressCallback(progress, height))
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                            "canceling");

                        loopState.Break();
                    }
                }
            );

            if (loopResult.IsCompleted)
            {
                Console.WriteLine($"{name} is succesfully applicated");
            }
            else
            {
                Console.WriteLine($"{name} application has been canceled");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        public Color<byte>[,] Gauss3x3(Color<byte>[,] imageUnfiltred)
        {
            string name = "Gauss3x3";
            int progress = 2;

            int[,] matrix = { { 1, 2, 1 },
                              { 2, 4, 2 },
                              { 1, 2, 1 } };

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            ParallelLoopResult loopResult = Parallel.For
            (1,
            height - 1,
            (i, loopState) =>
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                    for (int j = 1; j < width - 1; j++)
                    {
                        if (loopState.LowestBreakIteration.HasValue)
                        {
                            break;
                        }

                        Color<int> newColor = new Color<int>();
                        newColor = Convolution(imageUnfiltred, i, j, newColor, matrix);

                        imageFiltred[i, j].Red = (byte)(newColor.Red / 16);
                        imageFiltred[i, j].Green = (byte)(newColor.Green / 16);
                        imageFiltred[i, j].Blue = (byte)(newColor.Blue / 16);
                    }

                    Interlocked.Increment(ref progress);

                    if (!ProgressCallback(progress, height))
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                            "canceling");

                        loopState.Break();
                    }
                }
            );

            if (loopResult.IsCompleted)
            {
                Console.WriteLine($"{name} is succesfully applicated");
            }
            else
            {
                Console.WriteLine($"{name} application has been canceled");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        public Color<byte>[,] SobelAny(Color<byte>[,] imageUnfiltred, string sobelType)
        {
            string name = sobelType;
            int progress = 2;

            int[,] matrix = new int[3, 3];

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            switch (sobelType)
            {
                case "SobelX":
                    {
                        matrix[0, 0] = -1; //{-1, -2, -1}
                        matrix[0, 1] = -2; //{ 0,  0,  0}
                        matrix[0, 2] = -1; //{ 1,  2,  1}
                        matrix[1, 0] = 0;
                        matrix[1, 1] = 0;
                        matrix[1, 2] = 0;
                        matrix[2, 0] = 1;
                        matrix[2, 1] = 2;
                        matrix[2, 2] = 1;

                        break;
                    }
                case "SobelY":
                    {
                        matrix[0, 0] = -1; //{-1, 0, 1}
                        matrix[0, 1] = 0;  //{-2, 0, 2}
                        matrix[0, 2] = 1;  //{-1, 0, 1}
                        matrix[1, 0] = -2;
                        matrix[1, 1] = 0;
                        matrix[1, 2] = 2;
                        matrix[2, 0] = -1;
                        matrix[2, 1] = 0;
                        matrix[2, 2] = 1;

                        break;
                    }
            }

            ParallelLoopResult loopResult = Parallel.For
            (1,
            height - 1,
            (i, loopState) =>
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                    for (int j = 1; j < width - 1; j++)
                    {
                        if (loopState.LowestBreakIteration.HasValue)
                        {
                            break;
                        }

                        Color<int> newColor = new Color<int>();
                        newColor = Convolution(imageUnfiltred, i, j, newColor, matrix);

                        if (newColor.Red < 0)
                        {
                            newColor.Red = 0;
                        }
                        else if (newColor.Red > 255)
                        {
                            newColor.Red = 255;
                        }

                        if (newColor.Green < 0)
                        {
                            newColor.Green = 0;
                        }
                        else if (newColor.Green > 255)
                        {
                            newColor.Green = 255;
                        }

                        if (newColor.Blue < 0)
                        {
                            newColor.Blue = 0;
                        }
                        else if (newColor.Blue > 255)
                        {
                            newColor.Blue = 255;
                        }

                        imageFiltred[i, j].Red = (byte)(newColor.Red);
                        imageFiltred[i, j].Green = (byte)(newColor.Green);
                        imageFiltred[i, j].Blue = (byte)(newColor.Blue);
                    }

                    Interlocked.Increment(ref progress);

                    if (!ProgressCallback(progress, height))
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                            "canceling");

                        loopState.Break();
                    }
                }
            );

            if (loopResult.IsCompleted)
            {
                Console.WriteLine($"{name} is succesfully applicated");
            }
            else
            {
                Console.WriteLine($"{name} application has been canceled");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        Color<int> Convolution(Color<byte>[,] imageUnfiltred, int i, int j, Color<int> newColor, int[,] matrix)
        {
            for (int k = -1; k < 2; k++)
            {
                for (int m = -1; m < 2; m++)
                {
                    newColor.Red += imageUnfiltred[i + k, j + m].Red * matrix[k + 1, m + 1];
                    newColor.Green += imageUnfiltred[i + k, j + m].Green * matrix[k + 1, m + 1];
                    newColor.Blue += imageUnfiltred[i + k, j + m].Blue * matrix[k + 1, m + 1];
                }
            }

            return (newColor);
        }

        /*Simulation of real situations*/
        bool ProgressCallback(int progress, int height)
        {
            Random random = new Random();

            if (random.Next(10000) == 19)
            {
                Console.Title = "Chance 0.0001 dropped! Client has been disconnected, progress sending has been canceled";

                return false;
            }

            Console.Title = $"Progress {(int)(progress / (double)height * 100)}% has been sended successfully";

            /*Change to false to have a 100% chance disconnection with the client*/
            return true;
        }
    }
}