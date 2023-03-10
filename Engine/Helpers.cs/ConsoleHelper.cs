using System;

namespace maze.Engine.Helpers.cs
{
    public class ConsoleHelper
    {
        private readonly char[,] _pixelsBuffer;
        private readonly ConsoleColor[,] _colorsBuffer;
        private readonly int _height;
        private readonly int _width;

        public ConsoleHelper(int height, int width)
        {
            _height = height;
            _width = width;
            _pixelsBuffer = new char[width, height];
            _colorsBuffer = new ConsoleColor[width, height];
        }

        public void Draw(char[,] pixels, ConsoleColor[,] colors)
        {
            for (int j = 0; j < _height; ++j)
            {
                for (int i = 0; i < _width; ++i)
                {
                    if (_pixelsBuffer[i, j] != pixels[i, j] ||
                        _colorsBuffer[i, j] != colors[i, j])
                    {
                        Console.ForegroundColor = colors[i, j];
                        Console.SetCursorPosition(i, _height - 1 - j);
                        Console.Write(pixels[i, j]);

                        _pixelsBuffer[i, j] = pixels[i, j];
                        _colorsBuffer[i, j] = colors[i, j];
                    }
                }
            }
        }
    }
}