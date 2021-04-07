using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Future
{
    public class ModifiedCascade : AModel, IVectorLengthComputer
    {
        public double ComputeLength(int[] coordinates)
        {
            if (coordinates.Length == 0)
            {
                return 0;
            }

            List<Task<int>> futures = new List<Task<int>>();
            int pivot = (int)Math.Sqrt(coordinates.Length);

            for (int i = 0; i < coordinates.Length / pivot; i++)
            {
                List<int> part = new List<int>();

                part = TakePart(coordinates, i, coordinates.Length / pivot);

                futures.Add(Task.Factory.StartNew
                    (() => SequentialSum(part.Select(x => Square(x)).ToList())));
            }

            return Root(CascadeSum(futures));
        }

        List<int> TakePart(int[] array, int iter, int count)
        {
            List<int> part = new List<int>();

            for (int i = iter * array.Length / count; (i < array.Length)
                && (i < (iter + 1) * array.Length / count); i++)
            {
                part.Add(array[i]);
            }

            return part;
        }
    }
}