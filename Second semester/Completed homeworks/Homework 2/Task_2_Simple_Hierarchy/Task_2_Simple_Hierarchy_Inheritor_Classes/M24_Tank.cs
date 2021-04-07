using Task_2_Simple_Hierarchy_Base_Class;

namespace Task_2_Simple_Hierarchy_Inheritor_Classes
{
    public class M24_Tank : Tank
    {
        public void InformationInitializer()
        {
            //Tank name
            TankName = "M24 Chaffee";

            //General information
            Nation = "USA";
            IssueYear = "1943";
            TankClass = "Light tank";
            Crew = 5;

            ForeheadBodyArmorThickness = 25;
            BoardBodyArmorThickness = 12;
            BacksideBodyArmorThickness = 0;

            ForeheadTowerArmorThickness = 38;
            BoardTowerArmorThickness = 25;
            BacksideTowerArmorThickness = 25;

            //Mobility information
            Mass = 18.4;
            EnginePower = 296;

            MaxForwardSpeed = 56;
            MaxBackSpeed = 23;

            //Armament information
            MainGun = "M6 (75 mm)";
            MainGunAmmunition = 648;
            MainGunReloading = 7.1;

            MachineGun = "Browning M2 (12,7 mm)";
            MachineGunAmmunition = 1800;
            MachineGunReloading = 9.2;
        }

        public override string DisplayInformation()
        {
            return(base.DisplayInformation());
        }
    }
}
