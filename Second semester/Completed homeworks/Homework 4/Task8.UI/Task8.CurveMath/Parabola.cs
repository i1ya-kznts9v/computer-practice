using System;
using System.Collections.Generic;

namespace Task8.CurveMath
{
    public class Parabola : Curve
    {
        public Parabola(float p, float defenitionArea) :
            base("Parabola", "y^2 = " + p + "x", defenitionArea) // p is already multiplied by 2
        {
            P = p;
        }

        public float P { get; set; }

        public override List<float> FindValue(float x)
        {
            List<float> value = new List<float>();

            float y = (float)Math.Sqrt(P * x); // P is already multiplied by 2

            if (y == 0)
            {
                value.Add(y);
            }

            if (y != 0)
            {
                value.Add(y);
                value.Add(-y);
            }

            return (value);
        }
    }
}
