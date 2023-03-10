using System;
using maze.Engine.Extensions;

namespace maze.Engine
{
    public class Controller
    {
        private Frame _frame { get; set; }
        public Controller(Frame frame)
        {
            _frame = frame;
        }

        public void Run()
        {
            _frame.Render();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    _frame._screen.LookUp(0.05000f);
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    _frame._screen.LookUp(-0.05000f);
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    _frame._screen.LookSide(0.05000f);
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _frame._screen.LookSide(-0.05000f);
                }
                if (key.Key == ConsoleKey.W)
                {
                    _frame._screen.MoveForward(10);
                }
                if (key.Key == ConsoleKey.S)
                {
                    _frame._screen.MoveForward(-10);
                }
                if (key.Key == ConsoleKey.D)
                {
                    _frame._screen.MoveSide(10);
                }
                if (key.Key == ConsoleKey.A)
                {
                    _frame._screen.MoveSide(-10);
                }
                if (key.Key == ConsoleKey.Spacebar)
                {
                    _frame._screen.MoveUp(10);
                }
                if (key.Key == ConsoleKey.C)
                {
                    _frame._screen.MoveUp(-10);
                }

                _frame.Render();
            }
        }
    }
}