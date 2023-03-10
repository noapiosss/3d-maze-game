using System;
using System.Numerics;

namespace maze.Graphic.Primitives.Helpres
{
    public struct Ellipse
    {
        public float CenterX { get; private set; }
        public float CenterY { get; private set; }
        public float RadiusA { get; private set; }
        public float RadiusB { get; private set; }
        public float Angle { get; private set; }

        public Ellipse(Vector3 W, Vector3 X, Vector3 Y, Vector3 Z)
        {
            float m00 = (X.X * Y.X * Z.Y) - (W.X * Y.X * Z.Y) - (X.X * Y.Y * Z.X) + (W.X * Y.Y * Z.X) - (W.X * X.Y * Z.X) + (W.Y * X.X * Z.X) + (W.X * X.Y * Y.X) - (W.Y * X.X * Y.X);
            float m01 = (W.X * Y.X * Z.Y) - (W.X * X.X * Z.Y) - (X.X * Y.Y * Z.X) + (X.Y * Y.X * Z.X) - (W.Y * Y.X * Z.X) + (W.Y * X.X * Z.X) + (W.X * X.X * Y.Y) - (W.X * X.Y * Y.X);
            float m02 = (X.X * Y.X * Z.Y) - (W.X * X.X * Z.Y) - (W.X * Y.Y * Z.X) - (X.Y * Y.X * Z.X) + (W.Y * Y.X * Z.X) + (W.X * X.Y * Z.X) + (W.X * X.X * Y.Y) - (W.Y * X.X * Y.X);
            float m10 = (X.Y * Y.X * Z.Y) - (W.Y * Y.X * Z.Y) - (W.X * X.Y * Z.Y) + (W.Y * X.X * Z.Y) - (X.Y * Y.Y * Z.X) + (W.Y * Y.Y * Z.X) + (W.X * X.Y * Y.Y) - (W.Y * X.X * Y.Y);
            float m11 = (-X.X * Y.Y * Z.Y) + (W.X * Y.Y * Z.Y) + (X.Y * Y.X * Z.Y) - (W.X * X.Y * Z.Y) - (W.Y * Y.Y * Z.X) + (W.Y * X.Y * Z.X) + (W.Y * X.X * Y.Y) - (W.Y * X.Y * Y.X);
            float m12 = (X.X * Y.Y * Z.Y) - (W.X * Y.Y * Z.Y) + (W.Y * Y.X * Z.Y) - (W.Y * X.X * Z.Y) - (X.Y * Y.Y * Z.X) + (W.Y * X.Y * Z.X) + (W.X * X.Y * Y.Y) - (W.Y * X.Y * Y.X);
            float m20 = (X.X * Z.Y) - (W.X * Z.Y) - (X.Y * Z.X) + (W.Y * Z.X) - (X.X * Y.Y) + (W.X * Y.Y) + (X.Y * Y.X) - (W.Y * Y.X);
            float m21 = (Y.X * Z.Y) - (X.X * Z.Y) - (Y.Y * Z.X) + (X.Y * Z.X) + (W.X * Y.Y) - (W.Y * Y.X) - (W.X * X.Y) + (W.Y * X.X);
            float m22 = (Y.X * Z.Y) - (W.X * Z.Y) - (Y.Y * Z.X) + (W.Y * Z.X) + (X.X * Y.Y) - (X.Y * Y.X) + (W.X * X.Y) - (W.Y * X.X);

            float determinant = (m00 * ((m11 * m22) - (m21 * m12))) - (m01 * ((m10 * m22) - (m12 * m20))) + (m02 * ((m10 * m21) - (m11 * m20)));

            float J = ((m11 * m22) - (m21 * m12)) / determinant;
            float K = -((m01 * m22) - (m02 * m21)) / determinant;
            float L = ((m01 * m12) - (m02 * m11)) / determinant;
            float M = -((m10 * m22) - (m12 * m20)) / determinant;
            float N = ((m00 * m22) - (m02 * m20)) / determinant;
            float O = -((m00 * m12) - (m10 * m02)) / determinant;
            float P = ((m10 * m21) - (m20 * m11)) / determinant;
            float Q = -((m00 * m21) - (m20 * m01)) / determinant;
            float R = ((m00 * m11) - (m10 * m01)) / determinant;

            float a = (J * J) + (M * M) - (P * P);
            float b = (J * K) + (M * N) - (P * Q);
            float c = (K * K) + (N * N) - (Q * Q);
            float d = (J * L) + (M * O) - (P * R);
            float f = (K * L) + (N * O) - (Q * R);
            float g = (L * L) + (O * O) - (R * R);

            float centerX = ((c * d) - (b * f)) / ((b * b) - (a * c));
            float centerY = ((a * f) - (b * d)) / ((b * b) - (a * c));

            float radiusA = (float)Math.Sqrt(2 * ((a * f * f) + (c * d * d) + (g * b * b) - (2 * b * d * f) - (a * c * g)) / (((b * b) - (a * c)) * (Math.Sqrt(((a - c) * (a - c)) + (4 * b * b)) - (a + c))));
            float radiusB = (float)Math.Sqrt(2 * ((a * f * f) + (c * d * d) + (g * b * b) - (2 * b * d * f) - (a * c * g)) / (((b * b) - (a * c)) * (-Math.Sqrt(((a - c) * (a - c)) + (4 * b * b)) - (a + c))));

            float angle = 0;

            if (b == 0 && a <= c)
            {
                angle = 0;
            }
            else if (b == 0 && a >= c)
            {
                angle = (float)(Math.PI / 2);
            }
            else if (b != 0 && a > c)
            {
                angle = (float)((Math.PI / 2) + (0.5 * ((Math.PI / 2) - Math.Atan2(a - c, 2 * b))));
            }
            else if (b != 0 && a <= c)
            {
                angle = (float)((Math.PI / 2) + (0.5 * ((Math.PI / 2) - Math.Atan2(a - c, 2 * b))));
            }

            CenterX = centerX;
            CenterY = centerY;
            RadiusA = radiusA;
            RadiusB = radiusB;
            Angle = angle;
        }
    }
}