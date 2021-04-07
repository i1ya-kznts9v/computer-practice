using Task_2_Simple_Hierarchy_Base_Class;

namespace Task_2_Simple_Hierarchy_Inheritor_Classes
{
    public class T_34_85_Tank : Tank
    {
        public void InformationInitializer() 
        {
            //Tank name
            TankName = "T-34-85";

            //General information
            Nation = "USSR";
            IssueYear = "1944";
            TankClass = "Medium tank";
            Crew = 5;

            ForeheadBodyArmorThickness = 45;
            BoardBodyArmorThickness = 45;
            BacksideBodyArmorThickness = 45;

            ForeheadTowerArmorThickness = 90;
            BoardTowerArmorThickness = 75;
            BacksideTowerArmorThickness = 52;

            //Mobility information
            Mass = 32.2;
            EnginePower = 500;

            MaxForwardSpeed = 54;
            MaxBackSpeed = 8;

            //Armament information
            MainGun = "ZiS-S-53 (85 mm)";
            MainGunAmmunition = 60;
            MainGunReloading = 8.5;

            MachineGun = "DT (7,62 mm)";
            MachineGunAmmunition = 1890;
            MachineGunReloading = 9.2;
        }

        public override string DisplayInformation()
        {
            return(base.DisplayInformation());
        }
    }
}
