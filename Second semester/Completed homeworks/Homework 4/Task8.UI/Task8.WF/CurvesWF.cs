using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Task8.CurveMath;

namespace Task8.WF
{
    public partial class CurvesWF : Form
    {
        private Graphics imageBuilder;
        private float imageHeight;
        private float imageWidth;
        private int pointsInDivision;

        public CurvesWF()
        {
            InitializeComponent();

            imageBuilder = ImagePictureBox.CreateGraphics();
            imageWidth = ImagePictureBox.Width / 2;
            imageHeight = ImagePictureBox.Height / 2;
            imageBuilder.TranslateTransform(imageWidth, imageHeight);

            Curve[] curveList = new Curve[]
            {
                new Parabola(1F, imageWidth),
                new Hyperbola(1F, 1F, imageWidth),
                new Ellipse(1F, 1F, imageWidth)
            };

            CurvesList.Items.AddRange(curveList);
        }

        private void BuildFunctionButton_Click(object sender, EventArgs e)
        {
            Curve curve = (Curve)CurvesList.SelectedItem;
            float scaleLevel = (float)(Convert.ToDouble(ScaleLevelLabel.Text));
            float step;
            //float step = (float)(Math.Round(Math.Abs(((0.15417841062432 * (Math.Pow(scaleLevel, 4)) - 1.821464297134164 * (Math.Pow(scaleLevel, 3)) + 7.754022953650099 * (Math.Pow(scaleLevel, 2)) - 14.68451862813908 * scaleLevel + 11.39271767973348))), 1));

            imageBuilder.Clear(ImagePictureBox.BackColor);

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

        private void BuildCoordinateSystem(float step)
        {
            imageBuilder.DrawLine(Pens.Black, -imageWidth, 0, imageWidth - 4, 0);
            imageBuilder.DrawLine(Pens.Black, 0, -imageHeight, 0, imageHeight - 4);

            for (float x = pointsInDivision * step; x < imageWidth - 9; x += pointsInDivision * step)
            {
                imageBuilder.DrawLine(Pens.Red, x, -5, x, 5);
                BuildNumberOnAxisOX(x / pointsInDivision);
            }

            for (float x = -pointsInDivision * step; x > -imageWidth; x -= pointsInDivision * step)
            {
                imageBuilder.DrawLine(Pens.Red, x, -5, x, 5);
                BuildNumberOnAxisOX(x / pointsInDivision);
            }

            imageBuilder.DrawLine(Pens.Black, imageWidth - 4, 0, imageWidth - 9, 5);
            imageBuilder.DrawLine(Pens.Black, imageWidth - 4, 0, imageWidth - 9, -5);

            for (float y = -pointsInDivision * step; y > -imageHeight + 5; y -= pointsInDivision * step)
            {
                imageBuilder.DrawLine(Pens.Red, -5, y, 5, y);
                BuildNumberOnAxisOY(y / pointsInDivision);
            }

            for (float y = pointsInDivision * step; y < imageHeight; y += pointsInDivision * step)
            {
                imageBuilder.DrawLine(Pens.Red, -5, y, 5, y);
                BuildNumberOnAxisOY(y / pointsInDivision);
            }

            imageBuilder.DrawLine(Pens.Black, 0, -imageHeight, -5, -imageHeight + 5);
            imageBuilder.DrawLine(Pens.Black, 0, -imageHeight, 5, -imageHeight + 5);

            imageBuilder.FillEllipse(Brushes.Red, new Rectangle(-3, -3, 6, 6));

            Font font = new Font(Font.FontFamily, 7);
            SizeF numberSize = imageBuilder.MeasureString("0", font);
            RectangleF numberRectangle = new RectangleF(new PointF(-numberSize.Width, 3), numberSize);
            imageBuilder.DrawString("0", font, Brushes.Red, numberRectangle);
        }

        private void BuildNumberOnAxisOX(float number)
        {
            number = (float)Math.Round(number, 1);
            Font font = new Font(Font.FontFamily, 7);
            SizeF numberSize = imageBuilder.MeasureString(number.ToString(), font);
            RectangleF numberRectangle = new RectangleF(new PointF(number * pointsInDivision - numberSize.Width / 2, 7), numberSize);
            imageBuilder.DrawString(number.ToString(), font, Brushes.Red, numberRectangle);
        }

        private void BuildNumberOnAxisOY(float number)
        {
            number = (float)Math.Round(number, 1);
            Font font = new Font(Font.FontFamily, 7);
            SizeF numberSize = imageBuilder.MeasureString(number.ToString(), font);
            RectangleF numberRectangle = new RectangleF(new PointF(-7 - numberSize.Width, -number * pointsInDivision - numberSize.Height / 2), numberSize);
            imageBuilder.DrawString(number.ToString(), font, Brushes.Red, numberRectangle);
        }

        private void BuildFunction(Curve curve)
        {
            List<PointF>[] points = new List<PointF>[4];
            points = curve.IdentifyPoints(pointsInDivision);

            if (points[0].Count != 0)
            {
                imageBuilder.DrawCurve(Pens.Red, points[0].ToArray());
            }

            if (points[1].Count != 0)
            {
                imageBuilder.DrawCurve(Pens.Red, points[1].ToArray());
            }

            if (points[2].Count != 0)
            {
                imageBuilder.DrawCurve(Pens.Red, points[2].ToArray());
            }

            if (points[3].Count != 0)
            {
                imageBuilder.DrawCurve(Pens.Red, points[3].ToArray());
            }
        }

        private void ScaleButton_Click(object sender, EventArgs e)
        {
            float newScaleLevel = (float)Math.Round((Convert.ToDouble(ScaleLevelLabel.Text)) + (float)(Convert.ToDouble(((Button)(sender)).Text)), 1);

            if (newScaleLevel > 0F && newScaleLevel < 5.3F)
            {
                ScaleLevelLabel.Text = newScaleLevel.ToString();
            }
        }

        private void ScaleDefaultButton_Click(object sender, EventArgs e)
        {
            float newScaleLevel = 1F;

            ScaleLevelLabel.Text = newScaleLevel.ToString();
        }
    }
}