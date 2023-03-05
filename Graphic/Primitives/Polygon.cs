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
        }

        public override ICollection<ProjectedVertice> Project(Screen screen)
        {
            return ProjectPolygon(Vertices[0], Vertices[1], Vertices[2], screen);
        }

        private ICollection<ProjectedVertice> ProjectPolygon(Vector3 a, Vector3 b, Vector3 c, Screen screen)
        {
            Vector3 aProjection = a.RotationInOZ(screen);
            Vector3 bProjection = b.RotationInOZ(screen);
            Vector3 cProjection = c.RotationInOZ(screen);

            if (aProjection.X > bProjection.X)
            {
                return ProjectPolygon(b, a, c, screen);
            }

            if (bProjection.X > cProjection.X)
            {
                return ProjectPolygon(a, c, b, screen);
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

                projection.AddRange(ProjectLine(x, c1, b1, screen));
            }

            for (float x = x2; x <= x3; ++x)
            {
                float a1 = ((x - x2) * (y3 - y2) / (x3 - x2)) + y2;
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;

                projection.AddRange(ProjectLine(x, a1, b1, screen));
            }

            return projection;
        }

        private IEnumerable<ProjectedVertice> ProjectLine(float x, float y1, float y2, Screen screen)
        {
            List<ProjectedVertice> projections = new();

            float yMin = Math.Min(y1, y2);
            float yMax = Math.Max(y1, y2);

            for (float y = yMin; y <= yMax; ++y)
            {
                float originZ = CalculateOriginZ(Vertices[0], Vertices[1], Vertices[2], x, y, screen.FocalDistance);
                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, originZ, screen, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                };
            }

            return projections;
        }

        private static float CalculateOriginZ(Vector3 v1, Vector3 v2, Vector3 v3, float xp, float yp, float zp)
        {
            float a21 = v2.X - v1.X;
            float a22 = v3.X - v1.X;
            float a23 = v2.Y - v1.Y;
            float a31 = v3.Y - v1.Y;
            float a32 = v2.Z - v1.Z;
            float a33 = v3.Z - v1.Z;

            float xk = (a22 * a33) - (a23 * a32);
            float yk = (a23 * a31) - (a21 * a23);
            float zk = (a21 * a32) - (a22 * a31);

            return ((v1.X * xk) + (v1.Y * yk) + (v1.Z * zk)) / ((xk * xp / zp) + (yk * yp / zp) + zk);
        }
    }
}