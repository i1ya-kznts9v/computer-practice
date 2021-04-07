namespace Task_7_IoC_Сontainer
{
    public class TitanicBot : Player //Info about a strategy: https://casino.ruletka.guru/kak-obigrat-ruletku-v-casino/evropejskaya-ruletka-na-realnye-dengi-titanik.html.                      
                                     //The strategy is changed so that a dozen change each time. I think it's make tactics more successful.
                                     //It's а very strong bot. Many casinos have already added it to the "black list".
    {
        private bool[] gameResultsOf6Previous = new bool[6];
        private int sumBetFirstOfSix;
        private int strategyCounter = 0;

        public TitanicBot(Table table) : base("TitanicBot", table)
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

                if (strategyCounter == 0)
                {
                    SelectBet(gameResultPrevious);
                    sumBetFirstOfSix = sumBet;
                }
                else if (strategyCounter >= 2 && strategyCounter <= 4)
                {
                    sumBet *= (strategyCounter + 1);
                }
                else if (strategyCounter == 5)
                {
                    sumBet *= (strategyCounter + 2);
                }

                //Console.WriteLine($"{sumBet}");

                switch (brain.Next(1, 3))
                {
                    case 1:
                        {
                            byte dozen = 1;

                            botTable.GameProcess(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                    case 2:
                        {
                            byte dozen = 2;

                            botTable.GameProcess(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                    case 3:
                        {
                            byte dozen = 3;

                            botTable.GameProcess(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                }

                //if (gameResult == true)
                //{
                //    Console.WriteLine("Win");
                //}
                //else
                //{
                //    Console.WriteLine("Lose");
                //}

                //Console.WriteLine($"{sum}");
                //Console.WriteLine();

                sumBet = sumBetFirstOfSix;

                strategyCounter = (gamesPlayedCounter - 1) % 6;
                gameResultsOf6Previous[strategyCounter] = gameResult;

                if (strategyCounter == 5)
                {
                    int loseCounter = 0;

                    for (int i = 0; i < 6; i++)
                    {
                        if (gameResultsOf6Previous[i] == true)
                        {
                            gameResultPrevious = true;

                            break;
                        }
                        else
                        {
                            loseCounter++;
                        }
                    }

                    if (loseCounter == 6)
                    {
                        gameResultPrevious = false;
                    }

                    strategyCounter = 0;
                }
            }
        }

        public override void DisplayInformation(int gamesMustPlay)
        {
            base.DisplayInformation(gamesMustPlay);
        }
    }
}
