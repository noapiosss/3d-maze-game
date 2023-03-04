using System;
using maze.Graphic.Extensions;
using maze.Engine;
using maze.Graphic.Figures;

//Vector3 a = Vector3.Abs(new(-123, -2234, 0));
// Vector3 a = new(-123, -2234, 234);
// Console.WriteLine(a);

// float angleY = new Vector3(a.X, 0, a.Z).Angle(Vector3.UnitZ);
// Vector3 b = a.RotateY(Vector3.Zero, angleY);
// Console.WriteLine(b);

// float angleX = new Vector3(0, b.Y, b.Z).Angle(Vector3.UnitZ);
// Vector3 c = b.RotateX(Vector3.Zero, -angleX);
// Console.WriteLine(c);


Screen screen = new(new(0, 0, 0), new(0, 0, 1), new(0, 1, 0), new(1, 0, 0), 60, 100, 100);
Frame frame = new(screen, 300);

// Vector3 torusCenter = new(0, 0, 200);
// Torus torus = new(torusCenter, 25, 15, ConsoleColor.Yellow);

// Polygon4 oxz = new(new(-100, 0, 100), new(100, 0, 100), new(100, 0, 300), new(-100, 0, 300), ConsoleColor.Gray);
// Line ox = new(new(0, 0, 200), new(10, 0, 200), new(-1, -1.1f, 0), ConsoleColor.Red);
// Line oy = new(new(0, 0, 200), new(0, 10, 200), new(-1, -1.1f, 0), ConsoleColor.Green);
// Line oz = new(new(0, 0, 200), new(0, 0, 210), new(-1, -1.1f, 0), ConsoleColor.Blue);

Cube cube1 = new(new(-50, 10, 160), new(-30, -10, 200), ConsoleColor.Red);
Cube cube2 = new(new(-10, 10, 160), new(10, -10, 200), ConsoleColor.Green);
Cube cube3 = new(new(30, 10, 160), new(50, -10, 200), ConsoleColor.Blue);

frame.AddLight(new(-500, -500, 200));

// frame.AddPoints(torus.Points);
frame.AddPoints(cube1.Points);
frame.AddPoints(cube2.Points);
frame.AddPoints(cube3.Points);
// frame.AddPoints(oxz.Points);
// frame.AddPoints(ox.Points);
// frame.AddPoints(oy.Points);
// frame.AddPoints(oz.Points);
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
    // Console.WriteLine($"camera position: {frame._screen.CameraPosition}");
    // Console.WriteLine($"camera forward: {frame._screen.CameraForward}");
    // Console.WriteLine($"camera up: {frame._screen.CameraUp}");
    // Console.WriteLine($"camera right: {frame._screen.CameraRight}");
}