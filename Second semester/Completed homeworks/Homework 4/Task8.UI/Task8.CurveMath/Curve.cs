using System.Collections.Generic;
using System.Drawing;

namespace Task8.CurveMath
{
    public abstract class Curve
    {
        public Curve(string name, string equation, float defenitionArea)
        {
            Name = name;
            Equation = equation;
            DefenitionArea = defenitionArea;
        }

        public string Name { get; protected set; }
        public string Equation { get; protected set; }
        public float DefenitionArea { get; protected set; }
        public abstract List<float> FindValue(float x);

        public List<PointF>[] IdentifyPoints(int pointsPerDivision)
        {
            List<PointF>[] points = new List<PointF>[4];
            List<PointF> firstQuarterPoints = new List<PointF>();
            List<PointF> secondQuarterPoints = new List<PointF>();
            List<PointF> thirdQuarterPoints = new List<PointF>();
            List<PointF> fourthQuarterPoints = new List<PointF>();

            float step = DefenitionArea / (pointsPerDivision * 8000);

            for (float x = -DefenitionArea; x <= DefenitionArea; x += step)
            {
                List<float> value = new List<float>();

                value.AddRange(FindValue(x / pointsPerDivision));
                float X = x;

                foreach (var y in value)
                {
                    float Y = y * pointsPerDivision;

                    if (Y >= 0F && X >= 0F)
                    {
                        firstQuarterPoints.Add(new PointF(X, Y));
                    }

                    if (Y >= 0F && X < 0F)
                    {
                        secondQuarterPoints.Add(new PointF(X, Y));
                    }

                    if (Y < 0F && X < 0F)
                    {
                        thirdQuarterPoints.Add(new PointF(X, Y));
                    }

                    if (Y < 0F && X > 0F)
                    {
                        fourthQuarterPoints.Add(new PointF(X, Y));
                    }
                }
            }

            points[0] = firstQuarterPoints;
            points[1] = secondQuarterPoints;
            points[2] = thirdQuarterPoints;
            points[3] = fourthQuarterPoints;

            return (points);
        }

        public override string ToString()
        {
            return (Equation); 
        }
    }
}
