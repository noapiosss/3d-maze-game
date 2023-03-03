using System;
using System.Collections.Generic;
using System.Numerics;

namespace maze.Graphic.Primitives
{
    public class Polygon4
    {
        public Vector3 A { get; set; }
        public Vector3 B { get; set; }
        public Vector3 C { get; set; }
        public Vector3 D { get; set; }
        public Vector3 Normal { get; set; }
        public ConsoleColor Color { get; set; }
        public Point[] Points { get; set; }

        public Polygon4(Vector3 a, Vector3 b, Vector3 c, Vector3 d, ConsoleColor color = ConsoleColor.White)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            Color = color;

            CalculateNormal();
            GeneratePoints();
        }

        private void CalculateNormal()
        {
            Vector3 direction = Vector3.Cross(B - A, C - A);
            Normal = Vector3.Normalize(direction);
        }

        private void GeneratePoints()
        {
            Points = Array.Empty<Point>();
            List<Point> points = new();

            Polygon3 ABC = new(A, B, C, Color);
            Polygon3 ACD = new(A, C, D, Color);
            points.AddRange(ABC.Points);
            points.AddRange(ACD.Points);

            Points = points.ToArray();
        }
    }
}