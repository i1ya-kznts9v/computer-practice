namespace Task9.Chat
{
    class MainPart
    {
        static void Main(string[] args)
        {
            StringMethods stringMethods = new StringMethods();
            UserMethods userMethods = new UserMethods(stringMethods);

            Client client = new Client(stringMethods, userMethods);
            Chat chat = new Chat(stringMethods, userMethods);

            chat.P2PChat(client);
        }
    }
}