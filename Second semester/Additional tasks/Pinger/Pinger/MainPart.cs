using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Pinger
{
    class MainPart
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the first site: ");
            string site1 = Console.ReadLine();
            Console.Write("Enter the second site: ");
            string site2 = Console.ReadLine();

            long[] site1Time = PingSomeTimes(50, site1);
            double site1Average;
            double site1Sigma = StandartDeviation(site1Time, out site1Average);
            Console.WriteLine($"Average time: {site1Average}; Standart deviation: {site1Sigma}");

            Console.WriteLine("\n");

            long[] site2Time = PingSomeTimes(50, site2);
            double site2Average;
            double site2Sigma = StandartDeviation(site2Time, out site2Average);
            Console.WriteLine($"Average time: {site2Average}; Standart deviation: {site2Sigma}");

            Console.WriteLine("\n");

            double site1Pr = site1Average - site1Sigma;
            double site2Pr = site2Average - site2Sigma;

            Console.Write("Decision: ");

            if(site1Pr - site2Pr > 0)
            {
                if (site1Pr - site2Pr - Math.Sqrt(Math.Pow(site1Sigma, 2) + Math.Pow(site2Sigma, 2)) > 0)
                {
                    Console.WriteLine($"{site2} is closer than {site1}.") ;
                }
                else
                {
                    Console.WriteLine("Unable to estimate distance.");
                }
            }
            else if(site2Pr - site1Pr > 0)
            {
                if (site2Pr - site1Pr - Math.Sqrt(Math.Pow(site2Sigma, 2) + Math.Pow(site1Sigma, 2)) > 0)
                {
                    Console.WriteLine($"{site1} is closer than {site2}.");
                }
                else
                {
                    Console.WriteLine("Unable to estimate distance.");
                }
            }
            else
            {
                Console.WriteLine("Unable to estimate distance.");
            }

            Console.ReadKey();
        }

        static long[] PingSomeTimes(int times, string site)
        {
            Ping ping = new Ping();
            long[] time = new long[50];

            for (int i = 0; i < times; i++)
            {
                PingReply reply = ping.Send(site);
                time[i] = reply.RoundtripTime;
                Console.WriteLine($"IP: {reply.Address} Status: {reply.Status} Time: {reply.RoundtripTime} ms");
            }

            Console.WriteLine();

            return (time);
        }

        static double StandartDeviation(long[] time, out double s)
        {
            s = time.Sum() / time.Length;
            double q = 0;

            for(int i = 0; i < time.Length; i++)
            {
                q += Math.Pow(time[i] - s, 2);
            }

            double sigma = Math.Sqrt(q / (time.Length * (time.Length - 1)));
            double fract = sigma - Math.Truncate(sigma);
            int cursor = 1;

            while(Math.Truncate(fract * cursor) == 0)
            {
                cursor *= 10;

                if(Math.Truncate(fract * cursor) != 0)
                {
                    if(Math.Truncate(fract * cursor) == 1)
                    {
                        sigma = Math.Round(sigma, (int)(Math.Log10((double)cursor) + 1));
                    }
                    else
                    {
                        sigma = Math.Round(sigma, (int)Math.Log10((double)cursor));
                    }
                }
            }

            return (2 * sigma);
        }
    }
}