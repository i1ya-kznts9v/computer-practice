using System;
using System.IO;
using static Task_1_Filters.Filters;

namespace Task_1_Filters
{
    class MainPart
    {
        static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                throw new Exception("Incorrect number of program parameters entered.\nTry it again!");
            }

            Picture picture = new Picture();

            FileStream inputPicture = new FileStream(args[0], FileMode.Open);
            picture.Read(inputPicture);
            inputPicture.Close();

            ApplyFilter(args[1]);

            FileStream outputPicture = new FileStream(args[2], FileMode.Create);
            picture.Write(outputPicture);
            outputPicture.Close();
        }
    }
}
