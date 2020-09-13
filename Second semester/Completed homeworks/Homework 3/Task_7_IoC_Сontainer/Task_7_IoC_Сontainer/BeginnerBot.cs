﻿namespace Task_7_IoC_Сontainer
{
    public class BeginnerBot : Player //Free stategy: a player bets only on a certain number, doubles the previous bet in case of defeat,
                                      //or makes a new bet in case of victory.
                                      //It's a light bot.
    {
        public BeginnerBot(Table table) : base("BeginnerBot", table)
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

                botTable.GameProcess(ref gameResult, brain.Next(0, 36), ref sumBet, ref sum);
                gamesPlayedCounter++;

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

                gameResultPrevious = gameResult;
            }
        }

        public override void DisplayInformation(int gamesMustPlay)
        {
            base.DisplayInformation(gamesMustPlay);
        }
    }
}
