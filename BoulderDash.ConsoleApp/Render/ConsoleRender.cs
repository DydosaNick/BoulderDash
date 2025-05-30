using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;

namespace BoulderDash.ConsoleApp.Render
{
    public class ConsoleRender : GameRender
    {
        ConsoleWindowControler windowControler = new();
        public override void Render(GameWorld gameMap)
        {
            Console.CursorVisible = false;

            int newWidth = Console.WindowWidth;
            int newHeight = Console.WindowHeight;

            if (newWidth != windowControler.WindowWidth ||
                newHeight != windowControler.WindowHeight)
            {
                if (newHeight == 0 || newWidth == 0)
                    return;
                windowControler.UpdateWindowSize();
                gameMap.PreviousMap = null;
                Console.Clear();
            }



            if (gameMap.PreviousMap == null)
            {
                Console.Clear();
            }

            for (int x = 0; x < gameMap.Width; x++)
            {
                for ( int y = 0; y < gameMap.Height; y++)
                {
                    if (newWidth < gameMap.Width ||
                        newHeight < gameMap.Height)
                    {
                        Console.Clear();
                        return;
                    }

                    if (gameMap.PreviousMap == null ||
                    gameMap.PreviousMap[x, y] != gameMap.Map[x, y])
                    {
                        Console.SetCursorPosition((x + (windowControler.WindowWidth - gameMap.Width) / 2), (y + (windowControler.WindowHeight - gameMap.Height) / 2));

                        if (gameMap.Map[x, y] is Player)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("@");
                        }
                        else if (gameMap.Map[x, y] is Rock)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("0");
                        }
                        else if (gameMap.Map[x, y] is Diamond)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*");
                        }
                        else if (gameMap.Map[x, y] is Wall)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("#");
                        }
                        else if (gameMap.Map[x, y] is Air)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ");
                        }
                        else if (gameMap.Map[x, y] is Exit)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("E");
                        }
                        else if (gameMap.Map[x, y] is Block)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("X");
                        }
                        else if (gameMap.Map[x, y] is Bomb)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("B");
                        }
                        else
                            Console.ResetColor();
                    }

                    Console.ResetColor();
                }
            }
        }

        public override void ShowMessage(GameStates gameStates)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            
            switch (gameStates)
            {
                case GameStates.PlayerDied:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You died! Press any key to continue...");
                    break;
                case GameStates.PlayerExitByHimself:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("You exited the game! Press any key to continue...");
                    break;
                case GameStates.LevelCompleted:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Level completed! Press any key to continue...");
                    break;
                case GameStates.GameCompleted:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Congratulations! You completed the game!");
                    Console.WriteLine($"Your diamond score is {Player.DiamondScore}");
                    break;
                case GameStates.NoEscape:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You can't escape from this level. You loose! Press any key to continue...");
                    break;
                case GameStates.InvalidInput:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Press any key to continue...");
                    break;
                default:
                    return;
            }

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
