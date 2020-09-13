using System;

namespace Task_3_Roulette
{
    public class MainPart
    {
        static void Main() // Everything is the same as in Unit Test, just so quickly check its result.
        {
            Console.WriteLine("“I know only 2 ways to beat roulette: steal chips from the tableA\n" +
               "or open your own casino and put roulette there,” - Albert Einstein.\nNow we will check these words:\n");

            int gamesMustPlay = 42;

            Table tableA = new Table();

            MartingaleBot martin = new MartingaleBot(tableA);
            TitanicBot titan = new TitanicBot(tableA);
            BeginnerBot begin = new BeginnerBot(tableA);

            Console.WriteLine();

            for (int i = 0; i < gamesMustPlay; i++)
            {
                tableA.SpinRoulette();

                martin.MakeBet();
                titan.MakeBet();
                begin.MakeBet();

                martin.DisplayInformation(gamesMustPlay);
                titan.DisplayInformation(gamesMustPlay);
                begin.DisplayInformation(gamesMustPlay);
            }

            Console.WriteLine($"\nThe cash balance of the tableA is {tableA.CashBalance}$.\n\n");

            Table tableB = new Table();

            MartingaleBot martinB = new MartingaleBot(tableB);
            TitanicBot titanB = new TitanicBot(tableB);
            BeginnerBot beginB = new BeginnerBot(tableB);

            Console.WriteLine();

            for (int i = 0; i < gamesMustPlay; i++)
            {
                tableB.SpinRoulette();

                martinB.MakeBet();
                titanB.MakeBet();
                beginB.MakeBet();

                martinB.DisplayInformation(gamesMustPlay);
                titanB.DisplayInformation(gamesMustPlay);
                beginB.DisplayInformation(gamesMustPlay);
            }

            Console.WriteLine($"\nThe cash balance of the tableB is {tableB.CashBalance}$.");

            Console.ReadKey();
        }
    }
}