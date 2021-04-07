using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_1_Filters;
using static Task_1_Filters.Filters;

namespace Task_1_Filters_Unit_Test
{
    [TestClass]
    public class UnitTest
    {
        private static Bitmap toyotaSupra = UnitTestPictures.Toyota_Supra;
        private static Bitmap toyotaSupraGrey = UnitTestPictures.Toyota_Supra_Grey_C;
        private static Bitmap toyotaSupraMedian3x3 = UnitTestPictures.Toyota_Supra_Median3x3_C;
        private static Bitmap toyotaSupraAverage3x3 = UnitTestPictures.Toyota_Supra_Average3x3_C;
        private static Bitmap toyotaSupraGauss3x3 = UnitTestPictures.Toyota_Supra_Gauss3x3_C;
        private static Bitmap toyotaSupraSobelX = UnitTestPictures.Toyota_Supra_SobelX_C;
        private static Bitmap toyotaSupraSobelY = UnitTestPictures.Toyota_Supra_SobelY_C;

        private static uint height = (uint) toyotaSupra.Height;
        private static uint width = (uint) toyotaSupra.Width;

        private Color<byte>[,] pixelsTestingPicture = new Color<byte>[height, width];

        private void TestingPictureInitialize()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color<byte> color = new Color<byte>();
                    color = ConvertStructColor(toyotaSupra.GetPixel(j, i), color);

                    pixelsTestingPicture[i, j] = color;
                }
            }
        }

        private Color<byte> ConvertStructColor(Color struct1, Color<byte> struct2)
        {
            struct2.red = struct1.R;
            struct2.green = struct1.G;
            struct2.blue = struct1.B;

            return(struct2);
        }

        private Color<byte>[,] pixelsFiltredPictures = new Color<byte>[height, width];

        private void FiltredPicturesInitialize(string pictureType)
        {
            switch (pictureType)
            {
                case "Grey":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraGrey.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
                case "Median3x3":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraMedian3x3.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
                case "Average3x3":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraAverage3x3.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
                case "Gauss3x3":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraGauss3x3.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
                case "SobelX":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraSobelX.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
                case "SobelY":
                    {
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color<byte> color = new Color<byte>();
                                color = ConvertStructColor(toyotaSupraSobelY.GetPixel(j, i), color);

                                pixelsFiltredPictures[i, j] = color;
                            }
                        }

                        break;
                    }
            }
        }

        [TestMethod]
        public void СomparisonWithGreyFilteredPicture()
        {
            TestingPictureInitialize();
            GreyFilter(pixelsTestingPicture, height, width);
            FiltredPicturesInitialize("Grey");

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }

        [TestMethod]
        public void СomparisonWithMedian3x3FilteredPicture()
        {
            TestingPictureInitialize();
            Median3x3Filter(pixelsTestingPicture, height, width);
            FiltredPicturesInitialize("Median3x3");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }

        [TestMethod]
        public void СomparisonWithAverage3x3FilteredPicture()
        {
            TestingPictureInitialize();
            Average3x3Filter(pixelsTestingPicture, height, width);
            FiltredPicturesInitialize("Average3x3");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }

        [TestMethod]
        public void СomparisonWithGauss3x3FilteredPicture()
        {
            TestingPictureInitialize();
            Gauss3x3Filter(pixelsTestingPicture, height, width);
            FiltredPicturesInitialize("Gauss3x3");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }

        [TestMethod]
        public void СomparisonWithSobelXFilteredPicture()
        {
            TestingPictureInitialize();
            SobelAnyFilter(pixelsTestingPicture, height, width, "SobelX");
            FiltredPicturesInitialize("SobelX");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }

        [TestMethod]
        public void СomparisonWithSobelYFilteredPicture()
        {
            TestingPictureInitialize();
            SobelAnyFilter(pixelsTestingPicture, height, width, "SobelY");
            FiltredPicturesInitialize("SobelY");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(pixelsFiltredPictures[i, j].red, pixelsTestingPicture[i, j].red);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].green, pixelsTestingPicture[i, j].green);
                    Assert.AreEqual(pixelsFiltredPictures[i, j].blue, pixelsTestingPicture[i, j].blue);
                }
            }
        }
    }
}