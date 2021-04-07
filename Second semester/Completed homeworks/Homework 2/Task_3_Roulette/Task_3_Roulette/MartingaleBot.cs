namespace Task_3_Roulette
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
                        if(sum < 2 * sumBetPrevious)
                        {
                            СalculateMoney();

                            sumBetPrevious = sumBet;
                        }
                        else
                        {
                            sumBetPrevious *= 2;
                            sum -= sumBetPrevious;
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

                SelectBet(gameResultPrevious);

                switch (brain.Next(1, 3))
                {
                    case 1:
                        {
                            switch (brain.Next(1, 3))
                            {
                                case 1:
                                    {
                                        //Colors color = Colors.Black; // For any polymorphism
                                        int color = 37;

                                        botTable.GameProcessing(ref gameResult, color, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                                case 2:
                                    {
                                        //Colors color = Colors.Red; // For any polymorphism
                                        int color = 38;

                                        botTable.GameProcessing(ref gameResult, color, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                            }

                            break;
                        }
                    case 2:
                        {
                            switch (brain.Next(1, 3))
                            {
                                case 1:
                                    {
                                        //Parities parity = Parities.Even; // For any polymorphism
                                        int parity = 37;

                                        botTable.GameProcessing(ref gameResult, parity, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                                case 2:
                                    {
                                        //Parities parity = Parities.Odd; // For any polymorphism
                                        int parity = 38;

                                        botTable.GameProcessing(ref gameResult, parity, ref sumBet, ref sum);
                                        gamesPlayedCounter++;

                                        break;
                                    }
                            }

                            break;
                        }
                }

                gameResultPrevious = gameResult;
            }
        }

        public override void DisplayInformation(int gamesMustPlay)
        {
            base.DisplayInformation(gamesMustPlay);
        }
    }
}