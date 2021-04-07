using System;
using static Task_1_Filters.Picture;

namespace Task_1_Filters
{
    public class Filters
    {
        public static void ApplyFilter(string filterType)
        {
            switch(filterType)
            {
                case "Grey":
                    {
                        GreyFilter(Pixels, Height, Width);
                        break;
                    }
                case "Median3x3":
                    {
                        Median3x3Filter(Pixels, Height, Width);
                        break;
                    }
                case "Average3x3":
                    {
                        Average3x3Filter(Pixels, Height, Width);
                        break;
                    }
                case "Gauss3x3":
                    {
                        Gauss3x3Filter(Pixels, Height, Width);
                        break;
                    }
                case "SobelX":
                    {
                        SobelAnyFilter(Pixels, Height, Width, "SobelX");
                        break;
                    }
                case "SobelY":
                    {
                        SobelAnyFilter(Pixels, Height, Width, "SobelY");
                        break;
                    }
                default:
                    {
                        throw new Exception("Invalid filter name.\nTry it again!");
                    }
            }
        }

        public static void GreyFilter(Color<byte>[,] pixels, uint height, uint width)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int newColor = Convolution(pixels, i, j);

                    pixels[i, j].red = (byte)newColor;
                    pixels[i, j].green = (byte)newColor;
                    pixels[i, j].blue = (byte)newColor;
                }
            }
        }

        private static int Convolution(Color<byte>[,] pixels, int i, int j)
        {
            return ((pixels[i, j].red + pixels[i, j].green + pixels[i, j].blue) / 3);
        }

        public static void Median3x3Filter(Color<byte>[,] pixels, uint height, uint width)
        {
            Color<byte>[,] pixelsCopy = new Color<byte>[height, width];
            CopyPixels(pixels, pixelsCopy, height, width);

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    Color<int[]> newPixels = new Color<int[]>
                    {
                        red = new int[9],
                        green = new int[9],
                        blue = new int[9]
                    };

                    newPixels = Convolution(pixelsCopy, i, j, newPixels);

                    Array.Sort(newPixels.red);
                    Array.Sort(newPixels.green);
                    Array.Sort(newPixels.blue);

                    pixels[i, j].red = (byte)(newPixels.red[4]);
                    pixels[i, j].green = (byte)(newPixels.green[4]);
                    pixels[i, j].blue = (byte)(newPixels.blue[4]);
                }
            }
        }

        private static Color<int[]> Convolution(Color<byte>[,] pixelsCopy, int i, int j, Color<int[]> newPixels)
        {
            int l = 0;

            for (int k = -1; k < 2; k++)
            {
                for (int m = -1; m < 2; m++)
                {
                    newPixels.red[l] = pixelsCopy[i + k, j + m].red;
                    newPixels.green[l] = pixelsCopy[i + k, j + m].green;
                    newPixels.blue[l] = pixelsCopy[i + k, j + m].blue;
                    l++;
                }
            }

            return (newPixels);
        }

        public static void Average3x3Filter(Color<byte>[,] pixels, uint height, uint width)
        {
            int[,] matrix = { { 1, 1, 1 },
                              { 1, 1, 1 },
                              { 1, 1, 1 } };

            Color<byte>[,] pixelsCopy = new Color<byte>[height, width];
            CopyPixels(pixels, pixelsCopy, height, width);

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    Color<int> newPixels = new Color<int>();
                    newPixels = Convolution(pixelsCopy, i, j, newPixels, matrix);

                    pixels[i, j].red = (byte)(newPixels.red / 9);
                    pixels[i, j].green = (byte)(newPixels.green / 9);
                    pixels[i, j].blue = (byte)(newPixels.blue / 9);
                }
            }
        }

        public static void Gauss3x3Filter(Color<byte>[,] pixels, uint height, uint width)
        {
            int[,] matrix = { { 1, 2, 1 },
                              { 2, 4, 2 },
                              { 1, 2, 1 } };

            Color<byte>[,] pixelsCopy = new Color<byte>[height, width];
            CopyPixels(pixels, pixelsCopy, height, width);

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    Color<int> newPixels = new Color<int>();
                    newPixels = Convolution(pixelsCopy, i, j, newPixels, matrix);

                    pixels[i, j].red = (byte)(newPixels.red / 16);
                    pixels[i, j].green = (byte)(newPixels.green / 16);
                    pixels[i, j].blue = (byte)(newPixels.blue / 16);
                }
            }
        }

        public static void SobelAnyFilter(Color<byte>[,] pixels, uint height, uint width, string sobelFilterType)
        {
            int[,] matrix = new int[3, 3];

            Color<byte>[,] pixelsCopy = new Color<byte>[height, width];
            CopyPixels(pixels, pixelsCopy, height, width);

            switch (sobelFilterType)
            {
                case "SobelX":
                    {
                        matrix[0, 0] = -1; //{-1, -2, -1}
                        matrix[0, 1] = -2; //{ 0,  0,  0}
                        matrix[0, 2] = -1; //{ 1,  2,  1}
                        matrix[1, 0] = 0;
                        matrix[1, 1] = 0;
                        matrix[1, 2] = 0;
                        matrix[2, 0] = 1;
                        matrix[2, 1] = 2;
                        matrix[2, 2] = 1;

                        break;
                    }
                case "SobelY":
                    {
                        matrix[0, 0] = -1; //{-1, 0, 1}
                        matrix[0, 1] = 0;  //{-2, 0, 2}
                        matrix[0, 2] = 1;  //{-1, 0, 1}
                        matrix[1, 0] = -2;
                        matrix[1, 1] = 0;
                        matrix[1, 2] = 2;
                        matrix[2, 0] = -1;
                        matrix[2, 1] = 0;
                        matrix[2, 2] = 1;

                        break;
                    }
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    Color<int> newPixels = new Color<int>();
                    newPixels = Convolution(pixelsCopy, i, j, newPixels, matrix);

                    if (newPixels.red < 0)
                    {
                        newPixels.red = 0;
                    }
                    else if (newPixels.red > 255)
                    {
                        newPixels.red = 255;
                    }

                    if (newPixels.green < 0)
                    {
                        newPixels.green = 0;
                    }
                    else if (newPixels.green > 255)
                    {
                        newPixels.green = 255;
                    }

                    if (newPixels.blue < 0)
                    {
                        newPixels.blue = 0;
                    }
                    else if (newPixels.blue > 255)
                    {
                        newPixels.blue = 255;
                    }

                    pixels[i, j].red = (byte)(newPixels.red);
                    pixels[i, j].green = (byte)(newPixels.green);
                    pixels[i, j].blue = (byte)(newPixels.blue);
                }
            }
        }

        private static void CopyPixels(Color<byte>[,] pixels, Color<byte>[,] pixelsCopy, uint height, uint width)
        {
            Array.Copy(pixels, pixelsCopy, height * width);
        }

        private static Color<int> Convolution(Color<byte>[,] pixelsCopy, int i, int j, Color<int> newPixels, int[,] matrix)
        {
            for (int k = -1; k < 2; k++)
            {
                for (int m = -1; m < 2; m++)
                {
                    newPixels.red += pixelsCopy[i + k, j + m].red * matrix[k + 1, m + 1];
                    newPixels.green += pixelsCopy[i + k, j + m].green * matrix[k + 1, m + 1];
                    newPixels.blue += pixelsCopy[i + k, j + m].blue * matrix[k + 1, m + 1];
                }
            }

            return (newPixels);
        }
    }
}