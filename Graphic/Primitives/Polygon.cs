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
            Vector3 aProjection = a.RotationInOZ(screen);
            Vector3 bProjection = b.RotationInOZ(screen);
            Vector3 cProjection = c.RotationInOZ(screen);

            if (aProjection.X > bProjection.X)
            {
                return ProjectPolygon(b, a, c, screen, light);
            }

            if (bProjection.X > cProjection.X)
            {
                return ProjectPolygon(a, c, b, screen, light);
            }

            List<ProjectedVertice> projection = new();

            float x1 = aProjection.X;
            float y1 = aProjection.Y;

            float x2 = bProjection.X;
            float y2 = bProjection.Y;

            float x3 = cProjection.X;
            float y3 = cProjection.Y;

            for (float x = x1; x <= x2; ++x)
            {
                float c1 = ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;

                projection.AddRange(ProjectLine(x, c1, b1, screen, light));
            }

            for (float x = x2; x <= x3; ++x)
            {
                float a1 = ((x - x2) * (y3 - y2) / (x3 - x2)) + y2;
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;

                projection.AddRange(ProjectLine(x, a1, b1, screen, light));
            }

            return projection;
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
                    Normal,
                    Vertices[0]
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