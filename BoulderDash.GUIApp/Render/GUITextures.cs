using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BoulderDash.GUIApp.Render
{
    public class GUITextures
    {
        public Dictionary<Type, Brush> Textures { get; private set; }

        public GUITextures()
        {
            Textures = new Dictionary<Type, Brush>
            {
                { typeof(Player), Brushes.LimeGreen },
                { typeof(Block), Brushes.MediumPurple },
                { typeof(Air), Brushes.Black },
                { typeof(Diamond), Brushes.Yellow },
                { typeof(Rock), Brushes.Gray },
                { typeof(Wall), Brushes.DarkSlateGray },
                { typeof(Exit), Brushes.Cyan },
                { typeof(Bomb), Brushes.Red }
            };
        }

        public Brush GetBrushForType(Type objectType)
        {
            if (Textures.TryGetValue(objectType, out Brush brush))
            {
                return brush;
            }
            return Brushes.Fuchsia;
        }
    }
}
