using System;
using System.Net;

namespace Task9.Chat
{
    static class InteractionProtocol
    {
        public static string MessageProcessing(string input, IPEndPoint sender)
        {
            input = input.Remove(0, 1);

            try
            {
                if (input.Length > 0)
                {
                    char firstLetter = input[0];
                    string message = input.Remove(0, 1);

                    switch (firstLetter)
                    {
                        case '*':
                            {
                                return ('*' + FromIpsToFullNames(message));
                            }
                        case '+':
                            {
                                return ('+' + FromIpsToFullNames(message));
                            }
                        case '-':
                            {
                                return ('-' + "Anonymous(" + message + ')');
                            }
                        default:
                            {
                                return ('=' + "Anonymous(" + sender.ToString() + "): " + firstLetter + message);
                            }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string FromIpsToFullNames(string input)
        {
            string fullNames = string.Empty;
            string[] iPs = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var iP in iPs)
            {
                fullNames += "Anonymous(" + iP + "), ";
            }

            fullNames = fullNames.Remove(fullNames.Length - 2);

            return (fullNames);
        }
    }
}