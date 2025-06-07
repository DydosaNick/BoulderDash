using BoulderDash.Core.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Render
{
    public class ConsoleTextures
    {
        public Dictionary<Type, (ConsoleColor ,char)> Textures { get; private set; }
    }
}
