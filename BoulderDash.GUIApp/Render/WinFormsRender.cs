// WinFormsRender.cs
using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoulderDash.WinFormsApp.Render
{
    public class WinFormsRender : GameRender
    {
        private PictureBox gameCanvas;
        private const int CellSize = 20; // Размер одной ячейки в пикселях
        private Bitmap gameBitmap;
        private Graphics gameGraphics;

        public WinFormsRender(PictureBox canvas)
        {
            gameCanvas = canvas;
        }

        public override void Render(GameWorld gameMap)
        {
            // Создаем или пересоздаем битмап если размер изменился
            if (gameBitmap == null ||
                gameBitmap.Width != gameMap.Width * CellSize ||
                gameBitmap.Height != gameMap.Height * CellSize)
            {
                gameBitmap?.Dispose();
                gameGraphics?.Dispose();

                gameBitmap = new Bitmap(gameMap.Width * CellSize, gameMap.Height * CellSize);
                gameGraphics = Graphics.FromImage(gameBitmap);
                gameMap.PreviousMap = null; // Принудительно перерисовываем всё
            }

            // Обновляем размер PictureBox
            gameCanvas.Size = new Size(gameMap.Width * CellSize, gameMap.Height * CellSize);

            // Рисуем только изменившиеся ячейки для оптимизации
            for (int x = 0; x < gameMap.Width; x++)
            {
                for (int y = 0; y < gameMap.Height; y++)
                {
                    if (gameMap.PreviousMap == null || gameMap.PreviousMap[x, y] != gameMap.Map[x, y])
                    {
                        DrawCell(x, y, gameMap.Map[x, y]);
                    }
                }
            }

            // Обновляем изображение в PictureBox
            gameCanvas.Image = gameBitmap;
        }

        private void DrawCell(int x, int y, object gameObject)
        {
            Rectangle cellRect = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);
            Color cellColor = GetColorForObject(gameObject);
            string cellSymbol = GetSymbolForObject(gameObject);

            // Заливаем фон
            using (Brush brush = new SolidBrush(cellColor))
            {
                gameGraphics.FillRectangle(brush, cellRect);
            }

            // Рисуем символ
            if (!string.IsNullOrEmpty(cellSymbol) && cellSymbol != " ")
            {
                using (Font font = new Font("Consolas", 14, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    gameGraphics.DrawString(cellSymbol, font, textBrush, cellRect, format);
                }
            }

            // Рисуем границу ячейки
            using (Pen pen = new Pen(Color.Black, 1))
            {
                gameGraphics.DrawRectangle(pen, cellRect);
            }
        }

        private Color GetColorForObject(object gameObject)
        {
            return gameObject switch
            {
                Player => Color.LightGreen,
                Rock => Color.DarkRed,
                Diamond => Color.Gold,
                Wall => Color.Gray,
                Air => Color.Black,
                Exit => Color.Cyan,
                Block => Color.Magenta,
                Bomb => Color.Orange,
                _ => Color.White
            };
        }

        private string GetSymbolForObject(object gameObject)
        {
            return gameObject switch
            {
                Player => "@",
                Rock => "O",
                Diamond => "♦",
                Wall => "█",
                Air => " ",
                Exit => "E",
                Block => "X",
                Bomb => "B",
                _ => "?"
            };
        }

        public override void ShowMessage(GameStates gameStates)
        {
            string message = gameStates switch
            {
                GameStates.PlayerDied => "You died! Press OK to continue...",
                GameStates.PlayerExitByHimself => "You exited the game! Press OK to continue...",
                GameStates.LevelCompleted => "Level completed! Press OK to continue...",
                GameStates.GameCompleted => $"Congratulations! You completed the game!\nYour diamond score is {Player.DiamondScore}",
                GameStates.NoEscape => "You can't escape from this level. You lose! Press OK to continue...",
                GameStates.InvalidInput => "Invalid input! Press OK to continue...",
                _ => ""
            };

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Boulder Dash", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Dispose()
        {
            gameGraphics?.Dispose();
            gameBitmap?.Dispose();
        }
    }
}