using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filters;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Filters : IContractCallback
    {
        static Process server = null;
        ContractClientBase client = null;
        static Converter converter = null;
        static AutoResetEvent waitHandler = null;

        static byte[] toyotaSupra = null;
        static int height = 0;
        static int width = 0;
        Color<byte>[,] actual = null;
        List<string> filters = null;
        bool successfully = false;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            server = new Process();
            string path = Path.GetFullPath(Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\")), @"Server\bin\Debug\Server.exe"));
            server.StartInfo.FileName = path;
            server.StartInfo.UseShellExecute = true;
            server.Start();

            converter = new Converter();
            waitHandler = new AutoResetEvent(false);

            Bitmap toyotaSupraBitmap = Resources.Toyota_Supra;
            height = toyotaSupraBitmap.Height;
            width = toyotaSupraBitmap.Width;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                toyotaSupraBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                toyotaSupra = memoryStream.GetBuffer();
            }
            toyotaSupraBitmap.Dispose();
        }

        [TestInitialize]
        public void Start()
        {
            client = new ContractClientBase(new System.ServiceModel.InstanceContext(this));
            filters = client.GetAvailableFilters();
        }

        [TestMethod]
        public void Grey()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[0], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap grey = Resources.Grey;
            Color<byte>[,] expected =  converter.ToTwoDimArray(grey);
            grey.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        [TestMethod]
        public void Median3x3()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[1], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap median3x3 = Resources.Median3x3;
            Color<byte>[,] expected = converter.ToTwoDimArray(median3x3);
            median3x3.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        [TestMethod]
        public void Average3x3()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[2], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap average3x3 = Resources.Average3x3;
            Color<byte>[,] expected = converter.ToTwoDimArray(average3x3);
            average3x3.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        [TestMethod]
        public void Gauss3x3()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[3], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap gauss3x3 = Resources.Gauss3x3;
            Color<byte>[,] expected = converter.ToTwoDimArray(gauss3x3);
            gauss3x3.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        [TestMethod]
        public void SobelX()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[4], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap sobelX = Resources.SobelX;
            Color<byte>[,] expected = converter.ToTwoDimArray(sobelX);
            sobelX.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        [TestMethod]
        public void SobelY()
        {
            Assert.IsTrue(new List<string> { "Grey", "Median3x3", "Average3x3", "Gauss3x3", "SobelX", "SobelY" }.SequenceEqual(filters));

            client.ApplyFilter(filters[5], toyotaSupra);
            waitHandler.WaitOne(60000);

            Assert.IsTrue(successfully);
            Assert.IsFalse(actual == null);

            Bitmap sobelY = Resources.SobelY;
            Color<byte>[,] expected = converter.ToTwoDimArray(sobelY);
            sobelY.Dispose();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j].Red, actual[i, j].Red);
                    Assert.AreEqual(expected[i, j].Green, actual[i, j].Green);
                    Assert.AreEqual(expected[i, j].Blue, actual[i, j].Blue);
                }
            }
        }

        public void ImageCallback(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                actual = converter.ToTwoDimArray((Bitmap)Image.FromStream(memoryStream));
            }
            bytes = null;

            waitHandler.Set();
        }

        public void ProgressCallback(int progress)
        {
            if(progress == 100)
            {
                successfully = true;
            }

            return;
        }

        [TestCleanup]
        public void End()
        {
            client.Abort();
            actual = null;
            filters = null;
            successfully = false;
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            server.Kill();
            waitHandler.Dispose();
        }
    }
}