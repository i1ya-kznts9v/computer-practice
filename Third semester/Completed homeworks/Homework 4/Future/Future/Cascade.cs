using System.Collections.Generic;
using System.Threading.Tasks;

namespace Future
{
    public class Cascade : AModel, IVectorLengthComputer
    {
        public double ComputeLength(int[] coordinates)
        {
            if(coordinates.Length == 0)
            {
                return 0;
            }

            List<Task<int>> futures = new List<Task<int>>();

            for (int i = 0; i < coordinates.Length; i++)
            {
                int it = coordinates[i];

                futures.Add(Task.Factory.StartNew
                    (() => Square(it)));
            }

            return Root(CascadeSum(futures));
        }
    }
}