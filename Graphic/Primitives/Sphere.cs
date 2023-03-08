using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

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

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Vector3 rotatedCenter = GlobalVertices[0].RotationInOZ(screen);
            Vector3 projectedCenter = rotatedCenter * screen.FocalDistance / rotatedCenter.Z;
            float projectedRadius = Radius * screen.FocalDistance / rotatedCenter.Z;

            for (float x = -projectedRadius; x <= projectedRadius; ++x)
            {
                for (float y = -projectedRadius; y <= projectedRadius; ++y)
                {
                    if (Math.Pow(x, 2) + Math.Pow(y, 2) <= Math.Pow(projectedRadius, 2))
                    {
                        Vector3 origin = Vector3Extensions.LineSphereIntersection(
                            Vector3.Zero,
                            new(x + projectedCenter.X, y + projectedCenter.Y, screen.FocalDistance),
                            rotatedCenter,
                            Radius
                        );

                        Vector3 normal = origin - rotatedCenter;

                        if (ProjectedVerticeIsInsideScreen((int)(x + projectedCenter.X), (int)(y + projectedCenter.Y), origin, normal, screen, light, out ProjectedVertice projection))
                        {
                            projections.Add(projection);
                        }
                    }
                }
            }

            return projections;
        }
    }
}