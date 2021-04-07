using System;
using System.Threading;
using System.Threading.Tasks;
using Filters;

namespace Research
{
    class SingleAndParallelFilters
    {
        public Color<byte>[,] Median3x3ParallelLoopState(Color<byte>[,] imageUnfiltred)
        {
            string name = "Median3x3ParallelLoopState";
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
                        if(loopState.LowestBreakIteration.HasValue)
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

        public Color<byte>[,] Median3x3ParallelCancel(Color<byte>[,] imageUnfiltred)
        {
            string name = "Median3x3ParallelCancel";
            int progress = 2;

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            ParallelOptions parallelOptions = new ParallelOptions();
            CancellationTokenSource cancellationOption = new CancellationTokenSource();

            parallelOptions.CancellationToken = cancellationOption.Token;

            try
            {
                ParallelLoopResult loopResult = Parallel.For
                (1,
                height - 1,
                parallelOptions,
                i =>
                    {
                        Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                        for (int j = 1; j < width - 1; j++)
                        {
                            if(cancellationOption.IsCancellationRequested)
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

                            cancellationOption.Cancel();
                        }
                    }
                );

                if(loopResult.IsCompleted)
                {
                    Console.WriteLine($"{name} is succesfully applicated");
                }
            }
            catch(OperationCanceledException cancellation)
            {
                Console.WriteLine($"{name} application has been canceled:\n{cancellation.ToString()}");

                imageUnfiltred = null;
                imageFiltred = null;

                return imageFiltred;
            }
            finally
            {
                cancellationOption.Dispose();
            }

            imageUnfiltred = null;

            return imageFiltred;
        }

        public Color<byte>[,] Median3x3Single(Color<byte>[,] imageUnfiltred)
        {
            string name = "Median3x3Single";
            int progress = 2;

            int height = imageUnfiltred.GetLength(0);
            int width = imageUnfiltred.GetLength(1);

            Color<byte>[,] imageFiltred = new Color<byte>[height, width];

            for (int i = 1; i < height - 1; i++)
            {
                Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> applicating");

                for (int j = 1; j < width - 1; j++)
                {
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

                progress++;

                if (!ProgressCallback(progress, height))
                {
                    Console.WriteLine($"{name}: thread #{Thread.CurrentThread.ManagedThreadId.ToString()} on height {i.ToString()} -> " +
                        "canceling");
                    Console.WriteLine($"{name} application has been canceled");

                    imageUnfiltred = null;
                    imageFiltred = null;

                    return imageFiltred;
                }
            }

            imageUnfiltred = null;

            Console.WriteLine($"{name} is succesfully applicated");

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