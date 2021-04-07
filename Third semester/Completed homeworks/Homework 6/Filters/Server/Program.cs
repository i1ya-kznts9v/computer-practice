using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Filters;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Contract), new Uri("net.tcp://localhost:19011"));

            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "/mex");

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, false);
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            host.AddServiceEndpoint(typeof(IContract), binding, "/srv");

            host.Open();

            Console.WriteLine("Server started.\nTo correctly shut down the server, enter \"close\":");
            while (Console.ReadLine().ToLower() != "close")
            {
                Console.WriteLine("Unknown command");
            }

            host.Close();
        }
    }
}