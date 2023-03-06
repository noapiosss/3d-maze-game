using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Primitives
{
    public class Cube : Primitive
    {
        public Cube(Vector3 center, float sideLength, ConsoleColor color)
        {
            float d = sideLength / 2;
            Vertices = new Vector3[]
            {
                new(center.X - d, center.Y - d, center.Z - d),//0 ---
                new(center.X - d, center.Y - d, center.Z + d),//1 --+
                new(center.X - d, center.Y + d, center.Z - d),//2 -+-
                new(center.X - d, center.Y + d, center.Z + d),//3 -++
                new(center.X + d, center.Y - d, center.Z - d),//4 +--
                new(center.X + d, center.Y - d, center.Z + d),//5 +-+
                new(center.X + d, center.Y + d, center.Z - d),//6 ++-
                new(center.X + d, center.Y + d, center.Z + d),//7 +++
            };

            Indexes = new (int, int, int)[]
            {
                (0,2,4),
                (2,6,4),
                (4,6,5),
                (6,7,5),
                (5,7,1),
                (7,3,1),
                (1,3,0),
                (3,2,0),
                (2,3,6),
                (3,7,6),
                (0,4,1),
                (4,4,1)
            };

            Lines = new (int, int)[]
            {
                (2,3), (3,7), (7,6), (6,2),
                (2,0), (3,1), (7,5), (6,4),
                (0,1), (1,5), (5,4), (4,0)
            };

            Color = color;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            foreach ((int, int, int) index in Indexes)
            {
                Polygon polygon = new(Vertices[index.Item1], Vertices[index.Item2], Vertices[index.Item3], Color);

                projections.AddRange(polygon.Project(screen, light));
            }

            return projections;
        }
    }
}