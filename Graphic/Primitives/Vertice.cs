using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

namespace maze.Graphic.Primitives
{
    public class Vertice : Primitive
    {
        public Vertice(Vector3 position, ConsoleColor color)
        {
            Vertices = new Vector3[] { position };
            Color = color;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen)
        {
            List<ProjectedVertice> projections = new();

            Vector3 p = Vertices[0].RotationInOZ(screen);

            float x = p.X * screen.FocalDistance / p.Z;
            float y = p.Y * screen.FocalDistance / p.Z;
            float distance = Vector3.Distance(Vector3.Zero, p);

            if (ProjectedVerticeIsInsideScreen((int)x, (int)y, distance, screen, out ProjectedVertice projection))
            {
                projections.Add(projection);
            }

            return projections;
        }
    }
}