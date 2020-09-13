using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Chat
{
    public class Client
    {
        private string name;
        public Dictionary<IPEndPoint, string> ConnectedClients { get; private set; }
        private IPEndPoint iPEndPoint;
        private Socket socket;
        private IUserMethods userMethods;
        private IStringMethods stringMethods;

        private enum ConsoleSignal
        {
            CtrlC = 0,
            CtrlBreak = 1,
            Close = 2,
            LogOff = 5,
            Shutdown = 6
        };

        private delegate void SignalHandler(ConsoleSignal consoleSignal);
        private SignalHandler signalHandler;

        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        private static extern bool SetSignalHandler(SignalHandler handler, bool add);

        private void HandleConsoleSignal(ConsoleSignal consoleSignal)
        {
            Disconnect();
        }

        public Client(IStringMethods stringMethods, IUserMethods userMethods)
        {
            signalHandler += HandleConsoleSignal;
            SetSignalHandler(signalHandler, true);

            ConnectedClients = new Dictionary<IPEndPoint, string>();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.stringMethods = stringMethods;
            this.userMethods = userMethods;

            iPEndPoint = null;
            bool isStarted = false;

            while (isStarted != true)
            {
                Console.Write("Input your port: ");
                int port = userMethods.ReadPort();

                try
                {
                    foreach (var iPAdress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (iPAdress.AddressFamily == AddressFamily.InterNetwork)
                        {
                            iPEndPoint = new IPEndPoint(iPAdress, port);
                            break;
                        }
                    }

                    socket.Bind(iPEndPoint);
                    isStarted = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("This port is already in use.");
                }
            }

            Console.Write("Input your name: ");
            name = userMethods.ReadName() + '(' + iPEndPoint.ToString() + ')';
            Console.Write("Your full name is ");
            Console.WriteLine(name);
            Console.WriteLine();
            this.userMethods.PrintUsingInformation();
            Console.WriteLine();
        }

        public Client(string name, IPEndPoint iPEndPoint, Socket socket, Dictionary<IPEndPoint, string> connectedClients,
            IStringMethods stringMethods, IUserMethods userMethods)
        {
            signalHandler += HandleConsoleSignal;
            SetSignalHandler(signalHandler, true);

            this.name = name;
            ConnectedClients = connectedClients;
            this.iPEndPoint = iPEndPoint;
            this.socket = socket;
            this.userMethods = userMethods;
            this.stringMethods = stringMethods;
        }

        private bool isConnected(IPEndPoint iPEndPoint)
        {
            return (ConnectedClients.ContainsKey(iPEndPoint));
        }

        public void AddConnectedClient(KeyValuePair<IPEndPoint, string> client)
        {
            if (!isConnected(client.Key) && !IPEndPoint.Equals(client.Key, iPEndPoint))
            {
                ConnectedClients.Add(client.Key, client.Value);
            }
        }

        public void DeleteConnectedClient(IPEndPoint iPEndPoint)
        {
            ConnectedClients.Remove(iPEndPoint);
        }

        private void SendIPToConnectedClients(string input)
        {
            string message = "@+" + input;
            byte[] data = Encoding.Unicode.GetBytes(message);

            foreach (var connectedClient in ConnectedClients)
            {
                socket.SendTo(data, connectedClient.Key);
            }
        }

        private void SendListOfConnectedClients(IPEndPoint iPEndPoint)
        {
            string message = null;

            if (ConnectedClients.Count == 0)
            {
                message = "@#" + name;
            }
            else
            {
                message = "@#" + name + ", " + stringMethods.ListOfConnectedClientsToString(this);
            }

            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.SendTo(data, iPEndPoint);
        }

        public void SendMessageToConnectedClients(string message)
        {
            string fullMessage = "@=" + name + ": " + message;
            byte[] data = Encoding.Unicode.GetBytes(fullMessage);

            foreach (var connectedClient in ConnectedClients)
            {
                socket.SendTo(data, connectedClient.Key);
            }
        }

        public void Connect()
        {
            Console.Write("Input \"IP:port\" to connect: ");
            bool isSended = false;

            while (isSended != true)
            {
                IPEndPoint iPEndPoint = userMethods.ReadIPEndPoint();

                if (IPEndPoint.Equals(iPEndPoint, this.iPEndPoint))
                {
                    Console.WriteLine("It's your IP:port. Try again.");
                    return;
                }

                if (isConnected(iPEndPoint))
                {
                    Console.WriteLine("You are already connected to this client. Try again.");
                    return;
                }

                string message = null;

                if (ConnectedClients.Count == 0)
                {
                    message = "@*" + name;
                }
                else
                {
                    message = "@*" + name + ", " + stringMethods.ListOfConnectedClientsToString(this);
                }

                byte[] data = Encoding.Unicode.GetBytes(message);

                try
                {
                    socket.SendTo(data, iPEndPoint);
                    isSended = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input. Try again.");
                }
            }
        }

        public void Disconnect()
        {
            if (ConnectedClients.Count > 0)
            {
                string message = "@-" + name;
                byte[] data = Encoding.Unicode.GetBytes(message);

                foreach (var connectedClient in ConnectedClients)
                {
                    socket.SendTo(data, connectedClient.Key);
                }

                ConnectedClients.Clear();
            }
        }

        public void SocketClose()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void ReceiveMessage(IPEndPoint sender, string input)
        {
            try
            {
                if(input.Length > 0)
                {
                    char firstLetter = input[0];
                    string message = input.Remove(0, 1);

                    switch (firstLetter)
                    {
                        case '-':
                            {
                                IPEndPoint iPEndPoint = stringMethods.StringToClient(message).Key;
                                userMethods.Disconnect(message);
                                DeleteConnectedClient(iPEndPoint);

                                break;
                            }
                        case '=':
                            {
                                userMethods.Message(message);

                                break;
                            }
                        case '*':
                            {
                                SendListOfConnectedClients(sender);

                                //goto case '#';
                                SendIPToConnectedClients(message);

                                //goto case '+';
                                stringMethods.StringToListOfConnectedClients(this, message);
                                userMethods.Connect(message);

                                break;
                            }
                        case '#':
                            {
                                SendIPToConnectedClients(message);

                                //goto case '+';
                                stringMethods.StringToListOfConnectedClients(this, message);
                                userMethods.Connect(message);

                                break;
                            }
                        case '+':
                            {
                                stringMethods.StringToListOfConnectedClients(this, message);
                                userMethods.Connect(message);

                                break;
                            }
                    }
                }
            }
            catch(Exception)
            {
                return;
            }
        }

        private void WaitMessage()
        {
            IPEndPoint sender = null;

            try
            {
                while (true)
                {
                    string input = string.Empty;
                    byte[] data = new byte[2048];
                    int bytes = 0;
                    EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                    if (socket.Available != 0)
                    {
                        Array.Resize(ref data, socket.Available);
                    }

                    do
                    {
                        bytes = socket.ReceiveFrom(data, ref endPoint);
                        input = input + Encoding.Unicode.GetString(data, 0, bytes);
                    }
                    while (socket.Available > 0);

                    sender = endPoint as IPEndPoint;

                    if (input[0] == '$')
                    {
                        input = InteractionProtocol.MessageProcessing(input, sender);
                    }
                    else if (input[0] == '@')
                    {
                        input = input.Remove(0, 1);
                    }

                    ReceiveMessage(sender, input);
                }
            }
            catch(SocketException socketException)
            {
                Console.WriteLine(socketException.Message);
            }
        }

        public void StartWaitingMessage()
        {
            Task waiting = new Task(WaitMessage);
            waiting.Start();
        }
    }
}