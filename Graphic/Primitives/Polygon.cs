using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base;
using maze.Graphic.Primitives.Helpres;

namespace maze.Graphic.Primitives
{
    public class Polygon : Primitive
    {

        public Polygon(Vector3 a, Vector3 b, Vector3 c, ConsoleColor color)
        {
            GlobalVertices = new Vector3[] { a, b, c };
            Color = color;
            Normal = Vector3.Normalize(Vector3.Cross(GlobalVertices[1] - GlobalVertices[0], GlobalVertices[2] - GlobalVertices[0]));
        }

        public override ICollection<ProjectedVertice> Project(Camera camera, Vector3 light)
        {
            return ProjectPolygon(GlobalVertices[0], GlobalVertices[1], GlobalVertices[2], camera, light);
        }

        private ICollection<ProjectedVertice> ProjectPolygon(Vector3 a, Vector3 b, Vector3 c, Camera camera, Vector3 light)
        {
            Vector3 aRotated = camera.View(a);
            Vector3 bRotated = camera.View(b);
            Vector3 cRotated = camera.View(c);
            Normal = Vector3.Normalize(Vector3.Cross(bRotated - aRotated, cRotated - aRotated));

            List<ProjectedVertice> projections = new();

            float x1 = aRotated.X * camera.FocalDistance / aRotated.Z;
            float y1 = aRotated.Y * camera.FocalDistance / aRotated.Z;

            float x2 = bRotated.X * camera.FocalDistance / bRotated.Z;
            float y2 = bRotated.Y * camera.FocalDistance / bRotated.Z;

            float x3 = cRotated.X * camera.FocalDistance / cRotated.Z;
            float y3 = cRotated.Y * camera.FocalDistance / cRotated.Z;

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

                projections.AddRange(ProjectLine(x, c1, b1, camera, light));
            }

            for (float x = x2; x <= x3; ++x)
            {
                float b1 = ((x - x1) * (y3 - y1) / (x3 - x1)) + y1;
                float a1 = ((x - x2) * (y3 - y2) / (x3 - x2)) + y2;

                projections.AddRange(ProjectLine(x, a1, b1, camera, light));
            }

            return projections;
        }

        private IEnumerable<ProjectedVertice> ProjectLine(float x, float y1, float y2, Camera camera, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            float yMin = Math.Min(y1, y2);
            float yMax = Math.Max(y1, y2);

            for (float y = yMin; y <= yMax; ++y)
            {
                if (Intersections.LinePlaneIntersection(
                    Vector3.Zero,
                    new(x, y, camera.FocalDistance),
                    camera.View(GlobalVertices[0]),
                    camera.View(GlobalVertices[1]),
                    camera.View(GlobalVertices[2]),
                    out Vector3 origin))
                {
                    if (ProjectedVerticeIsInsideScreen(x, y, origin, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    };
                }
            }

            return projections;
        }
    }
}