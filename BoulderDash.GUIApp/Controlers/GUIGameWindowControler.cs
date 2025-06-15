using BoulderDash.Core.Controlers;
using System.Windows.Forms;

namespace BoulderDash.GUIApp.Controlers
{
    public class GUIGameWindowControler : WindowControler
    {
        public GUIGameWindowControler()
        {
            WindowWidth = Screen.PrimaryScreen.WorkingArea.Width;
            WindowHeight = Screen.PrimaryScreen.WorkingArea.Height;
        }

        public void UpdateWindowSize()
        {
            WindowWidth = Screen.PrimaryScreen.WorkingArea.Width;
            WindowHeight = Screen.PrimaryScreen.WorkingArea.Height;
        }

        public bool HasWindowSizeChanged(int currentClientWidth, int currentClientHeight)
        {
            if (currentClientWidth != WindowWidth || currentClientHeight != WindowHeight)
            {
                WindowWidth = currentClientWidth;
                WindowHeight = currentClientHeight;
                return true;
            }
            return false;
        }

        public bool HasWindowSizeSmallerThenWorld(BoulderDash.Core.World.GameWorld gameWorld)
        {
            if (WindowWidth < gameWorld.Width || WindowHeight < gameWorld.Height)
            {
                return true;
            }
            return false;
        }
    }
}
