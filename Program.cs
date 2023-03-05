using System;
using maze.Graphic.Extensions;
using maze.Engine;
using maze.Graphic.Primitives;

Screen screen = new(new(0, 0, 0), new(0, 0, 1), new(0, 1, 0), new(1, 0, 0), 60, 200, 100, 300);
Frame frame = new(screen);

// Vertice vertice = new(new(0, 0, 150), ConsoleColor.DarkGreen);
// frame.AddPrimitive(vertice);

// Line line1 = new(new(-20, 0, 150), new(20, 0, 150), ConsoleColor.Green);
// frame.AddPrimitive(line1);

// Line line2 = new(new(0, -20, 150), new(0, 20, 150), ConsoleColor.Green);
// frame.AddPrimitive(line2);

CubeEdges cubeEdges = new(new(0, 0, 150), 50, ConsoleColor.Red);
frame.AddPrimitive(cubeEdges);

// Cube cube = new(new(0, 0, 150), 50, ConsoleColor.Green);
// frame.AddPrimitive(cube);


frame.AddLight(new(0, -500, 0));
frame.Render();

while (true)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.UpArrow)
    {
        frame._screen.RotateX(-0.10000f);
    }
    if (key.Key == ConsoleKey.DownArrow)
    {
        frame._screen.RotateX(0.10000f);
    }
    if (key.Key == ConsoleKey.RightArrow)
    {
        frame._screen.RotateY(0.10000f);
    }
    if (key.Key == ConsoleKey.LeftArrow)
    {
        frame._screen.RotateY(-0.10000f);
    }
    if (key.Key == ConsoleKey.W)
    {
        frame._screen.MoveForward(10);
    }
    if (key.Key == ConsoleKey.S)
    {
        frame._screen.MoveForward(-10);
    }
    if (key.Key == ConsoleKey.D)
    {
        frame._screen.MoveSide(10);
    }
    if (key.Key == ConsoleKey.A)
    {
        frame._screen.MoveSide(-10);
    }

    frame.Render();
    Console.SetCursorPosition(0, 0);
    Console.WriteLine($"camera position: {frame._screen.CameraPosition}");
    // Console.WriteLine($"camera forward: {frame._screen.CameraForward}");
    // Console.WriteLine($"camera up: {frame._screen.CameraUp}");
    // Console.WriteLine($"camera right: {frame._screen.CameraRight}");
}