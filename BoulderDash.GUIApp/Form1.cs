using BoulderDash.Core;
using BoulderDash.Core.GameObjects;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.GUIApp.Input;
using BoulderDash.GUIApp.Render;
namespace BoulderDash.GUIApp
{
    public partial class Form1 : Form
    {
        private GUIInput guiInput;

        private GUIRender guiRender;

        GameCore gameCore;

        private int tics = 0;

        private readonly GUITextures textures = new();

        private readonly GUIKeys guiKeys = new();

        public Form1()
        {
            InitializeComponent();

            guiRender = new GUIRender(DrawPanel);

            guiInput = new GUIInput(timer1);

            gameCore = new(
                guiRender,
                guiInput,
                new Audio.GUIAudio()
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Paint += DrawPanel_Paint;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Actions PressedKeyAction = Actions.Nothing;

            Keys pressedKey = e.KeyData;

            if (guiKeys.Key.ContainsKey(pressedKey))
            {
                PressedKeyAction = guiKeys.Key[pressedKey];
            }
            else
            {
                return;
            }
            if (pressedKey == Keys.P)
            {
                if (timer1.Enabled)
                {
                    IsGamePausedLabel.Visible = true;
                    timer1.Stop();
                }
                else
                {
                    IsGamePausedLabel.Visible = false;
                    timer1.Start();
                }

            }

            guiInput.AddActionToBuffer(PressedKeyAction);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gameCore.Run();
            ScoreLable.Text = $"Your score is: {Player.DiamondScore}";
            
            tics++;
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            //Start_Button.Enabled = false;
            Start_Button.Visible = false;
            Backgroung.Visible = false;
            DrawPanel.Visible = true;
            this.KeyPreview = true;
            ScoreLable.Visible = true;
            timer1.Start();
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle clip = e.ClipRectangle;
            int cellSize = 32;

            for (int y = 0; y < gameCore.gameWorld.Height; y++)
            {
                for (int x = 0; x < gameCore.gameWorld.Width; x++)
                {
                    GameObject cell = gameCore.gameWorld.Map[x, y];

                    Image texture = textures.GetTexture(cell.GetType());

                    Rectangle cellRect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);

                    if (!clip.IntersectsWith(cellRect)) continue;

                    if (texture != null)
                    {
                        g.DrawImage(texture, cellRect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Gray, cellRect);
                    }
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (gameCore.gameStateConroler.GetCurrentGameState())
            {
                case GameStates.NoEscape:
                case GameStates.PlayerDied:
                    timer1.Stop();
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
