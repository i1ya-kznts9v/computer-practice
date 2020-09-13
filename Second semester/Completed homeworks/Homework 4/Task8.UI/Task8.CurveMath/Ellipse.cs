using System;
using System.Collections.Generic;

namespace Task8.CurveMath
{
    public class Ellipse : Curve
    {
        public Ellipse(float a, float b, float defenitionArea) :
            base("Ellipse", "x^2/" + a + " + y^2/" + b + " = 1", defenitionArea) // a and b are already squared
        {
            A = a;
            B = b;
        }

        public float A { get; set; }
        public float B { get; set; }

        public override List<float> FindValue(float x)
        {
            List<float> value = new List<float>();

            float y = (float)Math.Sqrt(B - (B / A) * x * x); // A and B are already squared

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
