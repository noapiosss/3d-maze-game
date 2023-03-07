using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Graphic.Primitives;

namespace maze.Engine
{
    public class Frame
    {
        private static readonly char[] _brightnessGradient = " .:-=+*#%@".ToCharArray();
        //private static readonly char[] _brightnessGradient = " .`:,;'_^\"\\></-!~=)(|j?}{][ti+l7v1%yrfcJ32uIC$zwo96sngaT5qpkYVOL40&mG8*xhedbZUSAQPFDXWK#RNEHBM@".ToCharArray();
        public Screen _screen;
        private readonly List<Vector3> _light = new();
        private readonly List<Primitive> _primitives = new();
        private readonly float[,] _depthBuffer;
        private readonly char[,] _pixels;
        private readonly ConsoleColor[,] _colors;
        private readonly ConsoleHelper _consoleHelper;


        public Frame(Screen screen)
        {
            _screen = screen;
            _pixels = new char[screen.Width, screen.Height];
            _colors = new ConsoleColor[screen.Width, screen.Height];
            _depthBuffer = new float[screen.Width, screen.Height];
            _consoleHelper = new(screen.Height, screen.Width);

            for (int i = 0; i < _screen.Width; ++i)
            {
                for (int j = _screen.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _colors[i, j] = ConsoleColor.White;
                    _depthBuffer[i, j] = screen.RenderDistance;
                }
            }
        }

        public void AddLight(Vector3 light)
        {
            _light.Add(light);
        }

        public void AddPrimitive(Primitive primitive)
        {
            _primitives.Add(primitive);
        }

        private void Project()
        {
            foreach (Primitive primitive in _primitives)
            {
                foreach (ProjectedVertice projection in primitive.Project(_screen, _light[0]))
                {
                    FillPixel(projection);
                }
            }
        }

        private void FillPixel(ProjectedVertice projection)
        {
            float distance = Vector3.Distance(projection.Origin, Vector3.Zero);

            if (_depthBuffer[projection.X, projection.Y] > distance)
            {
                _depthBuffer[projection.X, projection.Y] = distance;
                _colors[projection.X, projection.Y] = projection.Color;
                _pixels[projection.X, projection.Y] = _brightnessGradient[(int)Math.Round((_brightnessGradient.Length - 1) * projection.Brightness)];
            }
        }

        private void Clear()
        {
            for (int i = 0; i < _screen.Width; ++i)
            {
                for (int j = _screen.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _depthBuffer[i, j] = _screen.RenderDistance;
                }
            }
        }

        public void Render()
        {
            Clear();
            Project();
            _consoleHelper.Draw(_pixels, _colors);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}