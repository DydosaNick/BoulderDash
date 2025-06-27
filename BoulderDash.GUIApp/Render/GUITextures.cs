using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.GUIApp.Render
{
    public class GUITextures
    {
        public Dictionary<Type, Image> Textures = new()
        {
            { typeof(Player), Image.FromFile("./Images/Player.png") },
            { typeof(Block), Image.FromFile("./Images/Block.png") },
            { typeof(Air),  Image.FromFile("./Images/Air.png")},
            { typeof(Diamond), Image.FromFile("./Images/Diamond.png")},
            { typeof(Rock), Image.FromFile("./Images/Rock.png") },
            { typeof(Wall), Image.FromFile("./Images/Wall.png") },
            { typeof(Exit), Image.FromFile("./Images/Exit.png")},
            { typeof(Bomb), Image.FromFile("./Images/Bomb.png")},
        };

        public Image GetTexture(Type type)
        {
            if (Textures.ContainsKey(type))
            {
                return Textures[type];
            }
            else
            {
                throw new ArgumentException($"Texture for type {type.Name} not found.");
            }
        }
    }
}
