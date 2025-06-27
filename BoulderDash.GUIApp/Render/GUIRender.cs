using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BoulderDash.Core.GameObjects;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using BoulderDash.GUIApp;

namespace BoulderDash.GUIApp.Render
{
    public class GUIRender : GameRender
    {
        private readonly Panel drawPanel;
        public GUIRender(Panel panel)
        {
            drawPanel = panel;
        }

        public override char Id { get; set; } = 'G';

        private readonly GUITextures textures = new();

        public override void Render(GameWorld gameWorld)
        {
            int cellSize = 32;
            drawPanel.Location = new Point(0, 0);

            drawPanel.Size = new Size(1280, 704);
            for (int i = 0; i < gameWorld.Width; i++)
            {
                for (int j = 0; j < gameWorld.Height; j++)
                {
                    if (gameWorld.PreviousMap == null)
                    {
                        drawPanel.Invalidate();
                        return;
                    }

                    if ( gameWorld.Map[i,j] != gameWorld.PreviousMap[i, j])
                    {
                        Rectangle rect = new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize);
                        drawPanel.Invalidate(rect);
                    }
                }
            }
        }
        public override void ShowMessage(GameStates gameStates)
        {
        }
    }
}

