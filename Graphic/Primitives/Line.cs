using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base;
using maze.Graphic.Primitives.Helpres;

namespace maze.Graphic.Primitives
{
    public class Line : Primitive
    {
        public Line(Vector3 a, Vector3 b, ConsoleColor color)
        {
            GlobalVertices = new Vector3[] { a, b };
            Color = color;
            Normal = Vector3.Zero;
        }

        public override ICollection<ProjectedVertice> Project(Camera camera, Vector3 light)
        {
            return ProjectLine(GlobalVertices[0], GlobalVertices[1], camera, light);
        }

        private ICollection<ProjectedVertice> ProjectLine(Vector3 v1, Vector3 v2, Camera camera, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Vector3 a = camera.View(v1);
            Vector3 b = camera.View(v2);

            float x1 = a.X * camera.FocalDistance / a.Z;
            float y1 = a.Y * camera.FocalDistance / a.Z;

            float x2 = b.X * camera.FocalDistance / b.Z;
            float y2 = b.Y * camera.FocalDistance / b.Z;

            float xMin = Math.Min(x1, x2);
            float xMax = Math.Max(x1, x2);

            float yMin = Math.Min(y1, y2);
            float yMax = Math.Max(y1, y2);

            for (float x = xMin; x <= xMax; ++x)
            {
                float y = ((x - x1) * (y2 - y1) / (x2 - x1)) + y1;

                if (Intersections.LinePlaneIntersection(
                    Vector3.Zero,
                    new(x, y, camera.FocalDistance),
                    Intersections.GetAnyLineNormal(a, b),
                    a,
                    out Vector3 origin))
                {
                    if (ProjectedVerticeIsInsideScreen(x, y, origin, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }
            }

            for (float y = yMin; y <= yMax; ++y)
            {
                float x = ((y - y1) * (x2 - x1) / (y2 - y1)) + x1;

                if (Intersections.LinePlaneIntersection(
                    Vector3.Zero,
                    new(x, y, camera.FocalDistance),
                    Intersections.GetAnyLineNormal(a, b),
                    a,
                    out Vector3 origin))
                {
                    if (ProjectedVerticeIsInsideScreen(x, y, origin, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }
            }

            return projections;
        }
    }
}