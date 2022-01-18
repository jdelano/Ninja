using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninja
{
    public class Game
    {
        private int score;
        private bool isPlaying;
        private bool invalidated;
        private DateTime gameTime = DateTime.Now;
        public Ninja Player;
        public List<ITarget> Targets;
        public List<IThrowable> Ammo;

        public Game()
        {
            Player = new Ninja() { Location = (40, 13), Direction = Direction.North };
            Targets = new List<ITarget>();
            Ammo = new List<IThrowable>();
        }

        public void Run()
        {
            InitializeGame();
            do
            {
                ProcessInput();
                UpdateGame();
                RenderOutput();
            } while (isPlaying);
            Console.Clear();
            Console.WriteLine("Thank you for playing!");
            Console.WriteLine($"Your score was: {score}");
        }

        private void InitializeGame()
        {
            isPlaying = true;
            invalidated = true;
            score = 0;
            Targets.Add(new Box() { Location = (35, 13) });
            // Add additional Targets here
        }


        private void UpdateGame()
        {
            int updateInterval = 250;
            if (DateTime.Now.Subtract(gameTime) > TimeSpan.FromMilliseconds(updateInterval))
            {
                // Advance all ammo
                foreach (var ammo in Ammo)
                {
                    ammo.Advance();
                }

                // Detect collisions
                var collisions = from ammo in Ammo
                                 from target in Targets
                                 where target.IsHitting(ammo)
                                 select new { ammo, target };

                foreach (var collision in collisions.ToList())
                {
                    Targets.Remove(collision.target);
                    Ammo.Remove(collision.ammo);
                    score++;
                }

                invalidated = true;
                gameTime = DateTime.Now;
            }
        }

        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Player.Direction = Direction.North;
                        Player.Move();
                        invalidated = true;
                        break;
                    case ConsoleKey.DownArrow:
                        Player.Direction = Direction.South;
                        Player.Move();
                        invalidated = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        Player.Direction = Direction.West;
                        Player.Move();
                        invalidated = true;
                        break;
                    case ConsoleKey.RightArrow:
                        Player.Direction = Direction.East;
                        Player.Move();
                        invalidated = true;
                        break;
                    case ConsoleKey.Spacebar:
                        Ammo.Add(new Shuriken { Location = Player.Location, Direction = Player.Direction });
                        invalidated = true;
                        break;
                    case ConsoleKey.Q:  // Press Q to quit the game
                        isPlaying = false;
                        break;
                }
            }
        }

        private void RenderOutput()
        {
            if (invalidated)
            {
                Console.Clear();
                (int left, int top) = Console.GetCursorPosition();
                Console.SetCursorPosition(0, 0);
                Console.Write($"Score: {score}");
                Console.SetCursorPosition(Player.Location.x, Player.Location.y);
                Player.Display();
                Console.SetCursorPosition(left, top);
                foreach (var target in Targets)
                {
                    (left, top) = Console.GetCursorPosition();
                    Console.SetCursorPosition(target.Location.x, target.Location.y);
                    target.Display();
                    Console.SetCursorPosition(left, top);
                }
                foreach (var ammo in Ammo)
                {
                    (left, top) = Console.GetCursorPosition();
                    Console.SetCursorPosition(ammo.Location.x, ammo.Location.y);
                    ammo.Display();
                    Console.SetCursorPosition(left, top);
                }
                invalidated = false;
            }
        }
    }
}
