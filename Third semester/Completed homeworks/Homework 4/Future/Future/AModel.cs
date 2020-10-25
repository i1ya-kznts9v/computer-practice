using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Future
{
    public abstract class AModel
    {
        protected int SequentialSum(List<int> list)
        {
            return list.Sum();
        }

        protected int CascadeSum(List<Task<int>> futures)
        {
            while(futures.Count > 1)
            {
                List<Task<int>> nextfutures = new List<Task<int>>();

                if (futures.Count % 2 == 1)
                {
                    nextfutures.Add(futures[futures.Count - 1]);
                }

                for (int j = 0; j < futures.Count - 1; j += 2)
                {
                    int next = futures[j + 1].Result;

                    nextfutures.Add(futures[j].ContinueWith((it) => Sum(it.Result, next)));
                }

                futures.ForEach(future =>
                {
                    future.Wait();
                    future.Dispose();
                });
                futures.Clear();

                futures = nextfutures;
            }

            return futures[0].Result;
        }

        protected int Sum(int x, int y)
        {
            return x + y;
        }

        protected int Square(int x)
        {
            return x * x;
        }

        protected double Root(int x)
        {
            return Math.Sqrt(x);
        }
    }
}