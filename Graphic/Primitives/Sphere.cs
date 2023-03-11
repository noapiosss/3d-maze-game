using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base;
using maze.Graphic.Primitives.Helpres;

namespace maze.Graphic.Primitives
{
    public class Sphere : Primitive
    {
        public float Radius { get; protected set; }
        public Sphere(Vector3 pivot, float radius, ConsoleColor color)
        {
            Pivot = new(pivot, Vector3.UnitZ, Vector3.UnitZ, Vector3.UnitX);
            LocalVertices = new Vector3[] { Vector3.Zero };
            GlobalVertices = ToGlobalVertices();
            Radius = radius;
            Color = color;
            Normal = Vector3.Zero;
        }

        public override ICollection<ProjectedVertice> Project(Camera camera, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Vector3 rotatedCenter = camera.View(GlobalVertices[0]);
            Vector3 projectedCenter = rotatedCenter * camera.FocalDistance / rotatedCenter.Z;
            float projectedRadius = Radius * camera.FocalDistance / rotatedCenter.Z;

            for (float x = -projectedRadius; x <= projectedRadius; ++x)
            {
                for (float y = -projectedRadius; y <= projectedRadius; ++y)
                {
                    if (Math.Pow(x, 2) + Math.Pow(y, 2) <= Math.Pow(projectedRadius, 2))
                    {
                        if (Intersections.LineSphereIntersection(
                            Vector3.Zero,
                            new(x + projectedCenter.X, y + projectedCenter.Y, camera.FocalDistance),
                            rotatedCenter,
                            Radius,
                            out Vector3 origin1,
                            out Vector3 origin2))
                        {
                            if (ProjectedVerticeIsInsideScreen(
                                x + projectedCenter.X,
                                y + projectedCenter.Y,
                                origin1,
                                origin1 - rotatedCenter,
                                camera,
                                light,
                                out ProjectedVertice projection))
                            {
                                projections.Add(projection);
                            }

                            if (ProjectedVerticeIsInsideScreen(
                                x + projectedCenter.X,
                                y + projectedCenter.Y,
                                origin2,
                                origin2 - rotatedCenter,
                                camera,
                                light,
                                out projection))
                            {
                                projections.Add(projection);
                            }
                        }
                    }
                }
            }

            return projections;
        }
    }
}