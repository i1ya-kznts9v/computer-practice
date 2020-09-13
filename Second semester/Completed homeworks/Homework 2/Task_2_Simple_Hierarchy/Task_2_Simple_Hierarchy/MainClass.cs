using System;
using Task_2_Simple_Hierarchy_Inheritor_Classes;

namespace Task_2_Simple_Hierarchy
{
    class MainClass
    {
        static void Main()
        {
            T_34_85_Tank t_34_85 = new T_34_85_Tank();
            Churchill_VII_Tank churchill_VII = new Churchill_VII_Tank();
            M24_Tank m24_Chaffie = new M24_Tank();

            t_34_85.InformationInitializer();
            churchill_VII.InformationInitializer();
            m24_Chaffie.InformationInitializer();
            
            t_34_85.DisplayInformation();
            churchill_VII.DisplayInformation();
            m24_Chaffie.DisplayInformation();

            Console.ReadKey();
        }
    }
}
