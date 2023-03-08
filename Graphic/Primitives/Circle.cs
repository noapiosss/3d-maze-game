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
            _ = Pivot.Forward.RotationInOZ(screen);
            Vector3 rotatedPivotUp = Pivot.Up.RotationInOZ(screen);
            Vector3 rotatedPivotRight = Pivot.Right.RotationInOZ(screen);
            Vector3 rotatedCenter = GlobalVertices[0].RotationInOZ(screen);
            Vector3 projectedCenter = rotatedCenter * screen.FocalDistance / rotatedCenter.Z;

            float projectedRadius = Radius * screen.FocalDistance / rotatedCenter.Z;

            float xMin = (float)(-projectedRadius * Math.Cos(rotatedPivotRight.Angle(Vector3.UnitX)));
            float xMax = (float)(projectedRadius * Math.Cos(rotatedPivotRight.Angle(Vector3.UnitX)));

            for (float x = xMin; x <= xMax; ++x)
            {
                float y1 = (float)Math.Sqrt(Math.Pow(projectedRadius, 2) - Math.Pow(x, 2));
                float y2 = (float)-Math.Sqrt(Math.Pow(projectedRadius, 2) - Math.Pow(x, 2));

                if (IsPorjectibleCirclePoint(
                    x + projectedCenter.X,
                    y1 + projectedCenter.Y,
                    rotatedCenter,
                    screen,
                    light,
                    out ProjectedVertice projection))
                {
                    projections.Add(projection);
                }

                if (IsPorjectibleCirclePoint(
                    x + projectedCenter.X,
                    y2 + projectedCenter.Y,
                    rotatedCenter,
                    screen,
                    light,
                    out projection))
                {
                    projections.Add(projection);
                }
            }

            float yMin = (float)(-projectedRadius * Math.Cos(rotatedPivotUp.Angle(Vector3.UnitY)));
            float yMax = (float)(projectedRadius * Math.Cos(rotatedPivotUp.Angle(Vector3.UnitY)));

            for (float y = yMin; y <= yMax; ++y)
            {
                float x1 = (float)Math.Sqrt(Math.Pow(projectedRadius, 2) - Math.Pow(y, 2));
                float x2 = (float)-Math.Sqrt(Math.Pow(projectedRadius, 2) - Math.Pow(y, 2));

                if (IsPorjectibleCirclePoint(
                    x1 + projectedCenter.X,
                    y + projectedCenter.Y,
                    rotatedCenter,
                    screen,
                    light,
                    out ProjectedVertice projection))
                {
                    projections.Add(projection);
                }

                if (IsPorjectibleCirclePoint(
                    x2 + projectedCenter.X,
                    y + projectedCenter.Y,
                    rotatedCenter,
                    screen,
                    light,
                    out projection))
                {
                    projections.Add(projection);
                }
            }

            return projections;
        }

        private bool IsPorjectibleCirclePoint(float x, float y, Vector3 rotatedCenter, Screen screen, Vector3 light, out ProjectedVertice projection)
        {
            Vector3 origin = Vector3Extensions.LinePlaneIntersection(
                Vector3.Zero,
                new(x, y, screen.FocalDistance),
                rotatedCenter,
                rotatedCenter + (Pivot.Up.RotationInOZ(screen) * Radius),
                rotatedCenter + (Pivot.Right.RotationInOZ(screen) * Radius)
            );

            return ProjectedVerticeIsInsideScreen((int)x, (int)y, origin, Normal, screen, light, out projection);
        }
    }
}