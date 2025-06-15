using BoulderDash.Core.GameObjects;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System.Drawing;
using System.Windows.Forms;
using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;

namespace BoulderDash.GUIApp.Render
{
    public class GUIGameRender : GameRender
    {
        private PictureBox _gamePictureBox;
        private Label _lblScore;
        private Label _lblLevel;
        private Label _lblMessage;

        private GUITextures _guiTextures;
        public GUIMessages Messages { get; private set; }

        private const int TILE_SIZE = 30;

        public GUIGameRender(PictureBox gamePictureBox, Label lblScore, Label lblLevel, Label lblMessage)
        {
            _gamePictureBox = gamePictureBox;
            _lblScore = lblScore;
            _lblLevel = lblLevel;
            _lblMessage = lblMessage;

            _guiTextures = new GUITextures();
            Messages = new GUIMessages();
        }

        public override void Render(GameWorld gameWorld)
        {
            if (gameWorld.Map == null || _gamePictureBox.Width == 0 || _gamePictureBox.Height == 0)
            {
                return;
            }

            Bitmap bmp = new Bitmap(_gamePictureBox.Width, _gamePictureBox.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);

                int offsetX = (_gamePictureBox.Width - (gameWorld.Width * TILE_SIZE)) / 2;
                int offsetY = (_gamePictureBox.Height - (gameWorld.Height * TILE_SIZE)) / 2;

                for (int y = 0; y < gameWorld.Height; y++)
                {
                    for (int x = 0; x < gameWorld.Width; x++)
                    {
                        GameObject obj = gameWorld.Map[x, y];
                        Brush brush = _guiTextures.GetBrushForType(obj.GetType());

                        g.FillRectangle(brush, offsetX + x * TILE_SIZE, offsetY + y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    }
                }
            }
            _gamePictureBox.Image = bmp;
        }

        public override void ShowMessage(GameStates gameStates)
        {
            var (color, message) = Messages.MessagesDictionary[gameStates];
            _lblMessage.Invoke((MethodInvoker)delegate
            {
                _lblMessage.Text = message;
                _lblMessage.ForeColor = color;
            });
        }
    }
}
