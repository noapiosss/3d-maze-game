using System;
using System.Numerics;
using maze.Graphic.Primitives.Extensions;

namespace maze.Engine
{
    public class Camera
    {
        public Vector3 Position { get; private set; }
        public Vector3 Forward { get; private set; }
        public Vector3 Up { get; private set; }
        public Vector3 Right { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public float FocalDistance { get; private set; }
        public float RenderDistance { get; private set; }

        public Camera(Vector3 cameraPosition, Vector3 cameraForward, Vector3 cameraUp, Vector3 cameraRight, int height, int widht, float focalDistance, float renderDistance)
        {
            Position = cameraPosition;
            Forward = cameraForward;
            Up = cameraUp;
            Right = cameraRight;
            Height = height;
            Width = widht;
            FocalDistance = focalDistance;
            RenderDistance = renderDistance;
        }

        public void LookUp(float angle)
        {
            float nextAngle = Forward.Angle(Vector3.UnitY) + angle;
            if (nextAngle is < 0 or > (float)Math.PI)
            {
                return;
            }

            Forward = Forward.RotateAround(Right, angle);
            Up = Up.RotateAround(Right, angle);
        }

        public void LookSide(float angle)
        {
            Forward = Forward.RotateY(angle);
            Up = Up.RotateY(angle);
            Right = Right.RotateY(angle);
        }

        public void MoveForward(float distance)
        {
            Position += Forward * distance;
        }

        public void MoveUp(float distance)
        {
            Position += Vector3.UnitY * distance;
        }

        public void MoveSide(float distance)
        {
            Position += Right * distance;
        }
    }
}