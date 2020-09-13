using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// Found in the NuGet
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;

namespace Task8.UnitTests
{
    [TestClass]
    public class UnitTest // Don't touch your mouse or touchpad during the UnitTests, because it can breaks tests!
    {
        [TestMethod]
        public void WFTest()
        {
            var curveVisio = TestStack.White.Application.Launch("Task8.WF.exe");
            var window = curveVisio.GetWindow("CurveVisio", InitializeOption.NoCache);

            var buildFunctionButton = window.Get<Button>("BuildFunctionButton");
            var scaleLevelLabel = window.Get<Label>("ScaleLevelLabel");
            var scaleUpButton = window.Get<Button>("ScaleUpButton");
            var scaleDownButton = window.Get<Button>("ScaleDownButton");
            var scaleDefaultButton = window.Get<Button>("ScaleDefaultButton");

            for (int i = 0; i < 3; i++)
            {
                ComboBox curvesList = window.Get<ComboBox>("CurvesList");
                curvesList.Select(i);

                buildFunctionButton.Click();
                Assert.AreEqual(1F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleUpButton.Click();
                buildFunctionButton.Click();
                Assert.AreEqual(1.1F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleDefaultButton.Click();

                scaleDownButton.Click();
                buildFunctionButton.Click();
                Assert.AreEqual(0.9F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleDefaultButton.Click();
            }
        }

        [TestMethod]
        public void WPFTest()
        {
            var curveVisio = TestStack.White.Application.Launch("Task8.WPF.exe");
            var window = curveVisio.GetWindow("CurveVisio", InitializeOption.NoCache);

            var buildFunctionButton = window.Get<Button>("BuildFunctionButton");
            var scaleLevelLabel = window.Get<Label>("ScaleLevelLabel");
            var scaleUpButton = window.Get<Button>("ScaleUpButton");
            var scaleDownButton = window.Get<Button>("ScaleDownButton");
            var scaleDefaultButton = window.Get<Button>("ScaleDefaultButton");

            for (int i = 0; i < 3; i++)
            {
                ComboBox curvesList = window.Get<ComboBox>("CurvesList");
                curvesList.Select(i);

                buildFunctionButton.Click();
                Assert.AreEqual(1F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleUpButton.Click();
                buildFunctionButton.Click();
                Assert.AreEqual(1.1F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleDefaultButton.Click();

                scaleDownButton.Click();
                buildFunctionButton.Click();
                Assert.AreEqual(0.9F, (float)Convert.ToDouble(scaleLevelLabel.Text));

                scaleDefaultButton.Click();
            }
        }
    }
}