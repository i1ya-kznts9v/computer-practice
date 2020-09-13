using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_2_Simple_Hierarchy_Inheritor_Classes;

namespace Task_2_Simple_Hierarchy_Unit_Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void T_34_85_Test()
        {
            T_34_85_Tank t_34_85 = new T_34_85_Tank();
            t_34_85.InformationInitializer();

            Assert.AreEqual("T-34-85", t_34_85.TankName);

            Assert.AreEqual("USSR", t_34_85.Nation);
            Assert.AreEqual("1944", t_34_85.IssueYear);
            Assert.AreEqual("Medium tank", t_34_85.TankClass);
            Assert.AreEqual(5, t_34_85.Crew);

            Assert.AreEqual(45, t_34_85.ForeheadBodyArmorThickness);
            Assert.AreEqual(45, t_34_85.BoardBodyArmorThickness);
            Assert.AreEqual(45, t_34_85.BacksideBodyArmorThickness);

            Assert.AreEqual(90, t_34_85.ForeheadTowerArmorThickness);
            Assert.AreEqual(75, t_34_85.BoardTowerArmorThickness);
            Assert.AreEqual(52, t_34_85.BacksideTowerArmorThickness);

            Assert.AreEqual(32.2, t_34_85.Mass);
            Assert.AreEqual(500, t_34_85.EnginePower);

            Assert.AreEqual(54, t_34_85.MaxForwardSpeed);
            Assert.AreEqual(8, t_34_85.MaxBackSpeed);

            Assert.AreEqual("ZiS-S-53 (85 mm)", t_34_85.MainGun);
            Assert.AreEqual(60, t_34_85.MainGunAmmunition);
            Assert.AreEqual(8.5, t_34_85.MainGunReloading);

            Assert.AreEqual("DT (7,62 mm)", t_34_85.MachineGun);
            Assert.AreEqual(1890, t_34_85.MachineGunAmmunition);
            Assert.AreEqual(9.2, t_34_85.MachineGunReloading);

            string displayInformation = t_34_85.DisplayInformation();

            Assert.AreEqual($"T-34-85\n" + "General information:\n\n" + $"Nation: USSR\nIssue Year: 1944\nClass of tank: Medium tank\nCrew: 5 persons\n\n" +
                $"Body armor thickness (forehead/board/backside): 45 mm./45 mm./45 mm.\n" +
                $"Tower armor thickness (forehead/board/backside): 90 mm./75 mm./52 mm.\n\n" +
                "Mobility information:\n\n" + $"Mass: 32,2 tons\nEngine power: 500 HP\n\nMax forward speed: 54 km./h.\nMax back speed: 8 km./h.\n\n" +
                "Armament information:\n\n" + $"Main gun name: ZiS-S-53 (85 mm)\nMain gun ammunition: 60 shells\nMain gun reloading time: 8,5 sec.\n\n" +
                $"Machine gun name: DT (7,62 mm)\nMachine gun ammunition: 1890 shells\nMachine gun reloading time: 9,2 sec.\n\n" +
                "------------------------------------------------------------------------------------------------------------------------", displayInformation);
        }

        [TestMethod]
        public void Churchill_VII_Test()
        {
            Churchill_VII_Tank churchill_VII = new Churchill_VII_Tank();
            churchill_VII.InformationInitializer();

            Assert.AreEqual("Churchill VII", churchill_VII.TankName);

            Assert.AreEqual("Great Britain", churchill_VII.Nation);
            Assert.AreEqual("1942", churchill_VII.IssueYear);
            Assert.AreEqual("Heavy tank", churchill_VII.TankClass);
            Assert.AreEqual(5, churchill_VII.Crew);

            Assert.AreEqual(152, churchill_VII.ForeheadBodyArmorThickness);
            Assert.AreEqual(95, churchill_VII.BoardBodyArmorThickness);
            Assert.AreEqual(51, churchill_VII.BacksideBodyArmorThickness);

            Assert.AreEqual(152, churchill_VII.ForeheadTowerArmorThickness);
            Assert.AreEqual(95, churchill_VII.BoardTowerArmorThickness);
            Assert.AreEqual(95, churchill_VII.BacksideTowerArmorThickness);

            Assert.AreEqual(41.5, churchill_VII.Mass);
            Assert.AreEqual(350, churchill_VII.EnginePower);

            Assert.AreEqual(20, churchill_VII.MaxForwardSpeed);
            Assert.AreEqual(2, churchill_VII.MaxBackSpeed);

            Assert.AreEqual("Ordnance QF Mk.V (75 mm)", churchill_VII.MainGun);
            Assert.AreEqual(84, churchill_VII.MainGunAmmunition);
            Assert.AreEqual(5.7, churchill_VII.MainGunReloading);

            Assert.AreEqual("BESA (7,92 mm)", churchill_VII.MachineGun);
            Assert.AreEqual(9350, churchill_VII.MachineGunAmmunition);
            Assert.AreEqual(9.2, churchill_VII.MachineGunReloading);

            string displayInformation = churchill_VII.DisplayInformation();

            Assert.AreEqual($"Churchill VII\n" + "General information:\n\n" + $"Nation: Great Britain\nIssue Year: 1942\nClass of tank: Heavy tank\nCrew: 5 persons\n\n" +
                $"Body armor thickness (forehead/board/backside): 152 mm./95 mm./51 mm.\n" +
                $"Tower armor thickness (forehead/board/backside): 152 mm./95 mm./95 mm.\n\n" +
                "Mobility information:\n\n" + $"Mass: 41,5 tons\nEngine power: 350 HP\n\nMax forward speed: 20 km./h.\nMax back speed: 2 km./h.\n\n" +
                "Armament information:\n\n" + $"Main gun name: Ordnance QF Mk.V (75 mm)\nMain gun ammunition: 84 shells\nMain gun reloading time: 5,7 sec.\n\n" +
                $"Machine gun name: BESA (7,92 mm)\nMachine gun ammunition: 9350 shells\nMachine gun reloading time: 9,2 sec.\n\n" +
                "------------------------------------------------------------------------------------------------------------------------", displayInformation);
        }

        [TestMethod]
        public void M24_Test()
        {
            M24_Tank m24_Chaffie = new M24_Tank();
            m24_Chaffie.InformationInitializer();

            Assert.AreEqual("M24 Chaffee", m24_Chaffie.TankName);

            Assert.AreEqual("USA", m24_Chaffie.Nation);
            Assert.AreEqual("1943", m24_Chaffie.IssueYear);
            Assert.AreEqual("Light tank", m24_Chaffie.TankClass);
            Assert.AreEqual(5, m24_Chaffie.Crew);

            Assert.AreEqual(25, m24_Chaffie.ForeheadBodyArmorThickness);
            Assert.AreEqual(12, m24_Chaffie.BoardBodyArmorThickness);
            Assert.AreEqual(0, m24_Chaffie.BacksideBodyArmorThickness);

            Assert.AreEqual(38, m24_Chaffie.ForeheadTowerArmorThickness);
            Assert.AreEqual(25, m24_Chaffie.BoardTowerArmorThickness);
            Assert.AreEqual(25, m24_Chaffie.BacksideTowerArmorThickness);

            Assert.AreEqual(18.4, m24_Chaffie.Mass);
            Assert.AreEqual(296, m24_Chaffie.EnginePower);

            Assert.AreEqual(56, m24_Chaffie.MaxForwardSpeed);
            Assert.AreEqual(23, m24_Chaffie.MaxBackSpeed);

            Assert.AreEqual("M6 (75 mm)", m24_Chaffie.MainGun);
            Assert.AreEqual(648, m24_Chaffie.MainGunAmmunition);
            Assert.AreEqual(7.1, m24_Chaffie.MainGunReloading);

            Assert.AreEqual("Browning M2 (12,7 mm)", m24_Chaffie.MachineGun);
            Assert.AreEqual(1800, m24_Chaffie.MachineGunAmmunition);
            Assert.AreEqual(9.2, m24_Chaffie.MachineGunReloading);

            string displayInformation = m24_Chaffie.DisplayInformation();

            Assert.AreEqual($"M24 Chaffee\n" + "General information:\n\n" + $"Nation: USA\nIssue Year: 1943\nClass of tank: Light tank\nCrew: 5 persons\n\n" +
                $"Body armor thickness (forehead/board/backside): 25 mm./12 mm./0 mm.\n" +
                $"Tower armor thickness (forehead/board/backside): 38 mm./25 mm./25 mm.\n\n" +
                "Mobility information:\n\n" + $"Mass: 18,4 tons\nEngine power: 296 HP\n\nMax forward speed: 56 km./h.\nMax back speed: 23 km./h.\n\n" +
                "Armament information:\n\n" + $"Main gun name: M6 (75 mm)\nMain gun ammunition: 648 shells\nMain gun reloading time: 7,1 sec.\n\n" +
                $"Machine gun name: Browning M2 (12,7 mm)\nMachine gun ammunition: 1800 shells\nMachine gun reloading time: 9,2 sec.\n\n" +
                "------------------------------------------------------------------------------------------------------------------------", displayInformation);
        }
    }
}
