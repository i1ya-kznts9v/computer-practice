using System;
using System.IO;

namespace Task_2_Simple_Hierarchy_Base_Class
{
    public abstract class Tank
    {
        //Tank name
        public string TankName { get; protected set; }

        //General information
        public string Nation { get; protected set; }
        public string IssueYear { get; protected set; }
        public string TankClass { get; protected set; }
        public int Crew { get; protected set; }

        public int ForeheadBodyArmorThickness { get; protected set; }
        public int BoardBodyArmorThickness { get; protected set; }
        public int BacksideBodyArmorThickness { get; protected set; }

        public int ForeheadTowerArmorThickness { get; protected set; }
        public int BoardTowerArmorThickness { get; protected set; }
        public int BacksideTowerArmorThickness { get; protected set; }

        //Mobility information
        public double Mass { get; protected set; }
        public int EnginePower { get; protected set; }

        public int MaxForwardSpeed { get; protected set; }
        public int MaxBackSpeed { get; protected set; }

        //Armament information
        public string MainGun { get; protected set; }
        public int MainGunAmmunition { get; protected set; }
        public double MainGunReloading { get; protected set; }

        public string MachineGun { get; protected set; }
        public int MachineGunAmmunition { get; protected set; }
        public double MachineGunReloading { get; protected set; }
        public virtual string DisplayInformation()
        {
            string displayInformation = $"{TankName}\n" + "General information:\n\n" + $"Nation: {Nation}\nIssue Year: {IssueYear}\nClass of tank: {TankClass}\nCrew: {Crew} persons\n\n" +
                $"Body armor thickness (forehead/board/backside): {ForeheadBodyArmorThickness} mm./{BoardBodyArmorThickness} mm./{BacksideBodyArmorThickness} mm.\n" +
                $"Tower armor thickness (forehead/board/backside): {ForeheadTowerArmorThickness} mm./{BoardTowerArmorThickness} mm./{BacksideTowerArmorThickness} mm.\n\n" +
                "Mobility information:\n\n" + $"Mass: {Mass} tons\nEngine power: {EnginePower} HP\n\nMax forward speed: {MaxForwardSpeed} km./h.\nMax back speed: {MaxBackSpeed} km./h.\n\n" +
                "Armament information:\n\n" + $"Main gun name: {MainGun}\nMain gun ammunition: {MainGunAmmunition} shells\nMain gun reloading time: {MainGunReloading} sec.\n\n" +
                $"Machine gun name: {MachineGun}\nMachine gun ammunition: {MachineGunAmmunition} shells\nMachine gun reloading time: {MachineGunReloading} sec.\n\n" +
                "------------------------------------------------------------------------------------------------------------------------";

            Console.WriteLine(displayInformation);
            return(displayInformation);
        }
    }
}
