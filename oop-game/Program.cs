using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using NAudio.Wave;
using System.Threading.Tasks;

namespace oop_game
{
    class Program
    {
        static GameSession _gameSession;
        static byte[] buffer;
        static byte[] clearBuffer;
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;

        static readonly WaveOutEvent outputDevice = new WaveOutEvent();
        static AudioFileReader audioFile;
        
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            _gameSession = new GameSession();
            buffer = _gameSession.currentMaze.Buffer;
            //read in an empty file, used to clear the screen way faster than console.clear by overwriting everything with emptychars
            clearBuffer = File.ReadAllBytes("ASCII/Clear.txt");
            Console.CursorVisible = true;

            ASCIIModel title = new ASCIIModel("ASCII/Title.txt");
            FastDraw(title.Buffer);

            PrintAnimation();
            PlaySound("Sound/Menu.mp3");
            Menu("main");

        }
        public static void PlaySound(string filePath)
        {
            outputDevice.Stop();
            audioFile = new AudioFileReader(filePath);
            outputDevice.Init(audioFile);
            outputDevice.Volume = 0.1f;
            outputDevice.Play();
        }
        public static void showStars() // To show stars on the screen
        {
            Console.ResetColor();
            Random rnd = new Random();
            for (int i = 0; i < 250; i++)
            {
                int winWidth = (Console.WindowWidth);
                int winHight = (Console.WindowHeight);
                int yPosition = rnd.Next(0, winHight);  // creates a random position for Y
                int xPosition = rnd.Next(0, winWidth);   // creates a random position for X
                Console.SetCursorPosition(xPosition, yPosition);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(".");
            }
        }
        public static void Menu(string menuStatus) // Menu has two states, main menu and a submenu to change player's color
        {
            Console.Clear();
            Console.ResetColor();
            ASCIIModel title = new ASCIIModel("ASCII/Title.txt");
            FastDraw(title.Buffer);
            showStars();
            
            int x = (Console.WindowWidth / 2), y = 10; // Position of the menu on the screen

            if (menuStatus == "main") // Main Menu 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(x-10, 8);
                Console.WriteLine("     Welcome {0} ", Environment.UserName);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(x, y);
                Console.WriteLine("Play game ");
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("Instructions");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("Settings");

                Console.SetCursorPosition(x, y + 3);
                Console.WriteLine("Quit game");
                Console.CursorVisible = false;
            }
            else
            if (menuStatus == "subMenu_Settings") // Submenu
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(x-25, 8);
                ConsoleColor color = _gameSession.currentPlayer.PlayerColor; // The current color of the player
                Console.WriteLine("Current color is             Pick a color!       Esc = Main menu");
                Console.SetCursorPosition(x-5, 8);
                Console.ForegroundColor = color;
                Console.Write(color);

                // To create a menu using a better illustration for user
                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("█████████");
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("█████████");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine("█████████");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.SetCursorPosition(x, y + 3);
                Console.WriteLine("█████████");
                Console.ResetColor();

            }

           



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

                        if (y == 13) // Last option of the menu
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

                            y = 13;
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
                            if (menuStatus == "main")
                            {
                                Console.ResetColor();
                                GameLoop();
                            }
                            else
                            {
                                _gameSession.currentPlayer.PlayerColor = ConsoleColor.Green;
                                changeColor_message(ConsoleColor.Green);

                            }
                        }

                        if (y == 11)
                        {
                            if (menuStatus == "main") // If 'Instruktioner' is selected
                            {
                                ViewInstructions(); // To do 
                                Console.ReadKey();
                                Menu("main");
                            }
                            else
                            {
                                _gameSession.currentPlayer.PlayerColor = ConsoleColor.Magenta;
                                changeColor_message(ConsoleColor.Magenta);
                            }


                        }

                        if (y == 12) // If 'Inställningar' is selected
                        {
                            if (menuStatus == "main")
                            {
                                Menu("subMenu_Settings");
                            }
                            else
                            {
                                _gameSession.currentPlayer.PlayerColor = ConsoleColor.Blue;
                                changeColor_message(ConsoleColor.Blue);
                            }
                        }

                        if (y == 13)
                        {
                            if (menuStatus == "main")
                            {
                                // The game is stopped and ended by user

                                Exit();
                            }
                            else
                            {

                                _gameSession.currentPlayer.PlayerColor = ConsoleColor.Yellow;
                                changeColor_message(ConsoleColor.Yellow);
                            }
                        }

                        break;
                    case ConsoleKey.Escape:    // Ability of ending the game using Esc key or jumping to the main menu from the submenu
                        if (menuStatus == "subMenu_Settings")
                            Menu("main");
                        else
                            if (menuStatus == "main")
                            Exit();
                        break;

                    default:
                        break;
                }
                Console.CursorVisible = false;
            } while (true);
        }

        public static void changeColor_message(ConsoleColor color)  // Color changing messge
        {
            int winWidth = (Console.WindowWidth / 2);
            Console.SetCursorPosition(winWidth-10, 15);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Player color changed to ");
            Console.SetCursorPosition(winWidth+20, 15);
            Console.ForegroundColor = color;
            Console.BackgroundColor = color;
            Console.Write("██");
            Thread.Sleep(800);
            Console.ResetColor();

            Menu("subMenu_Settings");
        }
        public static void ViewInstructions() //TDO
        {
            Console.Clear();
            Console.ResetColor();
            showStars();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(60, 20);
            Console.Write("* Move using the arrow keys");
            Console.SetCursorPosition(60, 22);
            Console.Write("* Fight your way through the dungeon to earn experience points and weapon rewards to take on stronger enemeies");
            Console.SetCursorPosition(60, 24);
            Console.Write("* Heal up with potions from your inventory by pressing P during a fight");
            Console.ReadKey();
            Menu("main");
        }

        public static void Exit() // Exit the game
        {
            int x = (Console.WindowWidth / 2), y = 20;
            Console.ResetColor();
            Console.SetCursorPosition(x-10, y);
            Console.WriteLine("Game exited...       Press any button!");
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
                    //ErasePlayer();
                    _gameSession.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    //ErasePlayer();
                    _gameSession.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    //ErasePlayer();
                    _gameSession.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    //ErasePlayer();
                    _gameSession.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Menu("main");
                    break;
                default:
                    break;

            }
        }
        private static void GameLoop()
        {
            PlaySound("Sound/Maze.mp3");
            //outputDevice.Stop();
            //audioFile = new AudioFileReader("Sound/Maze.mp3");
            //outputDevice.Init(audioFile);
            //outputDevice.Play();

            Console.CursorVisible = false;
            Console.Clear();
            Console.ResetColor();
            FastDraw(buffer);
            var cursorpos = Console.CursorTop + 1;

            while (true)
            {
                if (_gameSession.currentPlayer.isAlive == false)
                {
                    _gameSession = new GameSession();
                    Menu("main");
                }
                if (_gameSession.inFight)
                {
                    FightView();
                }
                //"Console.Clear"
                //FastDraw(clearBuffer);
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                //Redraw the maze and all characters on it
                FastDraw(buffer);
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
        public static int Mod(int a , int b)
        {
            //returns a value between 0 and b-1
            //implements modulo instead of remainder
            if (a < 0)
            {
                if (b + (a % b) == b)
                {
                    return 0;
                }
                return b + (a % b);
            }
            return a % b;
        }
        public static void FightView()
        {
            PlaySound("Sound/Fight.mp3");
            int fightMenuCursor = 0;
            Console.Clear();
            FastDraw(_gameSession.currentFight.fightScene.Buffer);
            while (_gameSession.inFight)
            {
                List<Potion> potions = _gameSession.currentPlayer.inventory.OfType<Potion>().ToList();
                
                
                StatusBar();
                //Clear previous arrow selection
                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(25, 22 + i);
                    Console.WriteLine("            ");

                }
                Console.SetCursorPosition(44, 2);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"HP {_gameSession.currentFight.GetEnemyHP():00}");
                Console.ResetColor();
                Console.SetCursorPosition(30, 22);
                Console.WriteLine("Fight");
                Console.CursorLeft = 30;
                Console.WriteLine("Inventory");
                Console.CursorLeft = 30;
                Console.WriteLine("Run");
                Console.SetCursorPosition(25 ,22 + Mod(fightMenuCursor,3));
                Console.WriteLine("-->");
                ConsoleKeyInfo keypress = Console.ReadKey(true);
                ConsoleKey key = keypress.Key;

                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (Mod(fightMenuCursor,3) == 0)
                        {
                            _gameSession.currentFight.TakeTurn();
                        }
                        else if (Mod(fightMenuCursor, 3) == 1)
                        {
                            //check inventory
                        }
                        else if (Mod(fightMenuCursor, 3) == 2)
                        {
                            _gameSession.RunFromFight();
                        }
                        
                        break;
                    case ConsoleKey.UpArrow:
                        fightMenuCursor--;
                        break;
                    case ConsoleKey.DownArrow:
                        fightMenuCursor++;
                        
                        break;
                    case ConsoleKey.P:
                        foreach (Potion pot in potions)
                        {
                            _gameSession.currentPlayer.DrinkPotion(pot);
                        }
                        break;
                }
            }
            StatusBar();
            Thread.Sleep(100);
            PlaySound("Sound/Maze.mp3");
        }
        public static void DrawEnemy()
        {
            foreach (Enemy enemy in _gameSession.enemies)
            {
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
        //public static void ErasePlayer()
        //{
        //    Console.ForegroundColor = _gameSession.currentPlayer.PlayerColor;
        //    Console.SetCursorPosition(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
        //    Console.Write(" ");
        //    Console.ResetColor();
        //}
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
            List<Potion> potions = _gameSession.currentPlayer.inventory.OfType<Potion>().ToList();
            //List<Weapon> weapons = _gameSession.currentPlayer.inventory.OfType<Weapon>().ToList();
            int healthBuff = 0;
            int damageBuff = 0;
            //int wepBuff = 0;
            potions.ForEach(potion => healthBuff += potion.HealEffect);
            potions.ForEach(potion => damageBuff += potion.AttackEffect);
            //weapons.ForEach(weapon => wepBuff += weapon.AttackDamage);

            // Position
            Console.ResetColor();
            Console.SetCursorPosition(5, 31);
            Console.Write("Position: {0} , {1}", _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);


            // Health
            Console.SetCursorPosition(50, 31);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Health: {0}", _gameSession.currentPlayer.HitPoints);

            // Show how much potion adds to health
            Console.Write($" (+{healthBuff})");


            // Attack
            Console.SetCursorPosition(70, 31);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Attack: {0}", _gameSession.currentPlayer.AttackDamage);

            // Show how much weapon and potion adds to damage
            Console.Write($" (+{damageBuff})");


            // Level
            Console.SetCursorPosition(90, 31);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Level: {0}", _gameSession.currentPlayer.level);


            // Experience
            Console.SetCursorPosition(110, 31);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("EXP: {0}", _gameSession.currentPlayer.ExperiencePoints);

            // Logs
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorTop += 2;
            Console.CursorLeft = 0;
            var lastFiveEntries = _gameSession.eventLogs
                .Skip(Math.Max(0, _gameSession.eventLogs.Count() - 5)).ToList();

            //CLEAR the lines where we print the logs before printing them
            Console.SetCursorPosition(50, 34);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorLeft = 50;
                Console.WriteLine();
            }
            Console.SetCursorPosition(50, 34);
            for (int i = 0; i < lastFiveEntries.Count; i++)
            {
                Console.CursorLeft = 50;
                Console.WriteLine(lastFiveEntries[i]);
            }

        }

        public static void PrintAnimation()
        {
            Image Picture = Image.FromFile("loading11.png");

            //Console.WindowWidth = 180;
            //Console.WindowHeight = 61;
            //Console.WindowWidth = 180;
            //Console.WindowHeight = 140;
            //Console.SetBufferSize((Picture.Width * 0x2), (Picture.Height * 0x2));

            FrameDimension Dimension = new FrameDimension(Picture.FrameDimensionsList[0x0]);
            int FrameCount = Picture.GetFrameCount(Dimension);
            int Left = Console.WindowLeft, Top = Console.WindowTop;
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', ' ', ' ' };
            Picture.SelectActiveFrame(Dimension, 0x0);
            for (int i = 0x0; i < Picture.Height; i++)
            {
                Console.Write("\t\t\t\t\t\t      ");
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color Color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    Console.Write(Chars[Index]);
                }
                Console.Write('\n');
                Thread.Sleep(50);
            }
            //Console.SetCursorPosition(Left, Top);
            //Console.Read();
        }
    }
}

