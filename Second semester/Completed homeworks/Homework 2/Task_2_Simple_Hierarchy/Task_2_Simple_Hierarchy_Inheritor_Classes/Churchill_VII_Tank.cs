using Task_2_Simple_Hierarchy_Base_Class;

namespace Task_2_Simple_Hierarchy_Inheritor_Classes
{
    public class Churchill_VII_Tank : Tank
    {
        public void InformationInitializer()
        {
            //Tank name
            TankName = "Churchill VII";

            //General information
            Nation = "Great Britain";
            IssueYear = "1942";
            TankClass = "Heavy tank";
            Crew = 5;

            ForeheadBodyArmorThickness = 152;
            BoardBodyArmorThickness = 95;
            BacksideBodyArmorThickness = 51;

            ForeheadTowerArmorThickness = 152;
            BoardTowerArmorThickness = 95;
            BacksideTowerArmorThickness = 95;

            //Mobility information
            Mass = 41.5;
            EnginePower = 350;

            MaxForwardSpeed = 20;
            MaxBackSpeed = 2;

            //Armament information
            MainGun = "Ordnance QF Mk.V (75 mm)";
            MainGunAmmunition = 84;
            MainGunReloading = 5.7;

            MachineGun = "BESA (7,92 mm)";
            MachineGunAmmunition = 9350;
            MachineGunReloading = 9.2;
        }

        public override string DisplayInformation()
        {
            return(base.DisplayInformation());
        }
    }

}
