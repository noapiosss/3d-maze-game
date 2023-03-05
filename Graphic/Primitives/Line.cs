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
        }

        public override ICollection<ProjectedVertice> Project(Screen screen)
        {
            return ProjectLine(Vertices[0], Vertices[1], screen);
        }

        private ICollection<ProjectedVertice> ProjectLine(Vector3 v1, Vector3 v2, Screen screen)
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

                float originX = ((a.Z * b.X) - (a.X * b.Z)) / ((screen.FocalDistance * (b.X - a.X) / x) - (b.Z - a.Z));
                float originY = ((originX - a.X) * (b.Y - a.Y) / (b.X - a.X)) + a.Y;
                float originZ = ((originX - a.X) * (b.Z - a.Z) / (b.X - a.X)) + a.Z;

                float distance = Vector3.Distance(Vector3.Zero, new(originX, originY, originZ));

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, distance, screen, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            for (float y = yMin; y <= yMax; ++y)
            {
                float x = ((y - y1) * (x2 - x1) / (y2 - y1)) + x1;

                float originX = ((a.Z * b.X) - (a.X * b.Z)) / ((screen.FocalDistance * (b.X - a.X) / x) - (b.Z - a.Z));
                float originY = ((originX - a.X) * (b.Y - a.Y) / (b.X - a.X)) + a.Y;
                float originZ = ((originX - a.X) * (b.Z - a.Z) / (b.X - a.X)) + a.Z;

                float distance = Vector3.Distance(Vector3.Zero, new(originX, originY, originZ));

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, distance, screen, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            return projections;
        }
    }
}