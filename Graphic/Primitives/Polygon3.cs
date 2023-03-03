using System;
using System.Collections.Generic;
using System.Numerics;

namespace maze.Graphic.Primitives
{
    public class Polygon3
    {
        public Vector3 A { get; set; }
        public Vector3 B { get; set; }
        public Vector3 C { get; set; }
        public Vector3 Normal { get; set; }
        public ConsoleColor Color { get; set; }
        public Point[] Points { get; set; }

        public Polygon3(Vector3 a, Vector3 b, Vector3 c, ConsoleColor color = ConsoleColor.White)
        {
            A = a;
            B = b;
            C = c;
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

            Line AB = new(A, B, Normal, Color);

            foreach (Point C1 in AB.Points)
            {
                Line CC1 = new(C, C1.Position, Normal, Color);
                points.AddRange(CC1.Points);
            }

            Points = points.ToArray();
        }
    }
}