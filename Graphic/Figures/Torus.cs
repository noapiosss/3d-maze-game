using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Graphic.Primitives;

namespace maze.Graphic.Figures
{
    public class Torus
    {
        public Vector3 Center { get; set; }
        public float Radius { get; set; }
        public float TubeRadius { get; set; }
        public ConsoleColor Color { get; set; }
        public Point[] Points { get; set; }

        public Torus(Vector3 center, float radius, float tubeRadius, ConsoleColor color = ConsoleColor.White)
        {
            Center = center;
            Radius = radius;
            TubeRadius = tubeRadius;
            Color = color;

            GeneratePoints();
        }

        private void GeneratePoints()
        {
            Points = Array.Empty<Point>();
            List<Point> points = new();

            for (float u = 0; u < Center.X + Radius; u += 0.05f)
            {
                for (float v = 0; v < Center.Y + Radius; v += 0.05f)
                {
                    points.Add(new(
                        new((float)((Radius + (TubeRadius * Math.Cos(v))) * Math.Cos(u)) + Center.X,
                            (float)((Radius + (TubeRadius * Math.Cos(v))) * Math.Sin(u)) + Center.Y,
                            (float)(TubeRadius * Math.Sin(v)) + Center.Z),
                        new((float)(Math.Cos(v) * Math.Cos(u)), (float)(Math.Cos(v) * Math.Sin(u)), (float)Math.Sin(v)),
                        Color
                    ));
                }
            }

            Points = points.ToArray();
        }
    }
}