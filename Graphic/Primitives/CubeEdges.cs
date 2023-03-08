using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Primitives
{
    public class CubeEdges : Primitive
    {
        public CubeEdges(Vector3 pivot, float sideLength, ConsoleColor color)
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

            foreach ((int, int) bipol in Lines)
            {
                Line line = new(GlobalVertices[bipol.Item1], GlobalVertices[bipol.Item2], Color);

                projections.AddRange(line.Project(screen, light));
            }

            return projections;
        }
    }
}