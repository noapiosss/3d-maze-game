using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Primitives
{
    public class Cube : Primitive
    {
        public Cube(Vector3 pivot, float sideLength, ConsoleColor color)
        {
            float d = sideLength / 2;

            Pivot = new(pivot, Vector3.UnitZ, Vector3.UnitY, Vector3.UnitX);

            LocalVertices = new Vector3[]
            {
                new(- d, - d, - d),//0 ---
                new(- d, - d, + d),//1 --+
                new(- d, + d, - d),//2 -+-
                new(- d, + d, + d),//3 -++
                new(+ d, - d, - d),//4 +--
                new(+ d, - d, + d),//5 +-+
                new(+ d, + d, - d),//6 ++-
                new(+ d, + d, + d),//7 +++
            };

            GlobalVertices = ToGlobalVertices();

            Polygons = new (int, int, int)[]
            {
                (2,3,7),
                (2,7,6),
                (0,5,1),
                (0,4,5),
                (0,2,6),
                (0,6,4),
                (4,6,7),
                (4,7,5),
                (5,7,3),
                (5,3,1),
                (1,3,2),
                (1,2,0)
            };

            Color = color;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            foreach ((int, int, int) indexes in Polygons)
            {
                Polygon polygon = new(GlobalVertices[indexes.Item1], GlobalVertices[indexes.Item2], GlobalVertices[indexes.Item3], Color);

                projections.AddRange(polygon.Project(screen, light));
            }

            return projections;
        }
    }
}