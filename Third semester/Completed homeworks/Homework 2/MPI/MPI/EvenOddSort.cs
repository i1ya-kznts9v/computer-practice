using System;
using System.Collections.Generic;
using System.Linq;
using static MPI.FileManager;

namespace MPI
{
    class EvenOddSort
    {
        enum SortTypes
        {
            Increase,
            Decrease
        }

        static void Main(string[] args)
        {
            MPI.Environment.Run(ref args, comm =>
            {
                if (args.Length != 3)
                {
                    return;
                }

                SortTypes type;

                if (args[2].Equals("Increase"))
                {
                    type = SortTypes.Increase;
                }
                else if(args[2].Equals("Decrease"))
                {
                    type = SortTypes.Decrease;
                }
                else
                {
                    return;
                }

                List<int> partList;

                if (comm.Rank == 0)
                {
                    List<int> unsortedList = ReadFileToList(args[0]);

                    if(comm.Size > unsortedList.Count)
                    {
                        Console.WriteLine("There are too many nodes to sort this array");

                        return;
                    }

                    List<int>[] unsortedListDistributed = new List<int>[comm.Size];
                    int[] distribution = Distribute(unsortedList.Count, comm.Size);
                    
                    for(int i = 0, sum = 0; i < unsortedListDistributed.Length; i++)
                    {
                        unsortedListDistributed[i] = unsortedList.GetRange(sum, distribution[i]);
                        sum += distribution[i];
                    }

                    partList = comm.Scatter(unsortedListDistributed, 0);
                }
                else
                {
                    partList = comm.Scatter<List<int>>(0);
                }

                comm.Barrier();

                for (int i = 0; i < comm.Size; i++)
                {
                    if (i % 2 == 0)
                    {
                        comm.Barrier();

                        for (int j = 0; j < comm.Size / 2; j++)
                        {
                            partList = CompareExchange(comm, partList, j, type, true);
                        }

                        comm.Barrier();
                    }
                    else
                    {
                        comm.Barrier();

                        for (int j = 0; j < (comm.Size % 2 == 0 ? comm.Size / 2 - 1 : comm.Size / 2); j++)
                        {
                            partList = CompareExchange(comm, partList, j, type, false);
                        }

                        comm.Barrier();
                    }
                }

                comm.Barrier();

                if(comm.Rank == 0)
                {
                    comm.Send(partList, (comm.Rank + 1) % comm.Size, 3);

                    partList = comm.Receive<List<int>>((comm.Rank + comm.Size - 1) % comm.Size, 3);

                    WriteListToFile(args[1], partList);
                }
                else
                {
                    partList.InsertRange(0, comm.Receive<List<int>>((comm.Rank + comm.Size - 1) % comm.Size, 3));

                    comm.Send(partList, (comm.Rank + 1) % comm.Size, 3);
                }
            });
        }

        static List<int> CompareExchange(Intracommunicator comm, List<int> partList, int j, SortTypes type, bool evenIter)
        {
            int evenNode;
            int oddNode;

            if(evenIter)
            {
                evenNode = 2 * j;
                oddNode = 2 * j + 1;
            }
            else
            {
                evenNode = 2 * j + 1;
                oddNode = 2 * j + 2;
            }

            if (comm.Rank == evenNode)
            {                
                partList.AddRange(comm.Receive<List<int>>(oddNode, 1));

                switch (type)
                {
                    case SortTypes.Increase:
                        {
                            partList.Sort();

                            break;
                        }
                    case SortTypes.Decrease:
                        {
                            partList.Sort();
                            partList.Reverse();

                            break;
                        }
                }

                int distribution = partList.Count / 2;

                comm.Send(partList.Skip(distribution).ToList(), oddNode, 2);
                partList = partList.Take(distribution).ToList();
            }
            else if (comm.Rank == oddNode)
            {
                comm.Send(partList, evenNode, 1);

                partList = comm.Receive<List<int>>(evenNode, 2);
            }

            return (partList);
        }

        static int[] Distribute(int elements, int nodes)
        {
            int[] numbersPerNodes = new int[nodes];
            int distribution = elements / nodes;

            for (int i = 0; i < nodes - 1; i++)
            {
                numbersPerNodes[i] = distribution;
                elements -= distribution;
            }
            numbersPerNodes[nodes - 1] = elements;

            return (numbersPerNodes);
        }
    }
}