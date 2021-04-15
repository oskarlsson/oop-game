﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace oop_game
{
    class Program
    {
        static GameSession _gameSession;
        static byte[] buffer;
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            _gameSession = new GameSession();
            buffer = _gameSession.currentMaze.Buffer;

            Console.CursorVisible = true;

            Menu();
            //GameLoop();
        }


        public static void Menu()
        {
            
            Console.Clear();

            // Title
            ASCIIModel title = new ASCIIModel("ASCII/Title.txt");
            FastDraw(title.Buffer);

            int x = 10, y = 10; // Position of the menu on the screen
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(5, 8);
            Console.WriteLine("     Welcome {0} ", Environment.UserName);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Spela ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("Instruktioner");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("Avsluta");
            Console.CursorVisible = false;
            do // Main loop for the menu. It continues until the user select 'AVSLUTA'
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(x - 3, y);
                Console.WriteLine("■■"); // Menu pointer
                ConsoleKeyInfo keypress = Console.ReadKey(true);
                ConsoleKey key = keypress.Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (y == 12) // Last option of the menu
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.WriteLine("  ");
                            y = 10;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        else
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y += 1;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y == 10)  // First option of the menu
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y = 12;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        else
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y -= 1;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (y == 10)
                        {
                            Console.ResetColor();
                            GameLoop();
                        }
                        else
                        if (y == 11)
                        {
                            ViewInstructions(); // To do 
                            Console.ReadKey();
                            Menu();
                        }

                        if (y == 12)
                        {
                            // The game is stopped and ended by user
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Tryck på en knapp!");
                            Exit();
                        }

                        break;
                    default:
                        break;
                }
                Console.CursorVisible = false;
            } while (true);
        }
        public static void ViewInstructions()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Instruktioner...");
            Console.ReadKey();
            Menu();
        }

        public static void Exit()
        {
            Console.ResetColor();
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PlayerInput()
        {
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            ConsoleKey key = keypress.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    ErasePlayer();
                    _gameSession.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    ErasePlayer();
                    _gameSession.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    ErasePlayer();
                    _gameSession.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    ErasePlayer();
                    _gameSession.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                        Menu();
                        break;
                default:
                    break;

            }
        }
        private static void GameLoop()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.ResetColor();
            FastDraw(buffer);
            var cursorpos = Console.CursorTop + 1;

            while (true)
            {
                DrawPlayer();
                DrawEnemy();
                DrawDrops();
                PlayerInput();
                Console.CursorTop = cursorpos;
                Console.WriteLine(" ");
                Console.CursorTop = cursorpos;
                StatusBar();

            }
        }
        public static void DrawEnemy()
        {
            foreach(Enemy enemy in _gameSession.enemies)
            {
                //Console.WriteLine(_gameSession.enemies.GetType().GetProperty(_gameSession.enemies.ToString())); 
                Console.ForegroundColor = enemy.Color;
                Console.SetCursorPosition(enemy.X, enemy.Y);
                Console.Write(enemy.Model);
                Console.ResetColor();
            }           
        }
        public static void DrawDrops()
        {
            foreach (Item drop in _gameSession.drops)
            {
                //Console.WriteLine(_gameSession.enemies.GetType().GetProperty(_gameSession.enemies.ToString())); 
                Console.ForegroundColor = drop.itemColor;
                Console.SetCursorPosition(drop.X, drop.Y);
                Console.Write(drop.itemModel);
                Console.ResetColor();
            }
        }
        public static void DrawPlayer()
        {
            Console.ForegroundColor = _gameSession.currentPlayer.PlayerColor;
            Console.SetCursorPosition(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
            Console.Write(_gameSession.currentPlayer.PlayerModel);
            Console.ResetColor();
        }
        public static void ErasePlayer()
        {
            Console.ForegroundColor = _gameSession.currentPlayer.PlayerColor;
            Console.SetCursorPosition(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
            Console.Write(" ");
            Console.ResetColor();
        }
        public static void FastDraw(byte[] buffer)
        {
            using (var stdout = Console.OpenStandardOutput(buffer.Length))
            {
                // fill

                stdout.Write(buffer, 0, buffer.Length);
                // rinse and repeat
            }
        }

        public static void StatusBar()
        {
            // Position
            Console.ResetColor();
            Console.Write("Position: {0},{1}", _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);


            // Health
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Health: {0}".PadLeft(50), _gameSession.currentPlayer.HitPoints);

            // Show how much potion adds to health
            if (_gameSession.currentPlayer.inventory.OfType<Potion>().Any())
            {
                Console.Write($" (+{_gameSession.currentPlayer.inventory.OfType<Potion>().First().HealEffect})");
            }


            // Attack
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Attack: {0}".PadLeft(20), _gameSession.currentPlayer.AttackDamage);

            // Show how much weapon and potion adds to damage
            if (_gameSession.currentPlayer.inventory.OfType<Weapon>().Any() || _gameSession.currentPlayer.inventory.OfType<Potion>().Any())
            {
                Console.Write($" (+{_gameSession.currentPlayer.inventory.OfType<Weapon>().First().AttackDamage + _gameSession.currentPlayer.inventory.OfType<Potion>().First().AttackEffect})");
            }

            
            // Level
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Level: {0}".PadLeft(20), _gameSession.currentPlayer.level);


            // Experience
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("EXP: {0}".PadLeft(20), _gameSession.currentPlayer.ExperiencePoints);
        }
    }
}

