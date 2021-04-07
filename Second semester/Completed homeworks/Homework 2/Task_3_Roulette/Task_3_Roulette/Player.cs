using System;

namespace Task_3_Roulette
{
    public abstract class Player
    {
        public Player(string name, Table table)
        {
            botName = name;
            botTable = table;
            sum = brain.Next(100, 1001);
            Console.WriteLine($"The {botName} took {sum}$ with him.");
        }

        protected string botName;
        protected Table botTable;

        protected static Random brain = new Random(); // This field is static so that random numbers are not repeated.

        protected int sum;
        protected int sumBet;
        protected int sumBetPrevious;

        protected bool gameResult;
        protected bool gameResultPrevious = true;

        protected int gamesPlayedCounter = 0;

        protected abstract void СalculateMoney();
        protected abstract void SelectBet(bool gameResultAnyPrevious);
        public abstract void MakeBet();
        public virtual void DisplayInformation(int gamesMustPlay)
        {
            if (sum == 0 && gamesPlayedCounter >= 0)
            {
                Console.WriteLine($"The {botName} went bankrupt at the {gamesPlayedCounter} game.");
                gamesPlayedCounter = -1;
            }

            if (sum > 0 && gamesPlayedCounter == gamesMustPlay)
            {
                Console.WriteLine($"After {gamesPlayedCounter} moves, the {botName} has {sum}$ left.");
            }
        }
    }
}