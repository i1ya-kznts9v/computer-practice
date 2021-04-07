namespace Task_7_IoC_Сontainer
{
    public class MartingaleBot : Player //Info about a strategy: https://casino.ruletka.guru/kak-obigrat-ruletku-v-casino/martingejl-ruletka.html.
                                        //It's a medium bot.
    {
        public MartingaleBot(Table table) : base("MartingaleBot", table)
        {

        }

        protected override void СalculateMoney()
        {
            int intellect = 0;

            if (sum >= 50)
            {
                intellect = 5;
            }
            else if (sum >= 25 && sum < 50)
            {
                intellect = 4;
            }
            else if (sum >= 10 && sum < 25)
            {
                intellect = 3;
            }
            else if (sum >= 5 && sum < 10)
            {
                intellect = 2;
            }
            else if (sum >= 1 && sum < 5)
            {
                intellect = 1;
            }

            switch (brain.Next(1, intellect))
            {
                case 1:
                    {
                        sumBet = 1;
                        sum -= sumBet;
                        botTable.cashBalance += sumBet;

                        break;
                    }
                case 2:
                    {
                        sumBet = 5;
                        sum -= sumBet;
                        botTable.cashBalance += sumBet;

                        break;
                    }
                case 3:
                    {
                        sumBet = 10;
                        sum -= sumBet;
                        botTable.cashBalance += sumBet;

                        break;
                    }
                case 4:
                    {
                        sumBet = 25;
                        sum -= sumBet;
                        botTable.cashBalance += sumBet;

                        break;
                    }
                case 5:
                    {
                        sumBet = 50;
                        sum -= sumBet;
                        botTable.cashBalance += sumBet;

                        break;
                    }
            }
        }

        protected override void SelectBet(bool gameResultPrevious)
        {
            switch (gameResultPrevious)
            {
                case true:
                    {
                        СalculateMoney();

                        sumBetPrevious = sumBet;

                        break;
                    }
                case false:
                    {
                        if (sum < 2 * sumBetPrevious)
                        {
                            СalculateMoney();

                            sumBetPrevious = sumBet;
                        }
                        else
                        {
                            sumBetPrevious *= 2;
                            sum -= sumBetPrevious;
                            botTable.cashBalance += sumBetPrevious;
                        }

                        break;
                    }
            }
        }

        public override void MakeBet()
        {
            if (sum > 0)
            {
                gameResult = false;

                //Console.WriteLine($"{sum}");

                SelectBet(gameResultPrevious);

                //Console.WriteLine($"{sumBet}");

                switch (brain.Next(1, 2))
                {
                    case 1:
                        {
                            switch (brain.Next(1, 2))
                            {
                                case 1:
                                    {
                                        string color = "Black";

                                        botTable.GameProcess(ref gameResult, color, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                                case 2:
                                    {
                                        string color = "Red";

                                        botTable.GameProcess(ref gameResult, color, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                            }

                            break;
                        }
                    case 2:
                        {
                            switch (brain.Next(1, 2))
                            {
                                case 1:
                                    {
                                        string parity = "Even";

                                        botTable.GameProcess(ref gameResult, parity, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                                case 2:
                                    {
                                        string parity = "Odd";

                                        botTable.GameProcess(ref gameResult, parity, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                            }

                            break;
                        }
                }

                //if (gameResult == true)
                //{
                //    Console.WriteLine($"{gameResult}");
                //}
                //else
                //{
                //    Console.WriteLine($"{gameResult}");
                //}

                //Console.WriteLine($"{sum}");
                //Console.WriteLine();

                gameResultPrevious = gameResult;
            }
        }

        public override void DisplayInformation(int gamesMustPlay)
        {
            base.DisplayInformation(gamesMustPlay);
        }
    }
}
