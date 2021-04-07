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

        static Policy policy;
        static int highFreqRangeCount;
        static Random random;

        static uint current;
        static uint primary;
        static int next;

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

        public static void Run(Policy policyChoice)
        {
            if(Fibers.Count == 0)
            {
                Console.WriteLine("No processes to start");
                
                return;
            }

            policy = policyChoice;
            primary = Fibers.ElementAt(0).Key;

            if(policy == Policy.Fifo)
            {
                next = -1;
            }
            else if(policy == Policy.Priority)
            {
                highFreqRangeCount = ComputeHFRC();
                random = new Random();
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
                current = Fiber.PrimaryId;
            }
            else
            {
                current = Select();
            }

            Thread.Sleep(1); // In connection with the problem with Fiber API in tests
            Fiber.Switch(current);
        }

        static uint Select()
        {
            uint choice = 0;

            switch (policy)
            {
                case Policy.Fifo:
                    {
                        next = (next + 1) % Fibers.Count;
                        choice = Fibers.ElementAt(next).Key;

                        break;
                    }
                case Policy.Priority:
                    {
                        Fibers = Fibers.OrderByDescending(x => x.Value.Priority).ToDictionary(dict => dict.Key, dict => dict.Value);

                        var highFreqRange = Fibers.Take(highFreqRangeCount);
                        var readyFibers = highFreqRange.Where(x => x.Key != current && x.Value.IsReady);

                        if(readyFibers.Count() == 0)
                        {
                            if (highFreqRange.Count() == 1)
                            {
                                choice = highFreqRange.First().Key;
                            }
                            else
                            {
                                if(Fibers.Count == highFreqRange.Count())
                                {
                                    choice = highFreqRange.Where(x => x.Key != current).First().Key;
                                }
                                else
                                {
                                    var decision = random.Next(2);

                                    switch(decision)
                                    {
                                        case 0:
                                            {
                                                choice = highFreqRange.Where(x => x.Key != current).First().Key;

                                                break;
                                            }
                                        case 1:
                                            {
                                                var lowFreqRange = Fibers.Skip(highFreqRangeCount).ToList();

                                                var prioritySum = lowFreqRange.Sum(x => x.Value.Priority);
                                                var assigner = random.Next(prioritySum);

                                                int counter = 0;
                                                foreach(var fiber in lowFreqRange)
                                                {
                                                    counter += fiber.Value.Priority;

                                                    if(counter >= assigner)
                                                    {
                                                        choice = fiber.Key;

                                                        break;
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                }
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
            if (policy == Policy.Fifo)
            {
                next--;
            }

            Fibers.Remove(current);

            if (current != primary)
            {
                Finished.Add(current);
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