using BoulderDash.Core.GameObjects;
using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Render
{
    public class ConsoleTextures
    {
        public Dictionary <Type, (ConsoleColor, char)> Textures
        {
            get
            {
                return new Dictionary<Type, (ConsoleColor, char)>()
                {
                    { typeof(Player), (ConsoleColor.Green, '@') },
                    { typeof(Block), (ConsoleColor.Magenta, 'X') },
                    { typeof(Air), (ConsoleColor.White, ' ') },
                    { typeof(Diamond), (ConsoleColor.Yellow, '*') },
                    { typeof(Rock), (ConsoleColor.Red, '0') },
                    { typeof(Wall), (ConsoleColor.DarkBlue, '#') },
                    { typeof(Exit), (ConsoleColor.Cyan, 'E') },
                    { typeof(Bomb), (ConsoleColor.Cyan, 'B') },
                };
            }
        }
    }
}
