using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Graphic.Primitives;

namespace maze.Graphic.Figures
{
    public class Cube
    {
        public Vector3 A1 { get; set; }
        public Vector3 B1 { get; set; }
        public Vector3 C1 { get; set; }
        public Vector3 D1 { get; set; }
        public Vector3 A2 { get; set; }
        public Vector3 B2 { get; set; }
        public Vector3 C2 { get; set; }
        public Vector3 D2 { get; set; }
        public ConsoleColor Color { get; set; }

        public Point[] Points { get; set; }
        public Polygon3[] Polygons { get; set; }

        public Cube(Vector3 a1, Vector3 c2, ConsoleColor color = ConsoleColor.White)
        {
            A1 = new(a1.X, a1.Y, a1.Z);
            B1 = new(a1.X, a1.Y, c2.Z);
            C1 = new(c2.X, a1.Y, c2.Z);
            D1 = new(c2.X, a1.Y, a1.Z);
            A2 = new(a1.X, c2.Y, a1.Z);
            B2 = new(a1.X, c2.Y, c2.Z);
            C2 = new(c2.X, c2.Y, c2.Z);
            D2 = new(c2.X, c2.Y, a1.Z);
            Color = color;

            GeneratePolygons();
            GeneratePoints();
        }

        private void GeneratePolygons()
        {
            List<Polygon3> polygons = new()
            {
                new(B1, D1, A1, Color),
                new(D1, B1, C1, Color),

                new(D2, C2, B2, Color),
                new(A2, D2, B2, Color),

                new(B2, C2, B1, Color),
                new(C2, C1, B1, Color),

                new(C2, D1, C1, Color),
                new(C2, D2, D1, Color),

                new(D2, A1, D1, Color),
                new(D2, A2, A1, Color),

                new(B2, B1, A1, Color),
                new(A2, B2, A1, Color)
            };

            Polygons = polygons.ToArray();
        }

        private void GeneratePoints()
        {
            List<Point> points = new();
            foreach (Polygon3 polygon in Polygons)
            {
                points.AddRange(polygon.Points);
            }
            Points = points.ToArray();
        }

        // private void GeneratePoints()
        // {
        //     List<Point> points = new();

        //     points.AddRange(new Line(A1, B1, Vector3.One, Color).Points);
        //     points.AddRange(new Line(B1, C1, Vector3.One, Color).Points);
        //     points.AddRange(new Line(C1, D1, Vector3.One, Color).Points);
        //     points.AddRange(new Line(D1, A1, Vector3.One, Color).Points);

        //     points.AddRange(new Line(A1, A2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(B1, B2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(C1, C2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(D1, D2, Vector3.One, Color).Points);

        //     points.AddRange(new Line(A2, B2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(B2, C2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(C2, D2, Vector3.One, Color).Points);
        //     points.AddRange(new Line(D2, A2, Vector3.One, Color).Points);

        //     Points = points.ToArray();
        // }
    }
}
