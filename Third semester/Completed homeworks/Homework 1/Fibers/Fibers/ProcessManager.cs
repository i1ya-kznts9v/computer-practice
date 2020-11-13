using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Fibers
{
    public static class ProcessManager
    {
        static Dictionary<uint, Process> Fibers = new Dictionary<uint, Process>();
        static List<uint> Finished = new List<uint>();

        static Policy Policy;
        static int HighFreqRangeCount;

        static uint Current;
        static uint Primary;
        static int Next;

        public static int GetFibersCount()
        {
            return Fibers.Count;
        }

        public static void Add(Process process)
        {
            Action action = new Action(process.Run);
            Fiber fiber = new Fiber(action);

            Fibers.Add(fiber.Id, process);
        }

        public static void Run(Policy policy)
        {
            if(Fibers.Count == 0)
            {
                Console.WriteLine("No processes to start");
                
                return;
            }

            Policy = policy;
            Primary = Fibers.ElementAt(0).Key;

            if(Policy == Policy.Fifo)
            {
                Next = -1;
            }
            else if(Policy == Policy.Priority)
            {
                HighFreqRangeCount = ComputeHFRC();
            }

            Switch(false);

            Thread.Sleep(1); // In connection with the problem with Fiber API in tests
            Dispose();
        }

        /* Create 3 abstact priority classes: low, medium, high
           and compute low bound of high priority abstact class in terms
           of real priorities. Then count the number of processes in this
           high priority abstract class and get ratio of high priorities
           to other classes */
        static int ComputeHFRC()
        {
            int maxPriority = Fibers.Values.OrderByDescending(x => x.Priority).First().Priority;
            int lowerBound = (maxPriority / 3) * 2 + 1; 
            int count = 0;

            if(lowerBound > maxPriority)
            {
                int temp = lowerBound;
                lowerBound = maxPriority;
                maxPriority = temp;
            }

            foreach(var fiber in Fibers)
            {
                if(fiber.Value.Priority >= lowerBound)
                    count++;
            }

            return Fibers.Count / count;
        }

        public static void Switch(bool fiberFinished)
        {
            if(fiberFinished)
            {
                Delete();
            }

            if(Fibers.Count == 0)
            {
                Current = Fiber.PrimaryId;
            }
            else
            {
                Current = Select();
            }

            Thread.Sleep(1); // In connection with the problem with Fiber API in tests
            Fiber.Switch(Current);
        }

        static uint Select()
        {
            uint choice = 0;

            switch (Policy)
            {
                case Policy.Fifo:
                    {
                        Next = (Next + 1) % Fibers.Count;
                        choice = Fibers.ElementAt(Next).Key;

                        break;
                    }
                case Policy.Priority:
                    {
                        var highFreqRange = Fibers.OrderByDescending(x => x.Value.Priority).Take(HighFreqRangeCount);
                        var readyFibers = highFreqRange.Where(x => x.Key != Current && x.Value.IsReady);

                        if(readyFibers.Count() == 0)
                        {
                            if (highFreqRange.Count() == 1)
                            {
                                choice = highFreqRange.First().Key;
                            }
                            else
                            {
                                choice = highFreqRange.Where(x => x.Key != Current).First().Key;
                            }
                        }
                        else
                        {
                            choice = readyFibers.First().Key;
                        }

                        break;
                    }
            }

            return choice;
        }

        static void Delete()
        {
            if (Policy == Policy.Fifo)
            {
                Next--;
            }

            Fibers.Remove(Current);

            if (Current != Primary)
            {
                Finished.Add(Current);
            }
        }

        static void Dispose()
        {
            Finished.ForEach(x => Fiber.Delete(x));
            Finished.Clear();
            
            Fibers.Clear();
        }
    }
}