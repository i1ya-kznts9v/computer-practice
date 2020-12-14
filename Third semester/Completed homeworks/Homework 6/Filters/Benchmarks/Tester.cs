using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Filters;
using System.Linq;

namespace Benchmarks
{
    class Tester : IContractCallback
    {
        AutoResetEvent waitHandler = new AutoResetEvent(false);

        byte[] imageUnfiltred = null;
        byte[] imageFiltred = null;

        volatile bool isRunning = false;
        volatile bool isCompleted = false;

        DateTime start = DateTime.MinValue;
        DateTime end = DateTime.MinValue;

        public Tester(byte[] bytes)
        {
            imageUnfiltred = bytes;

            Start();
        }

        public void Start()
        {
            isCompleted = false;
            isRunning = false;

            List<string> filters = null;

            try
            {
                start = DateTime.Now;
                ContractClientBase client = new ContractClientBase(new System.ServiceModel.InstanceContext(this));

                filters = client.GetAvailableFilters();

                client.ApplyFilter(filters.Last(), imageUnfiltred);
                isRunning = true;

                imageUnfiltred = null;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);

                return;
            }
        }

        public int GetTime()
        {
            if (isCompleted || isRunning)
            {
                // Service time limit leading to denial of service
                waitHandler.WaitOne(60000);

                if (imageFiltred == null)
                {
                    return -1;
                }

                return (int)(end - start).TotalMilliseconds;
            }

            return 0;
        }

        public void ImageCallback(byte[] bytes)
        {
            imageFiltred = bytes;
            end = DateTime.Now;

            isCompleted = true;
            isRunning = false;

            waitHandler.Set();
        }

        public void ProgressCallback(int progress)
        {
            return;
        }
    }
}