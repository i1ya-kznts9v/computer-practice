using System.Net;

namespace Task9.Chat
{
    public interface IUserMethods
    {
        string ReadMessage();
        int ReadPort();
        string ReadName();
        IPEndPoint ReadIPEndPoint();
        bool Exit(string input);
        void PrintUsingInformation();
        void Connect(string name);
        void Disconnect(string name);
        void PrintConnectedClients(Client client);
        void StopUsingChat(Client client);
        void Message(string message);
    }
}