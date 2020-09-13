using System;

namespace Task_7_IoC_Сontainer
{
    public class Table
    {
        private struct Number
        {
            public string color;
            public string parity;
            public byte dozen;
        }

        public int cashBalance = 0;

        private Number[] numbers = new Number[37];
        private Random roulette = new Random();
        private int spinResult;

        public Table()
        {
            numbers[0].color = "Green";
            numbers[0].parity = "Not even, not odd";
            numbers[0].dozen = 0;

            for (int i = 1; i <= 36; i++)
            {
                if (i % 2 == 0)
                {
                    numbers[i].color = "Black";
                    numbers[i].parity = "Even";
                }
                else if (i % 2 == 1)
                {
                    numbers[i].color = "Red";
                    numbers[i].parity = "Odd";
                }

                if (i >= 1 && i <= 12)
                {
                    numbers[i].dozen = 1;
                }
                else if (i >= 13 && i <= 24)
                {
                    numbers[i].dozen = 2;
                }
                else if (i >= 25 && i <= 36)
                {
                    numbers[i].dozen = 3;
                }
            }
        }

        public void SpinRoulette()
        {
            spinResult = roulette.Next(0, 36);
        }

        public void GameProcess(ref bool gameResult, int number, ref int sumBet, ref int sum) //For specific number bet type.
        {
            //Console.WriteLine($"{gameResult}");

            if (spinResult == number)
            {
                gameResult = true;

                int multiplier = 35;
                sum += sumBet + sumBet * multiplier; //35 to 1
                cashBalance -= (sumBet + sumBet * multiplier);
            }

            //Console.WriteLine($"{gameResult}");
        }

        public void GameProcess(ref bool gameResult, string colorOrParity, ref int sumBet, ref int sum) //For color and parity bet type.
        {
            //Console.WriteLine($"{gameResult}");

            if (numbers[spinResult].color == colorOrParity)
            {
                gameResult = true;

                sum += sumBet + sumBet; //1 to 1
                cashBalance -= (sumBet + sumBet);
            }
            else if (numbers[spinResult].parity == colorOrParity)
            {
                gameResult = true;

                sum += sumBet + sumBet; //1 to 1
                cashBalance -= (sumBet + sumBet);
            }

            //Console.WriteLine($"{gameResult}");
        }

        public void GameProcess(ref bool gameResult, byte dozen, ref int sumBet, ref int sum) //For dozen bet type.
        {
            //Console.WriteLine($"{gameResult}");

            if (numbers[spinResult].dozen == dozen)
            {
                gameResult = true;

                int multiplier = 2;
                sum += sumBet + sumBet * multiplier; //2 to 1
                cashBalance -= (sumBet + sumBet * multiplier);
            }

            //Console.WriteLine($"{gameResult}");
        }
    }
}
