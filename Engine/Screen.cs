using System;
using System.Numerics;

namespace Maze.Engine
{
    public class Screen
    {
        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraForward { get; set; }
        public Vector3 CameraUp { get; set; }
        public Vector3 CameraRight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public float FovX { get; set; }
        public float FovY { get; set; }
        public float Near { get; set; }
        public float Far { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Top { get; set; }

        public Screen(Vector3 cameraPosition, Vector3 cameraForward, Vector3 cameraUp, Vector3 cameraRight, int height, int widht, float near, float far, float fovX, float fovY)
        {
            CameraPosition = cameraPosition;
            CameraForward = cameraForward;
            CameraUp = cameraUp;
            CameraRight = cameraRight;
            Height = height;
            Width = widht;
            Near = near;
            Far = far;
            Left = (float)(-near * Math.Tan(fovX / 2));
            Right = (float)(near * Math.Tan(fovX / 2));
            Bottom = (float)(-near * Math.Tan(fovY / 2));
            Top = (float)(near * Math.Tan(fovY / 2)) / (widht / height);
        }

    }
}