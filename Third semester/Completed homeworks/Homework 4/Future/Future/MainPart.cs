using System;

namespace Future
{
    class MainPart
    {
        static void Main(string[] args)
        {
            Cascade cascade = new Cascade();
            ModifiedCascade modifiedCascade = new ModifiedCascade();
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine(cascade.ComputeLength(array));
            Console.WriteLine(modifiedCascade.ComputeLength(array));

            Console.ReadKey();
        }
    }
}
