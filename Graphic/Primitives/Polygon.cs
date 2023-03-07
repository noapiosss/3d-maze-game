using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

namespace maze.Graphic.Primitives
{
    public class Polygon : Primitive
    {

        public Polygon(Vector3 a, Vector3 b, Vector3 c, ConsoleColor color)
        {
            Vertices = new Vector3[] { a, b, c };
            Indexes = new (int, int, int)[] { (0, 1, 2) };
            Color = color;
            Normal = Vector3.Normalize(Vector3.Cross(Vertices[1] - Vertices[0], Vertices[2] - Vertices[0]));
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            return ProjectPolygon(Vertices[0], Vertices[1], Vertices[2], screen, light);
        }

        private ICollection<ProjectedVertice> ProjectPolygon(Vector3 a, Vector3 b, Vector3 c, Screen screen, Vector3 light)
        {
            Vector3 aRotated = a.RotationInOZ(screen);
            Vector3 bRotated = b.RotationInOZ(screen);
            Vector3 cRotated = c.RotationInOZ(screen);

            List<ProjectedVertice> projections = new();

            float x1 = aRotated.X * screen.FocalDistance / aRotated.Z;
            float y1 = aRotated.Y * screen.FocalDistance / aRotated.Z;

            float x2 = bRotated.X * screen.FocalDistance / bRotated.Z;
            float y2 = bRotated.Y * screen.FocalDistance / bRotated.Z;

            float x3 = cRotated.X * screen.FocalDistance / cRotated.Z;
            float y3 = cRotated.Y * screen.FocalDistance / cRotated.Z;

            if (x1 > x2)
            {
                (x1, x2, y1, y2) = (x2, x1, y2, y1);
            }

            if (x2 > x3)
            {
                (x3, x2, y3, y2) = (x2, x3, y2, y3);
            }

            if (x1 > x2)
            {
                (x1, x2, y1, y2) = (x2, x1, y2, y1);
            }


            for (float x = x1; x <= x2; ++x)
            {
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;
                float c1 = ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;

                projections.AddRange(ProjectLine(x, c1, b1, screen, light));
            }

            for (float x = x2; x <= x3; ++x)
            {
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;
                float a1 = ((x - x2) * (y3 - y2) / (x3 - x2)) + y2;

                projections.AddRange(ProjectLine(x, a1, b1, screen, light));
            }

            return projections;
        }

        private IEnumerable<ProjectedVertice> ProjectLine(float x, float y1, float y2, Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            float yMin = Math.Min(y1, y2);
            float yMax = Math.Max(y1, y2);

            for (float y = yMin; y <= yMax; ++y)
            {
                Vector3 origin = Vector3Extensions.LinePlaneIntersection(
                    Vector3.Zero,
                    new(x, y, screen.FocalDistance),
                    Vertices[0].RotationInOZ(screen),
                    Vertices[1].RotationInOZ(screen),
                    Vertices[2].RotationInOZ(screen)
                );

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, origin, Normal.NormalRotationInOZ(screen), screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            return projections;
        }
    }
}