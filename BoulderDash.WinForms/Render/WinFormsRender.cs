using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BoulderDash.WinForms.Render
{
    public class WinFormsRender : GameRender
    {
        private readonly Panel _panel;
        private readonly Dictionary<Type, Brush> _brushes = new()
        {
            { typeof(Player), Brushes.Green },
            { typeof(Block), Brushes.Magenta },
            { typeof(Air), Brushes.Black },
            { typeof(Diamond), Brushes.Yellow },
            { typeof(Rock), Brushes.Red },
            { typeof(Wall), Brushes.Blue },
            { typeof(Exit), Brushes.Cyan },
            { typeof(Bomb), Brushes.Gray },
        };

        public WinFormsRender(Panel panel)
        {
            _panel = panel;
            _panel.Paint += Panel_Paint;
        }

        private GameWorld _gameWorld;

        public override void Render(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;
            _panel.Invalidate();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (_gameWorld == null) return;

            const int cellSize = 20;
            for (int x = 0; x < _gameWorld.Width; x++)
            {
                for (int y = 0; y < _gameWorld.Height; y++)
                {
                    var obj = _gameWorld.Map[x, y];
                    var brush = _brushes.TryGetValue(obj.GetType(), out var b) ? b : Brushes.White;
                    e.Graphics.FillRectangle(brush, x * cellSize, y * cellSize, cellSize, cellSize);
                }
            }
        }

        public override void ShowMessage(GameStates gameStates)
        {
            MessageBox.Show(gameStates.ToString(), "Game Message");
        }
    }
}