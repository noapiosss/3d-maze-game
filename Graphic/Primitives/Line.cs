using System;
using System.Collections.Generic;
using System.Numerics;

namespace maze.Graphic.Primitives
{
    public class Line
    {
        public Vector3 A { get; set; }
        public Vector3 B { get; set; }
        public Vector3 Normal { get; set; }
        public ConsoleColor Color { get; set; }
        public Point[] Points { get; set; }

        public Line(Vector3 a, Vector3 b, Vector3 normal, ConsoleColor color = ConsoleColor.White)
        {
            A = a;
            B = b;
            Normal = normal;
            Color = color;

            GeneratePoints();
        }

        private void GeneratePoints()
        {
            Points = Array.Empty<Point>();
            List<Point> points = new();

            for (float i = Math.Min(A.X, B.X); i <= Math.Max(A.X, B.X); i += 0.5f)
            {
                points.Add(new(
                    new(i, ((i - A.X) * (B.Y - A.Y) / (B.X - A.X)) + A.Y, ((i - A.X) * (B.Z - A.Z) / (B.X - A.X)) + A.Z),
                    Normal,
                    Color
                ));
            }

            for (float i = Math.Min(A.Y, B.Y); i < Math.Max(A.Y, B.Y); i += 0.5f)
            {
                points.Add(new(
                    new(((i - A.Y) * (B.X - A.X) / (B.Y - A.Y)) + A.X, i, ((i - A.Y) * (B.Z - A.Z) / (B.Y - A.Y)) + A.Z),
                    Normal,
                    Color
                ));
            }

            for (float i = Math.Min(A.Z, B.Z); i < Math.Max(A.Z, B.Z); i += 0.5f)
            {
                points.Add(new(
                    new(((i - A.Z) * (B.X - A.X) / (B.Z - A.Z)) + A.X, ((i - A.Z) * (B.Y - A.Y) / (B.Z - A.Z)) + A.Y, i),
                    Normal,
                    Color
                ));
            }

            Points = points.ToArray();
        }
    }
}