using System;
using System.Linq;
using System.Net;

namespace Task9.Chat
{
    public class UserMethods : IUserMethods
    {
        private IStringMethods stringMethods;
        public UserMethods(IStringMethods stringMethods)
        {
            this.stringMethods = stringMethods;
        }

        public string ReadMessage()
        {
            return (Console.ReadLine());
        }

        public int ReadPort()
        {
            int port = 0;
            string input = null;
            bool isInt = false;

            while(isInt != true || port < 1 || port > 65535)
            {
                try
                {
                    input = Console.ReadLine();

                    if (Exit(input))
                    {
                        Environment.Exit(0);
                    }

                    isInt = Int32.TryParse(input, out port);

                    if(port < 1 || port > 65535)
                    {
                        throw new Exception();
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Incorrect port. Try again.");
                }
            }

            return(port);
        }

        public string ReadName()
        {
            string name = null;
            bool isName = false;

            while(isName != true)
            {
                name = Console.ReadLine();

                try
                {
                    if(Exit(name))
                    {
                        Environment.Exit(0);
                    }

                    if (name == string.Empty || name.Contains(' '))
                    {
                        throw new Exception();
                    }

                    isName = true;
                }
                catch(Exception)
                {
                    Console.WriteLine("Incorrect name. Try Again.");
                }
            }

            return (name);
        }

        public IPEndPoint ReadIPEndPoint()
        {
            string input = null;
            IPEndPoint iPEndPoint = null;
            bool isIPEndPoint = false;

            while(!isIPEndPoint)
            {
                try
                {
                    input = Console.ReadLine();

                    if(Exit(input))
                    {
                        Environment.Exit(0);
                    }

                    iPEndPoint = stringMethods.StringToIPEndPoint(input);
                    isIPEndPoint = true;
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return (iPEndPoint);
        }

        public bool Exit(string input)
        {
            return (String.Equals(input.Trim().ToLower(), "exit"));
        }

        public void PrintUsingInformation()
        {
            Console.WriteLine("List of available commands:");
            Console.WriteLine("Use command \"connect\" to establish a connection to any chat version\n" +
                "and then enter IP:port to connect");
            Console.WriteLine("Use command \"disconnect\" to break the connection");
            Console.WriteLine("Use command \"clients\" to show connected clients");
            Console.WriteLine("Use command \"exit\" to close the application");
            Console.WriteLine("If the text entered is not one of the commands, " +
                "it will be considered a message and\nwill be visible to everyone who is in the chat.");
        }

        public void Connect(string name)
        {
            Console.WriteLine($"{name} joined the chat!");
            Console.WriteLine();
        }

        public void Disconnect(string name)
        {
            Console.WriteLine($"{name} left the chat!");
            Console.WriteLine();
        }

        public void PrintConnectedClients(Client client)
        {
            string[] connectedClients = stringMethods.ListOfConnectedClientsToString(client).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Connected clients:");

            foreach (var connectedClient in connectedClients)
            {
                Console.WriteLine(connectedClient.Trim());
            }

            Console.WriteLine($"Total: {client.ConnectedClients.Count} clients.");
            Console.WriteLine();
        }

        public void StopUsingChat(Client client)
        {
            if(client.ConnectedClients.Count > 0)
            {
                string[] connectedClients = stringMethods.ListOfConnectedClientsToString(client).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine($"You disconnected from:");

                foreach (var connectedClient in connectedClients)
                {
                    Console.WriteLine(connectedClient.Trim());
                }

                Console.WriteLine($"Total: {client.ConnectedClients.Count} clients.");
                Console.WriteLine();
            }
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}