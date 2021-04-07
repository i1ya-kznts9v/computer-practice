using System;
using System.Collections.Generic;
using System.Net;

namespace Task9.Chat
{
    public class StringMethods : IStringMethods
    {
        public string ListOfConnectedClientsToString(Client client)
        {
            string result = string.Empty;

            foreach (var connectedClient in client.ConnectedClients)
            {
                result = result + connectedClient.Value + "(" + connectedClient.Key.ToString() + "), ";
            }

            if (result.Length > 0)
            {
                result = result.Remove(result.Length - 2);
            }

            return (result);
        }

        public IPEndPoint StringToIPEndPoint(string input)
        {
            string[] inputSplitted = input.Split(':');

            if (inputSplitted.Length != 2)
            {
                throw new Exception("Incorrect input.");
            }

            IPAddress ip;

            if (!IPAddress.TryParse(inputSplitted[0].Trim(), out ip))
            {
                throw new Exception("Incorrect IP-adress.");
            }

            int port;

            if (!Int32.TryParse(inputSplitted[1].Trim(), out port) || port < 1 || port > 65535)
            {
                throw new Exception("Incorrect port.");
            }

            return (new IPEndPoint(ip, port));
        }

        public KeyValuePair<IPEndPoint, string> StringToClient(string input)
        {
            string[] inputSplitted = input.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            if (inputSplitted.Length != 2)
            {
                throw new Exception("Incorrect input.");
            }

            string name = inputSplitted[0];
            IPEndPoint iPEndPoint = StringToIPEndPoint(inputSplitted[1]);

            return (new KeyValuePair<IPEndPoint, string>(iPEndPoint, name));
        }

        public void StringToListOfConnectedClients(Client client, string input)
        {
            string[] inputSplitted = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var fullName in inputSplitted)
            {
                KeyValuePair<IPEndPoint, string> newClient = StringToClient(fullName);
                client.AddConnectedClient(newClient);
            }
        }
    }
}