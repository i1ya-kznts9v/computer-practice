using System;
using System.Collections.Generic;
using System.Net;

namespace Task9.Chat
{
    public class Chat
    {
        private IStringMethods stringMethods;
        private IUserMethods userMethods;
        public Dictionary<IPEndPoint, string> ConnectedClientsHistory { get; private set; }

        public Chat(IStringMethods stringMethods, IUserMethods userMethods)
        {
            this.stringMethods = stringMethods;
            this.userMethods = userMethods;
            ConnectedClientsHistory = new Dictionary<IPEndPoint, string>();
        }

        public void P2PChat(Client client)
        {
            client.StartWaitingMessage();

            bool isInChat = true;
            string message = string.Empty;

            while(isInChat != false)
            {
                try
                {
                    message = userMethods.ReadMessage();

                    switch (message.Trim().ToLower())
                    {
                        case "connect":
                            {
                                client.Connect();
                                ConnectedClientsHistory = client.ConnectedClients;

                                break;
                            }
                        case "disconnect":
                            {
                                userMethods.StopUsingChat(client);
                                client.Disconnect();

                                break;
                            }
                        case "clients":
                            {
                                userMethods.PrintConnectedClients(client);

                                break;
                            }
                        case "exit":
                            {
                                ConnectedClientsHistory = client.ConnectedClients;

                                client.Disconnect();
                                client.SocketClose();
                                isInChat = false;
                                //Environment.Exit(0);

                                break;
                            }
                        default:
                            {
                                client.SendMessageToConnectedClients(message);

                                break;
                            }
                    }
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}