using System;
using Unity;
using Unity.Injection;

namespace Task_7_IoC_Сontainer
{
    public class MainPart
    {
        public static IUnityContainer InitializeRouletteIoCContainer()
        {
            IUnityContainer rouletteContainer = new UnityContainer();
            Table table = new Table();

            rouletteContainer.RegisterInstance(table);
            rouletteContainer.RegisterType<Player, MartingaleBot>("MartingaleBot", new InjectionConstructor(typeof(Table)));
            rouletteContainer.RegisterType<Player, TitanicBot>("TitanicBot", new InjectionConstructor(typeof(Table)));
            rouletteContainer.RegisterType<Player, BeginnerBot>("BeginnerBot", new InjectionConstructor(typeof(Table)));

            return (rouletteContainer);
        }

        static void Main() // Everything is the same as in Unit Test, just so quickly check its result.
        {
            Console.WriteLine("“I know only 2 ways to beat roulette: steal chips from the table\n" +
               "or open your own casino and put roulette there,” - Albert Einstein.\nNow we will check these words:\n");

            int gamesMustPlay = 42;

            IUnityContainer rouletteContainer = InitializeRouletteIoCContainer();

            Table table = rouletteContainer.Resolve<Table>();
            MartingaleBot martin = rouletteContainer.Resolve<MartingaleBot>("MartingaleBot");
            TitanicBot titan = rouletteContainer.Resolve<TitanicBot>("TitanicBot");
            BeginnerBot begin = rouletteContainer.Resolve<BeginnerBot>("BeginnerBot");

            Console.WriteLine();

            for (int i = 0; i < gamesMustPlay; i++)
            {
                table.SpinRoulette();

                martin.MakeBet();
                titan.MakeBet();
                begin.MakeBet();

                martin.DisplayInformation(gamesMustPlay);
                titan.DisplayInformation(gamesMustPlay);
                begin.DisplayInformation(gamesMustPlay);
            }

            Console.WriteLine($"\nThe cash balance of the table is {table.cashBalance}$.");

            Console.ReadKey();
        }
    }
}
