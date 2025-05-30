using BoulderDash.ConsoleApp.Audio;
using BoulderDash.ConsoleApp.Input;
using BoulderDash.ConsoleApp.Render;
using BoulderDash.Core;
using BoulderDash.Core.GameObjects;

namespace BoulderDash.ConsoleApp
{
    internal class Program
    {
        public static void Main()
        {            
            new GameCore(new ConsoleRender(), new ConsoleInput(), new ConsoleAudio()).Run();
        }
    }
}
