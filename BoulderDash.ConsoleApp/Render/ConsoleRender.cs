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

        Messages messages = new Messages();

        private bool isNeedToRewrite = false;

        public override char Id { get ; set; } = 'C';

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

            int newCursorX = (Console.WindowWidth - gameWorld.Width) / 2;
            int newCursorY = (Console.WindowHeight - gameWorld.Height) / 2;

            for (int x = 0; x < gameWorld.Width; x++)
            {
                for (int y = 0; y < gameWorld.Height; y++)
                {
                    if ((newCursorX <= windowControler.WindowWidth && newCursorY <= windowControler.WindowHeight) ||
                       gameWorld.PreviousMap == null || gameWorld.PreviousMap[x, y] != gameWorld.Map[x, y] || isNeedToRewrite)
                    {
                        try
                        {
                            Console.SetCursorPosition(newCursorX + x, newCursorY + y);
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

            Thread.Sleep(20);
        }

        public override void ShowMessage(GameStates gameState)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            var (foregroundColor, message) = messages.MessagesDictionary[gameState];

            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);

            Thread.Sleep(1000);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
