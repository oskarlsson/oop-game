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

        public static List<string> mainMenu = new List<string>() { "    Spela    ", "Instruktioner", "Inställningar", "   Avsluta   " };
        public static List<string> Submenu = new List<string>() { "  Guld   ", "   Vit    ", "   Blå    ", "   Grå    ", "   Grön   ", "   Cyan   ", "Huvudmenyn" };
        public static List<string> confirmExit = new List<string>() { "    Ja     ", "    Nej    " };
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
            menu("main", mainMenu);

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
        // Menus of the game, menuType can be main for main menu, subMenu for sub menu and confirmExit for exit menu
        public static void menu(string menuType, List<string> menuOptions)
        {
            int y = Console.WindowHeight / 2; //Height positioning
            int x = Console.WindowWidth / 2; // Width positioning
            int pointer = 0;
            Console.Clear();
            Console.ResetColor();
            ASCIIModel title = new ASCIIModel("ASCII/Title.txt");
            FastDraw(title.Buffer);
            showStars();
            Console.CursorVisible = false;
            while (true)
            {
                Console.ResetColor();
                if (menuType == "main")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(x, y - 3);
                    Console.WriteLine("Welcome {0} ", Environment.UserName);
                }
                else
                if (menuType == "subMenu")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(x - 25, y - 5);
                    ConsoleColor color = _gameSession.currentPlayer.PlayerColor; // The current color of the player
                    Console.WriteLine("Nuvarande färgen är             Välj en färg!       Esc = Huvudmenyn");
                    Console.SetCursorPosition(x - 5, y - 5);
                    Console.ForegroundColor = color;
                    Console.Write(color);
                }
                else
                if (menuType == "confirmExit")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(x - 5, y - 2);
                    Console.WriteLine("Vill du sluta spelet?");
                }
                //Creating menu 
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    if (i == pointer)
                    {
                        Console.ResetColor();
                        Console.SetCursorPosition(x, y + i);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(menuOptions[i]);
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(x, y + i);
                        Console.WriteLine(menuOptions[i]);
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo Selection = Console.ReadKey(true); // Reading arrow keys
                ConsoleKey key = Selection.Key;

                switch (key)  // 
                {
                    case ConsoleKey.UpArrow: //Navigation
                        if (pointer <= 0)
                        {
                            pointer = menuOptions.Count - 1;
                        }
                        else
                            pointer--;
                        break;
                    case ConsoleKey.DownArrow:   //Navigation
                        if (pointer == menuOptions.Count - 1)
                        {
                            pointer = 0;

                        }
                        else
                            pointer++;
                        break;
                    case ConsoleKey.Escape: // Exit, if the user use Esc key to exit
                        if (menuType == "main")
                            menu("confirmExit", confirmExit);
                        else
                            if (menuType == "subMenu")
                            menu("main", mainMenu);
                        else
                            if (menuType == "confirmExit")
                            menu("main", mainMenu);
                        break;

                    case ConsoleKey.Enter: // Selection by pressing Enter 
                        switch (menuType)
                        {
                            case "main": // Starting the game

                                switch (pointer)
                                {
                                    case 0:
                                        Console.ResetColor();
                                        GameLoop();
                                        break;

                                    case 1:
                                        Console.Clear();
                                        Console.ResetColor();
                                        ViewInstructions(); // Isstruktion of the game
                                        break;
                                    case 2:
                                        menu("subMenu", Submenu); // Settings
                                        break;

                                    case 3:
                                        menu("confirmExit", confirmExit);
                                        break;
                                }
                                break;
                            case "subMenu":  // Submenu for the settings (Selecting color of player)
                                switch (pointer)
                                {
                                    case 0:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.Yellow;
                                        changeColor_message(ConsoleColor.Yellow);
                                        break;
                                    case 1:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.White;
                                        changeColor_message(ConsoleColor.White);
                                        break;
                                    case 2:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.Blue;
                                        changeColor_message(ConsoleColor.Blue);
                                        break;
                                    case 3:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.Gray;
                                        changeColor_message(ConsoleColor.Gray);
                                        break;
                                    case 4:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.Green;
                                        changeColor_message(ConsoleColor.Green);
                                        break;
                                    case 5:
                                        _gameSession.currentPlayer.PlayerColor = ConsoleColor.Cyan;
                                        changeColor_message(ConsoleColor.Cyan);
                                        break;
                                    case 6:
                                        menu("main", mainMenu);
                                        break;
                                }
                                break;
                            case "confirmExit":
                                if (pointer == 0)
                                    Exit();
                                else
                                    if (pointer == 1)
                                    menu("main", mainMenu);
                                break;
                        }
                        break;
                }
            }
        }
        public static void changeColor_message(ConsoleColor color)  // Color preview messge
        {
            int x = (Console.WindowWidth / 2);
            int y = (Console.WindowHeight / 2);
            Console.SetCursorPosition(x - 10, y + 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Spelarens färg ändrades till ");
            Console.SetCursorPosition(x + 20, y + 10);
            Console.ForegroundColor = color;
            Console.BackgroundColor = color;
            Console.Write("██");
            Thread.Sleep(800);
            Console.ResetColor();
            menu("subMenu", Submenu);
        }
        public static void ViewInstructions() //TDO
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Instruktioner...");
            Console.ReadKey();
            menu("main", mainMenu);
        }

        public static void Exit() // Exit the game
        {
            int x = (Console.WindowWidth / 2), y = 20;
            Console.Clear();
            Console.ResetColor();
            ASCIIModel title = new ASCIIModel("ASCII/Title.txt");
            FastDraw(title.Buffer);
            showStars();
            Console.SetCursorPosition(x - 10, y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Spelet är slut...       ");
            Thread.Sleep(3000);
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
                    menu("main", mainMenu);
                    // Menu("main");
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
                    menu("main", mainMenu);
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

        public static void FightView()
        {
            PlaySound("Sound/Fight.mp3");
            while (_gameSession.inFight)
            {
                List<Potion> potions = _gameSession.currentPlayer.inventory.OfType<Potion>().ToList();
                Console.Clear();
                FastDraw(_gameSession.currentFight.fightScene.Buffer);
                StatusBar();
                ConsoleKeyInfo keypress = Console.ReadKey(true);
                ConsoleKey key = keypress.Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        _gameSession.currentFight.TakeTurn();
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
            List<Weapon> weapons = _gameSession.currentPlayer.inventory.OfType<Weapon>().ToList();
            int healthBuff = 0;
            int damageBuff = 0;
            potions.ForEach(potion => healthBuff += potion.HealEffect);
            potions.ForEach(potion => damageBuff += potion.AttackEffect);
            weapons.ForEach(weapon => damageBuff += weapon.AttackDamage);

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

