﻿using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Game
    {
        GameSession _gameSession;
        //byte[] buffer = Levelreader.ReadFileToByteArray("TextFile1.txt");
        byte[] buffer;
        //string[,] grid = Import("TextFile1.txt", out buffer);

        public void Start()
        {
            //string[,] grid = Levelreader.ReadFileToArray("TextFile1.txt", out byte[] buffer);
            //string[,] grid = Import("TextFile1.txt", out buffer);

            _gameSession = new GameSession();

            buffer = _gameSession._currentView.Import("TextFile1.txt");
            Console.CursorVisible = false;
            GameLoop();
        }

        private void PlayerInput()
        {
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            ConsoleKey key = keypress.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (_gameSession._currentView.Walkable(_gameSession._currentPlayer.X, _gameSession._currentPlayer.Y - 1))
                    {
                        DrawModel(_gameSession._currentPlayer.PlayerColor, _gameSession._currentPlayer.X, _gameSession._currentPlayer.Y);
                        _gameSession._currentPlayer.Y -= 1;
                    }
                    
                    break;
                case ConsoleKey.DownArrow:
                    if (_gameSession._currentView.Walkable(_gameSession._currentPlayer.X, _gameSession._currentPlayer.Y + 1))
                    {
                        DrawModel(_gameSession._currentPlayer.PlayerColor, _gameSession._currentPlayer.X, _gameSession._currentPlayer.Y);
                        _gameSession._currentPlayer.Y += 1;
                    }
                   
                    break;
                case ConsoleKey.LeftArrow:
                    if (_gameSession._currentView.Walkable(_gameSession._currentPlayer.X - 1, _gameSession._currentPlayer.Y))
                    {
                        DrawModel(_gameSession._currentPlayer.PlayerColor, _gameSession._currentPlayer.X, _gameSession._currentPlayer.Y);
                        _gameSession._currentPlayer.X -= 1;
                    }                   
                    break;
                case ConsoleKey.RightArrow:
                    if (_gameSession._currentView.Walkable(_gameSession._currentPlayer.X + 1, _gameSession._currentPlayer.Y))
                    {
                        DrawModel(_gameSession._currentPlayer.PlayerColor, _gameSession._currentPlayer.X, _gameSession._currentPlayer.Y);
                        _gameSession._currentPlayer.X += 1;
                    }                    
                    break;
                default:                   
                    break;
            }
        }
        private void GameLoop()
        {
            FastDraw(buffer);
            while (true)
            {
                
                DrawModel(_gameSession._currentPlayer.PlayerColor, _gameSession._currentPlayer.X, _gameSession._currentPlayer.Y,
                    _gameSession._currentPlayer.PlayerModel);
                PlayerInput();
            }
        }

        public void DrawModel(ConsoleColor modelColor, int xPos, int yPos, string modelChar = " ")
        {
            Console.ForegroundColor = modelColor;
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(modelChar);
            Console.ResetColor();
        }
        public void FastDraw(byte[] buffer)
        {
            using (var stdout = Console.OpenStandardOutput(buffer.Length))
            {
                // fill

                stdout.Write(buffer, 3, buffer.Length - 3);
                // rinse and repeat
            }

        }
    }
}
