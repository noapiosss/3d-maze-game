using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base;

namespace maze.Graphic.Primitives
{
    public class Vertice : Primitive
    {
        public Vertice(Vector3 position, ConsoleColor color)
        {
            GlobalVertices = new Vector3[] { position };
            Color = color;
            Normal = Vector3.Zero;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Vector3 origin = screen.View(GlobalVertices[0]);

            float x = origin.X * screen.FocalDistance / origin.Z;
            float y = origin.Y * screen.FocalDistance / origin.Z;

            if (ProjectedVerticeIsInsideScreen((int)x, (int)y, origin, Normal, screen, light, out ProjectedVertice projection))
            {
                projections.Add(projection);
            }

            return projections;
        }
    }
}