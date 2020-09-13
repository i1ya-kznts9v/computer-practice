using System.Collections.Generic;
using System.Net;

namespace Task9.Chat
{
    public interface IStringMethods
    {
        string ListOfConnectedClientsToString(Client client);
        IPEndPoint StringToIPEndPoint(string input);
        KeyValuePair<IPEndPoint, string> StringToClient(string input);
        void StringToListOfConnectedClients(Client client, string input);
    }
}