using BoulderDash.Core.Controlers;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Render
{
    public class ConsoleWindowControler : WindowControler
    {
        public ConsoleWindowControler()
        {
            WindowWidth = Console.WindowWidth;
            WindowHeight = Console.WindowHeight;
        }
        public void UpdateWindowSize()
        {
            WindowWidth = Console.WindowWidth;
            WindowHeight = Console.WindowHeight;
        }

        public bool HasWindowSizeChanged()
        {
            int newWindowWidth = Console.WindowWidth;
            int newWindowHeight = Console.WindowHeight;

            if (newWindowWidth != WindowWidth || newWindowHeight != WindowHeight)
            {
                return true;
            }

            return false;
        }

        public bool HasWindowSizeSmallerThenWorld(GameWorld gameWorld) 
        {
            if (WindowWidth < gameWorld.Width ||  WindowHeight < gameWorld.Height)
            {
                return true;
            }
            return false;
        }

    }
}
