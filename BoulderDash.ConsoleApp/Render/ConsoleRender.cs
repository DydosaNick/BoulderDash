using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;

namespace BoulderDash.ConsoleApp.Render
{
    public class ConsoleRender : GameRender
    {
        ConsoleWindowControler windowControler = new();

        ConsoleTextures consoleTextures = new ConsoleTextures();

        private bool isNeedToRewrite = false;

        public override void Render(GameWorld gameWorld)
        {

            if (gameWorld.PreviousMap == null)
            {
                isNeedToRewrite = true;
                Console.Clear();
            }

            if (windowControler.HasWindowSizeChanged())
            {
                isNeedToRewrite = true;
                Console.Clear();
            }

            windowControler.UpdateWindowSize();

            if (windowControler.HasWindowSizeSmallerThenWorld(gameWorld))
            {
                isNeedToRewrite = true;
                Console.Clear();
                return;
            }

            Console.CursorVisible = false;

            for (int x = 0; x < gameWorld.Width; x++)
            {
                for (int y = 0; y < gameWorld.Height; y++)
                {

                    int newCursorX = x + (Console.WindowWidth - gameWorld.Width) / 2;
                    int newCursorY = y + (Console.WindowHeight - gameWorld.Height) / 2;

                    if ((newCursorX <= windowControler.WindowWidth && newCursorY <= windowControler.WindowHeight) ||
                       gameWorld.PreviousMap == null || gameWorld.PreviousMap[x, y] != gameWorld.Map[x, y] || isNeedToRewrite)
                    {
                        try
                        {
                            Console.SetCursorPosition(newCursorX, newCursorY);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            continue;
                        }

                        var (color, texture) = consoleTextures.Textures[gameWorld.Map[x, y].GetType()];

                        Console.ForegroundColor = color;
                        Console.Write(texture);                      

                        Console.ResetColor();
                    }
                }

                isNeedToRewrite = false;
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
