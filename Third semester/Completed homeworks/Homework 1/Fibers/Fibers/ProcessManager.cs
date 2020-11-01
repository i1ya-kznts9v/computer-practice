using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Fibers
{
    public static class ProcessManager
    {
        static Dictionary<uint, int> Fibers = new Dictionary<uint, int>();
        static List<uint> Finished = new List<uint>();

        static Policy Policy;

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

            Fibers.Add(fiber.Id, process.Priority);
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

            Switch(false);

            Thread.Sleep(1); // In connection with the problem with Fiber API in tests
            Dispose();
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
                        choice = Fibers.OrderByDescending(x => x.Value).First().Key;

                        if (Fibers.Count > 1)
                        {
                            Dictionary<uint, int> newFibers = new Dictionary<uint, int>();

                            foreach (var fiber in Fibers)
                            {
                                if (fiber.Key == choice)
                                {
                                    newFibers.Add(fiber.Key, 0);
                                }
                                else
                                {
                                    newFibers.Add(fiber.Key, fiber.Value + 1);
                                }
                            }

                            Fibers = newFibers;
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