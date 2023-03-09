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

            Pivot rotatedPivot = new(
                GlobalVertices[0].RotationInOZ(screen),
                Pivot.Forward.NormalRotationInOZ(screen),
                Pivot.Up.NormalRotationInOZ(screen),
                Pivot.Right.NormalRotationInOZ(screen)
            );

            Vector3 projectedCenter = rotatedPivot.Center * screen.FocalDistance / rotatedPivot.Center.Z;
            float projectedRadius = Radius * screen.FocalDistance / rotatedPivot.Center.Z;

            for (float x = projectedCenter.X - projectedRadius; x <= projectedCenter.X + projectedRadius; ++x)
            {
                float y = 0;
                Vector3 porjection = new(x, y, screen.FocalDistance);
                Vector3 pointGlobalOnCircle = Vector3Extensions.LinePlaneIntersection(
                    Vector3.Zero,
                    porjection,
                    rotatedPivot.Forward,
                    rotatedPivot.Center
                );

                Vector3 pointLocalOnCircle = rotatedPivot.ToLocalCoords(pointGlobalOnCircle);
                float xLocal = pointLocalOnCircle.X;
                float y1Local = (float)Math.Sqrt((Radius * Radius) - (xLocal * xLocal));
                float y2Local = -y1Local;

                Vector3 point1Global = rotatedPivot.ToGlobalCoords(new(xLocal, y1Local, 0));
                Vector3 point2Global = rotatedPivot.ToGlobalCoords(new(xLocal, y2Local, 0));

                y = point1Global.Y * screen.FocalDistance / point1Global.Z;

                if (ProjectedVerticeIsInsideScreen(x, y, point1Global, Normal, screen, light, out ProjectedVertice projection1))
                {
                    projections.Add(projection1);
                }


                y = point2Global.Y * screen.FocalDistance / point2Global.Z;

                if (ProjectedVerticeIsInsideScreen(x, y, point2Global, Normal, screen, light, out projection1))
                {
                    projections.Add(projection1);
                }

            }

            for (float y = projectedCenter.Y - projectedRadius; y <= projectedCenter.Y + projectedRadius; ++y)
            {
                float x = 0;
                Vector3 porjection = new(x, y, screen.FocalDistance);
                Vector3 pointGlobalOnCircle = Vector3Extensions.LinePlaneIntersection(
                    Vector3.Zero,
                    porjection,
                    rotatedPivot.Forward,
                    rotatedPivot.Center
                );

                Vector3 pointLocalOnCircle = rotatedPivot.ToLocalCoords(pointGlobalOnCircle);
                float yLocal = pointLocalOnCircle.Y;
                float x1Local = (float)Math.Sqrt((Radius * Radius) - (yLocal * yLocal));
                float x2Local = -x1Local;

                Vector3 point1Global = rotatedPivot.ToGlobalCoords(new(x1Local, yLocal, 0));
                Vector3 point2Global = rotatedPivot.ToGlobalCoords(new(x2Local, yLocal, 0));

                x = point1Global.X * screen.FocalDistance / point1Global.Z;

                if (ProjectedVerticeIsInsideScreen(x, y, point1Global, Vector3.One, screen, light, out ProjectedVertice projection1))
                {
                    projections.Add(projection1);
                }


                x = point2Global.X * screen.FocalDistance / point2Global.Z;

                if (ProjectedVerticeIsInsideScreen(x, y, point2Global, Vector3.One, screen, light, out projection1))
                {
                    projections.Add(projection1);
                }

            }

            return projections;
        }
    }
}