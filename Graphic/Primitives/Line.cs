using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

namespace maze.Graphic.Primitives
{
    public class Line : Primitive
    {
        public Line(Vector3 a, Vector3 b, ConsoleColor color)
        {
            Vertices = new Vector3[] { a, b };
            Color = color;
            Normal = Vector3.Zero;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            return ProjectLine(Vertices[0], Vertices[1], screen, light);
        }

        private ICollection<ProjectedVertice> ProjectLine(Vector3 v1, Vector3 v2, Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Vector3 a = v1.RotationInOZ(screen);
            Vector3 b = v2.RotationInOZ(screen);

            float x1 = a.X * screen.FocalDistance / a.Z;
            float y1 = a.Y * screen.FocalDistance / a.Z;

            float x2 = b.X * screen.FocalDistance / b.Z;
            float y2 = b.Y * screen.FocalDistance / b.Z;

            float xMin = Math.Min(x1, x2);
            float xMax = Math.Max(x1, x2);

            float yMin = Math.Min(y1, y2);
            float yMax = Math.Max(y1, y2);

            for (float x = xMin; x <= xMax; ++x)
            {
                float y = ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;

                Vector3 origin = Vector3Extensions.LinePlaneIntersection(
                        Vector3.Zero,
                        new(x, y, screen.FocalDistance),
                        Vector3Extensions.GetAnyLineNormal(a, b),
                        a
                );

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, origin, screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            for (float y = yMin; y <= yMax; ++y)
            {
                float x = ((y - y1) * (x2 - x1) / (y2 - y1)) + x1;

                Vector3 origin = Vector3Extensions.LinePlaneIntersection(
                        Vector3.Zero,
                        new(x, y, screen.FocalDistance),
                        Vector3Extensions.GetAnyLineNormal(a, b),
                        a
                );

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, origin, screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            return projections;
        }
    }
}