using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Task8.CurveMath;
using Ellipse = Task8.CurveMath.Ellipse;

namespace Task8.WPF
{
    public partial class CurvesWPF : Window
    {
        private float imageHeight;
        private float imageWidth;
        private int pointsInDivision;

        public CurvesWPF()
        {
            InitializeComponent();

            imageHeight = (float)ImageCanvas.Height / 2;
            imageWidth = (float)ImageCanvas.Width / 2;

            Curve[] curveList = new Curve[]
            {
                new Parabola(1F, imageWidth),
                new Hyperbola(1F, 1F, imageWidth),
                new Ellipse(1F, 1F, imageWidth)
            };

            CurvesList.Items.Add(curveList[0]);
            CurvesList.Items.Add(curveList[1]);
            CurvesList.Items.Add(curveList[2]);
        }

        private void BuildFunctionButton_Click(object sender, RoutedEventArgs e)
        {
            Curve curve = (Curve)CurvesList.SelectedItem;
            float scaleLevel = (float)(Convert.ToDouble(ScaleLevelLabel.Content));
            float step;
            //float step = (float)(Math.Round(Math.Abs(((0.15417841062432 * (Math.Pow(scaleLevel, 4)) - 1.821464297134164 * (Math.Pow(scaleLevel, 3)) + 7.754022953650099 * (Math.Pow(scaleLevel, 2)) - 14.68451862813908 * scaleLevel + 11.39271767973348))), 1));

            ImageCanvas.Children.Clear();

            if (scaleLevel == 0.1F)
            {
                step = 10F;
            }
            else if (scaleLevel < 0.7F)
            {
                step = 5F;
            }
            else if (scaleLevel < 2F)
            {
                step = 1F;
            }
            else if (scaleLevel < 3F)
            {
                step = 0.5F;
            }
            else
            {
                step = 0.2F;
            }

            pointsInDivision = (int)(imageWidth / 8 * scaleLevel);
            BuildCoordinateSystem(step);

            if (curve != null)
            {
                BuildFunction(curve);
            }
        }

        private void ToPointCollection(List<PointF>[] points, ref PointCollection firstQuarterPoints, ref PointCollection secondQuarterPoints,
            ref PointCollection thirdQuarterPoints, ref PointCollection fourthQuarterPoints)
        {
            foreach (var point in points[0])
            {
                if (point.Y <= imageHeight)
                {
                    firstQuarterPoints.Add(new System.Windows.Point(point.X + imageWidth, imageHeight - point.Y));
                }
            }

            foreach (var point in points[1])
            {
                if (point.Y <= imageHeight)
                {
                    secondQuarterPoints.Add(new System.Windows.Point(point.X + imageWidth, imageHeight - point.Y));
                }
            }

            foreach (var point in points[2])
            {
                if (point.Y >= -imageHeight)
                {
                    thirdQuarterPoints.Add(new System.Windows.Point(point.X + imageWidth, imageHeight - point.Y));
                }
            }

            foreach (var point in points[3])
            {
                if (point.Y >= -imageHeight)
                {
                    fourthQuarterPoints.Add(new System.Windows.Point(point.X + imageWidth, imageHeight - point.Y));
                }
            }
        }

        private void BuildFunction(Curve curve)
        {
            List<PointF>[] points = new List<PointF>[4];
            points = curve.IdentifyPoints(pointsInDivision);

            PointCollection firstQuarterPoints = new PointCollection();
            PointCollection secondQuarterPoints = new PointCollection();
            PointCollection thirdQuarterPoints = new PointCollection();
            PointCollection fourthQuarterPoints = new PointCollection();
            ToPointCollection(points, ref firstQuarterPoints, ref secondQuarterPoints, ref thirdQuarterPoints, ref fourthQuarterPoints);

            Polyline firstQuarterDrawer = new Polyline();
            firstQuarterDrawer.Stroke = System.Windows.Media.Brushes.Red;
            firstQuarterDrawer.Points = firstQuarterPoints;
            ImageCanvas.Children.Add(firstQuarterDrawer);

            Polyline secondQuarterDrawer = new Polyline();
            secondQuarterDrawer.Stroke = System.Windows.Media.Brushes.Red;
            secondQuarterDrawer.Points = secondQuarterPoints;
            ImageCanvas.Children.Add(secondQuarterDrawer);

            Polyline thirdQuarterDrawer = new Polyline();
            thirdQuarterDrawer.Stroke = System.Windows.Media.Brushes.Red;
            thirdQuarterDrawer.Points = thirdQuarterPoints;
            ImageCanvas.Children.Add(thirdQuarterDrawer);

            Polyline fourthQuarterDrawer = new Polyline();
            fourthQuarterDrawer.Stroke = System.Windows.Media.Brushes.Red;
            fourthQuarterDrawer.Points = fourthQuarterPoints;
            ImageCanvas.Children.Add(fourthQuarterDrawer);
        }

        private void BuildColorLine(float x1, float y1, float x2, float y2, SolidColorBrush color)
        {
            Line line = new Line();

            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            line.StrokeThickness = 2;
            line.Stroke = color;

            ImageCanvas.Children.Add(line);
        }

        private void BuildCoordinateSystem(float step)
        {
            BuildColorLine(0, imageHeight, imageWidth * 2, imageHeight, System.Windows.Media.Brushes.Black);
            BuildColorLine(imageWidth, 0, imageWidth, imageHeight * 2, System.Windows.Media.Brushes.Black);
            BuildColorLine(imageWidth * 2, imageHeight, imageWidth * 2 - 5, imageHeight - 5, System.Windows.Media.Brushes.Black);
            BuildColorLine(imageWidth * 2, imageHeight, imageWidth * 2 - 5, imageHeight + 5, System.Windows.Media.Brushes.Black);
            BuildColorLine(imageWidth, 0, imageWidth - 5, 5, System.Windows.Media.Brushes.Black);
            BuildColorLine(imageWidth, 0, imageWidth + 5, 5, System.Windows.Media.Brushes.Black);

            for (float x = pointsInDivision * step; x < imageWidth - 5; x += pointsInDivision * step)
            {
                BuildColorLine(x + imageWidth, imageHeight - 5, x + imageWidth, imageHeight + 5, System.Windows.Media.Brushes.Red);
                BuildNumberOnAxisOX(x / pointsInDivision);
            }

            for (float x = -pointsInDivision * step; x > -imageWidth; x -= pointsInDivision * step)
            {
                BuildColorLine(x + imageWidth, imageHeight - 5, x + imageWidth, imageHeight + 5, System.Windows.Media.Brushes.Red);
                BuildNumberOnAxisOX(x / pointsInDivision);
            }

            for (float y = -pointsInDivision * step; y > -imageHeight + 5; y -= pointsInDivision * step)
            {
                BuildColorLine(imageWidth - 5, y + imageHeight, imageWidth + 5, y + imageHeight, System.Windows.Media.Brushes.Red);
                BuildNumberOnAxisOY(y / pointsInDivision);
            }

            for (float y = pointsInDivision * step; y < imageHeight; y += pointsInDivision * step)
            {
                BuildColorLine(imageWidth - 5, y + imageHeight, imageWidth + 5, y + imageHeight, System.Windows.Media.Brushes.Red);
                BuildNumberOnAxisOY(y / pointsInDivision);
            }

            System.Windows.Shapes.Ellipse originPoint = new System.Windows.Shapes.Ellipse();
            originPoint.Width = 9;
            originPoint.Height = 9;
            originPoint.Fill = System.Windows.Media.Brushes.Black;
            originPoint.Margin = new Thickness(imageWidth - 4.5, imageHeight - 4.5, imageWidth + 4.5, imageHeight + 4.5);
            ImageCanvas.Children.Add(originPoint);

            TextBlock zeroSpace = new TextBlock();
            zeroSpace.Text = "0";
            zeroSpace.FontSize = 10;
            zeroSpace.Margin = new Thickness(imageWidth - 9, imageHeight + 2, imageWidth + 9, imageHeight - 2);
            ImageCanvas.Children.Add(zeroSpace);
        }

        private void BuildNumberOnAxisOX(float number)
        {
            number = (float)Math.Round(number, 1);

            TextBlock numberSpace = new TextBlock();
            numberSpace.Text = number.ToString();
            numberSpace.FontSize = 10;
            numberSpace.Margin = new Thickness(imageWidth + number * pointsInDivision - 4, imageHeight + 5, imageWidth - number * pointsInDivision + 5, imageHeight - 5);
            ImageCanvas.Children.Add(numberSpace);
        }

        private void BuildNumberOnAxisOY(float number)
        {
            number = (float)Math.Round(number, 1);

            TextBlock numberSpace = new TextBlock();
            numberSpace.Text = (-number).ToString();
            numberSpace.FontSize = 10;
            numberSpace.Margin = new Thickness(imageWidth + 8, imageHeight + number * pointsInDivision - 8, imageWidth - 8, imageHeight - number * pointsInDivision + 8);
            ImageCanvas.Children.Add(numberSpace);
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            float newScaleLevel = (float)Math.Round((Convert.ToDouble(ScaleLevelLabel.Content)) + (float)(Convert.ToDouble(((Button)(sender)).Content)), 1);

            if (newScaleLevel > 0F && newScaleLevel < 5.3F)
            {
                ScaleLevelLabel.Content = newScaleLevel.ToString();
            }
        }

        private void ScaleDefaultButton_Click(object sender, EventArgs e)
        {
            float newScaleLevel = 1F;

            ScaleLevelLabel.Content = newScaleLevel.ToString();
        }
    }
}
