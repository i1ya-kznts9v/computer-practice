namespace Task_3_Roulette
{
    public class TitanicBot : Player //Info about a strategy: https://casino.ruletka.guru/kak-obigrat-ruletku-v-casino/evropejskaya-ruletka-na-realnye-dengi-titanik.html.                      
                                     //The strategy is changed so that a dozen change each time. I think it's make tactics more successful.
                                     //It's а strong bot. Many casinos have already added it to the "black list".
    {
        private bool[] gameResultsOf6Previous = new bool[6];
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

            switch (brain.Next(1, intellect + 1))
            {
                case 1:
                    {
                        sumBet = 1;
                        sum -= sumBet;
                        botTable.CashBalance += sumBet;

                        break;
                    }
                case 2:
                    {
                        sumBet = 5;
                        sum -= sumBet;
                        botTable.CashBalance += sumBet;

                        break;
                    }
                case 3:
                    {
                        sumBet = 10;
                        sum -= sumBet;
                        botTable.CashBalance += sumBet;

                        break;
                    }
                case 4:
                    {
                        sumBet = 25;
                        sum -= sumBet;
                        botTable.CashBalance += sumBet;

                        break;
                    }
                case 5:
                    {
                        sumBet = 50;
                        sum -= sumBet;
                        botTable.CashBalance += sumBet;

                        break;
                    }
            }
        }

        private void AdditionalMoneyChecking()
        {
            if (sumBet > sum)
            {
                СalculateMoney();

                sumBetPrevious = sumBet;
            }
            else
            {
                sum -= sumBet;
                botTable.CashBalance += sumBet;
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

                            sumBet = sumBetPrevious;
                            sum -= sumBet;
                            botTable.CashBalance += sumBetPrevious;
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

                if (gamesPlayedCounter % 6 == 0)
                {
                    SelectBet(gameResultPrevious);
                }
                else if(gamesPlayedCounter % 6 == 1)
                {
                    sumBet = sumBetPrevious;

                    AdditionalMoneyChecking();
                }
                else if (gamesPlayedCounter % 6 >= 2 && gamesPlayedCounter % 6 <= 4)
                {
                    sumBet *= (strategyCounter + 1);

                    AdditionalMoneyChecking();
                }
                else if (gamesPlayedCounter % 6 == 5)
                {
                    sumBet *= (strategyCounter + 2);

                    AdditionalMoneyChecking();
                }

                switch (brain.Next(1, 4))
                {
                    case 1:
                        {
                            //int dozen = 1; // For any polymorphism
                            int dozen = -1;

                            botTable.GameProcessing(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                    case 2:
                        {
                            //int dozen = 2; // For any polymorphism
                            int dozen = -2;

                            botTable.GameProcessing(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                    case 3:
                        {
                            //int dozen = 3; // For any polymorphism
                            int dozen = -3;

                            botTable.GameProcessing(ref gameResult, dozen, ref sumBet, ref sum);
                            gamesPlayedCounter++;

                            break;
                        }
                }

                sumBet = sumBetPrevious;

                strategyCounter = (gamesPlayedCounter - 1) % 6;
                gameResultsOf6Previous[strategyCounter] = gameResult;

                if (strategyCounter == 5)
                {
                    int loseCounter = 0;

                    for (int i = 0; i < 6; i++)
                    {
                        if (gameResultsOf6Previous[i])
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