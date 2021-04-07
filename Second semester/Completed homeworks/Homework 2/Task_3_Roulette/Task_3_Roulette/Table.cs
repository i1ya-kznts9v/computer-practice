using System;

namespace Task_3_Roulette
{
    public class Table
    {
        private struct Number
        {
            public ColorsAndParities colorAndParity;
            public int dozen;
        }

        //private struct Number
        //{
        //    public Colors color;
        //    public Parities parity;
        //    public byte dozen;
        //}

        public int CashBalance { get; set; }

        private Number[] numbers = new Number[37];
        private Random roulette = new Random();
        private int spinResult;

        public Table()
        {
            numbers[0].colorAndParity = ColorsAndParities.GreenAndNENO;
            numbers[0].dozen = 0;

            for (int i = 1; i <= 36; i++)
            {
                if (i % 2 == 0)
                {
                    numbers[i].colorAndParity = ColorsAndParities.BlackAndEven;
                }
                else if (i % 2 == 1)
                {
                    numbers[i].colorAndParity = ColorsAndParities.RedAndOdd;
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

            //numbers[0].color = Colors.Green;
            //numbers[0].parity = Parities.NENO;
            //numbers[0].dozen = 0;

            //for (int i = 1; i <= 36; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        numbers[i].color = Colors.Black;
            //        numbers[i].parity = Parities.Even;
            //    }
            //    else if (i % 2 == 1)
            //    {
            //        numbers[i].color = Colors.Red;
            //        numbers[i].parity = Parities.Odd;
            //    }

            //    if (i >= 1 && i <= 12)
            //    {
            //        numbers[i].dozen = 1;
            //    }
            //    else if (i >= 13 && i <= 24)
            //    {
            //        numbers[i].dozen = 2;
            //    }
            //    else if (i >= 25 && i <= 36)
            //    {
            //        numbers[i].dozen = 3;
            //    }
            //}
        }

        public void SpinRoulette()
        {
            spinResult = roulette.Next(0, 37);
        }

        // Without polymorphism

        public void GameProcessing(ref bool gameResult, int bet, ref int sumBet, ref int sum)
        {
            if(bet >= 0 && bet <= 36)
            {
                if (bet == spinResult)
                {
                    gameResult = true;

                    int multiplier = 35;
                    sum += sumBet + sumBet * multiplier; //35 to 1
                    CashBalance -= (sumBet + sumBet * multiplier);
                }
            }

            if(bet == 37 || bet == 38)
            {
                if (bet % 37 == (int)numbers[spinResult].colorAndParity)
                {
                    gameResult = true;

                    sum += sumBet + sumBet; //1 to 1
                    CashBalance -= (sumBet + sumBet);
                }
            }

            if(bet >= -3 && bet <= -1)
            {
                if (-bet == numbers[spinResult].dozen)
                {
                    gameResult = true;

                    int multiplier = 2;
                    sum += sumBet + sumBet * multiplier; //2 to 1
                    CashBalance -= (sumBet + sumBet * multiplier);
                }
            }
        }

        // With parametric polymorphism

        //public void GameProcessing<T>(ref bool gameResult, T betType, ref int sumBet, ref int sum)
        //{
        //    if (betType is int)
        //    {
        //        if (betType.Equals(spinResult))
        //        {
        //            gameResult = true;

        //            int multiplier = 35;
        //            sum += sumBet + sumBet * multiplier; //35 to 1
        //            CashBalance -= (sumBet + sumBet * multiplier);
        //        }
        //    }

        //    if (betType is Colors || betType is Parities)
        //    {
        //        if (betType.Equals(numbers[spinResult].color))
        //        {
        //            gameResult = true;

        //            sum += sumBet + sumBet; //1 to 1
        //            CashBalance -= (sumBet + sumBet);
        //        }
        //        else if (betType.Equals(numbers[spinResult].parity))
        //        {
        //            gameResult = true;

        //            sum += sumBet + sumBet; //1 to 1
        //            CashBalance -= (sumBet + sumBet);
        //        }
        //    }

        //    if (betType is byte)
        //    {
        //        if (betType.Equals(numbers[spinResult].dozen))
        //        {
        //            gameResult = true;

        //            int multiplier = 2;
        //            sum += sumBet + sumBet * multiplier; //2 to 1
        //            CashBalance -= (sumBet + sumBet * multiplier);
        //        }
        //    }
        //}

        // With ad-hoc polymorphism

        //public void GameProcess(ref bool gameResult, int number, ref int sumBet, ref int sum) // For specific number bet type.
        //{
        //    //Console.WriteLine($"{gameResult}");

        //    if (spinResult == number)
        //    {
        //        gameResult = true;

        //        int multiplier = 35;
        //        sum += sumBet + sumBet * multiplier; //35 to 1
        //        CashBalance -= (sumBet + sumBet * multiplier);
        //    }

        //    //Console.WriteLine($"{gameResult}");
        //}

        //public void GameProcess(ref bool gameResult, string colorOrParity, ref int sumBet, ref int sum) // For color and parity bet type.
        //{
        //    //Console.WriteLine($"{gameResult}");

        //    if (numbers[spinResult].color == colorOrParity)
        //    {
        //        gameResult = true;

        //        sum += sumBet + sumBet; //1 to 1
        //        CashBalance -= (sumBet + sumBet);
        //    }
        //    else if(numbers[spinResult].parity == colorOrParity)
        //    {
        //        gameResult = true;

        //        sum += sumBet + sumBet; //1 to 1
        //        CashBalance -= (sumBet + sumBet);
        //    }

        //    //Console.WriteLine($"{gameResult}");
        //}

        //public void GameProcess(ref bool gameResult, byte dozen, ref int sumBet, ref int sum) // For dozen bet type.
        //{
        //    //Console.WriteLine($"{gameResult}");

        //    if (numbers[spinResult].dozen == dozen)
        //    {
        //        gameResult = true;

        //        int multiplier = 2;
        //        sum += sumBet + sumBet * multiplier; //2 to 1
        //        CashBalance -= (sumBet + sumBet * multiplier);
        //    }

        //    //Console.WriteLine($"{gameResult}");
        //}
    }
}