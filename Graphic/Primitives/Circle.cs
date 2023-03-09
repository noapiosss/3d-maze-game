using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

namespace maze.Graphic.Primitives
{
    public class Circle : Primitive
    {
        public float Radius { get; protected set; }

        public Circle(Vector3 pivot, float radius, ConsoleColor color)
        {
            Pivot = new(pivot, Vector3.UnitZ, Vector3.UnitY, Vector3.UnitX);
            LocalVertices = new Vector3[] { Vector3.Zero };
            GlobalVertices = ToGlobalVertices();
            Radius = radius;
            Color = color;
            Normal = Vector3.Zero;
        }
        public Circle(Vector3 pivot, Vector3 pivotForward, Vector3 pivotUp, Vector3 pivotRight, float radius, ConsoleColor color)
        {
            Pivot = new(pivot, pivotForward, pivotUp, pivotRight);
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
            // Vector3 projectedCenter = rotatedCenter * screen.FocalDistance / rotatedCenter.Z;

            Vector3 rotatedPivotUp = Pivot.Up.NormalRotationInOZ(screen);
            Vector3 rotatedPivotRight = Pivot.Right.NormalRotationInOZ(screen);

            // float projectedRadius = Radius * screen.FocalDistance / rotatedCenter.Z;

            for (double t = 0; t <= 2 * Math.PI; t += 0.1)
            {
                Vector3 point = (Radius * (((float)Math.Cos(t) * rotatedPivotUp) + ((float)Math.Sin(t) * rotatedPivotRight))) + rotatedCenter;
                float x = point.X * screen.FocalDistance / point.Z;
                float y = point.Y * screen.FocalDistance / point.Z;

                // Vector3 origin = Vector3Extensions.LinePlaneIntersection(
                //     Vector3.Zero,
                //     new(x, y, screen.FocalDistance),
                //     rotatedCenter,
                //     rotatedCenter + (rotatedPivotUp * Radius),
                //     rotatedCenter + (rotatedPivotRight * Radius)
                // );

                if (ProjectedVerticeIsInsideScreen((int)x, (int)y, point, Normal, screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                }
            }

            return projections;
        }
    }
}