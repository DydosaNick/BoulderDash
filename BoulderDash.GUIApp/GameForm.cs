using BoulderDash.WinFormsApp.Input;
using BoulderDash.WinFormsApp.Render;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoulderDash.WinFormsApp
{
    public class GameForm : Form
    {
        private PictureBox gameCanvas;
        private WinFormsRender gameRender;
        private WinFormsInput gameInput;

        public WinFormsRender GameRender => gameRender;
        public WinFormsInput GameInput => gameInput;

        public GameForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            this.Text = "Boulder Dash";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Создаем PictureBox для игрового поля
            gameCanvas = new PictureBox()
            {
                Location = new Point(10, 10),
                Size = new Size(760, 540),
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(gameCanvas);

            // Делаем форму фокусируемой для обработки клавиатуры
            this.KeyPreview = true;
            this.TabStop = true;
        }

        private void InitializeGame()
        {
            gameRender = new WinFormsRender(gameCanvas);
            gameInput = new WinFormsInput(this);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            gameRender?.Dispose();
            base.OnFormClosed(e);
        }

        // Обеспечиваем фокус на форме при клике
        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseClick(e);
        }
    }
}