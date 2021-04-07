using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task9.Chat;
using Moq;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Task9.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void StringMethodsUnitTest()
        {
            IPEndPoint iPEndPoint = null;
            Dictionary<IPEndPoint, string> connectedClients = new Dictionary<IPEndPoint, string>();
            StringMethods stringMethods = new StringMethods();

            foreach (var iPAdress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (iPAdress.AddressFamily == AddressFamily.InterNetwork)
                {
                    iPEndPoint = new IPEndPoint(iPAdress, 1901);
                    break;
                }
            }

            string fullName = $"Ilya({iPEndPoint})";
            Client client = new Client(fullName, iPEndPoint, null, connectedClients, null, null);

            IPEndPoint iPEndPointVlad = stringMethods.StringToIPEndPoint("192.168.31.11:3011");
            IPEndPoint iPEndPointBill = stringMethods.StringToIPEndPoint("142.234.11.44:4007");

            stringMethods.StringToListOfConnectedClients(client, "Vlad(192.168.31.11:3011), Bill(142.234.11.44:4007)");
            Assert.AreEqual(true, client.ConnectedClients.Values.Contains("Vlad"));
            Assert.AreEqual(true, client.ConnectedClients.Keys.Contains(iPEndPointVlad));
            Assert.AreEqual(true, client.ConnectedClients.Values.Contains("Bill"));
            Assert.AreEqual(true, client.ConnectedClients.Keys.Contains(iPEndPointBill));

            Assert.AreEqual("Vlad(192.168.31.11:3011), Bill(142.234.11.44:4007)", stringMethods.ListOfConnectedClientsToString(client));

            Assert.AreEqual(new KeyValuePair<IPEndPoint, string>(iPEndPointVlad, "Vlad"), stringMethods.StringToClient("Vlad(192.168.31.11:3011)"));
            Assert.AreEqual(new KeyValuePair<IPEndPoint, string>(iPEndPointBill, "Bill"), stringMethods.StringToClient("Bill(142.234.11.44:4007)"));
        }

        [TestMethod]
        public void ChatUnitTest()
        {
            Client[] clients = new Client[2];
            Chat.Chat[] chats = new Chat.Chat[2];

            Mock<IUserMethods>[] clientMocks = new Mock<IUserMethods>[2];
            Mock<IUserMethods>[] chatMock = new Mock<IUserMethods>[2];

            string[] names = new string[] { "Vlad", "Bill" };
            int[] ports = new int[] { 3011, 4007 };
            IPEndPoint iPEndPointVlad = null;
            IPEndPoint iPEndPointBill = null;

            foreach (var iPAdress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (iPAdress.AddressFamily == AddressFamily.InterNetwork)
                {
                    iPEndPointVlad = new IPEndPoint(iPAdress, 3011);
                    iPEndPointBill = new IPEndPoint(iPAdress, 4007);
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                clientMocks[i] = new Mock<IUserMethods>();
                chatMock[i] = new Mock<IUserMethods>();

                clientMocks[i].Setup(r => r.ReadName()).Returns(names[i]);
                clientMocks[i].Setup(q => q.ReadPort()).Returns(ports[i]);

                clients[i] = new Client(new StringMethods(), clientMocks[i].Object);
            }

            chatMock[0].Setup(l => l.ReadMessage()).Returns("connect");
            clientMocks[0].Setup(l => l.ReadIPEndPoint()).Returns(iPEndPointBill);

            chatMock[1].Setup(p => p.ReadMessage()).Returns(() =>
            {
                Thread.Sleep(50);
                return ("exit");
            });

            chats[0] = new Chat.Chat(new StringMethods(), chatMock[0].Object);
            Thread thread = new Thread(() => chats[0].P2PChat(clients[0]));
            thread.Start();

            chats[1] = new Chat.Chat(new StringMethods(), chatMock[1].Object);
            chats[1].P2PChat(clients[1]);

            Assert.AreEqual(1, chats[0].ConnectedClientsHistory.Count);
            Assert.AreEqual(1, chats[1].ConnectedClientsHistory.Count);
        }
    }
}