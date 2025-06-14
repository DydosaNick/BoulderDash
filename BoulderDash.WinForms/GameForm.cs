using BoulderDash.Core;
using BoulderDash.WinForms.Input;
using BoulderDash.WinForms.Render;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoulderDash.WinForms
{
    public partial class GameForm : Form
    {
        private readonly WinFormsInput _input;
        private PictureBox pictureBox1;
        private readonly GameCore _game;

        public GameForm()
        {
            InitializeComponent();

            var renderPanel = new Panel { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.Black };
            Controls.Add(renderPanel);

            var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
            var startButton = new Button { Text = "Start", Width = 80 };
            var exitButton = new Button { Text = "Exit", Width = 80 };

            startButton.Click += (s, e) => Task.Run(() => _game.Run());
            exitButton.Click += (s, e) => Close();

            buttonPanel.Controls.Add(startButton);
            buttonPanel.Controls.Add(exitButton);
            Controls.Add(buttonPanel);

            _input = new WinFormsInput();
            var render = new WinFormsRender(renderPanel);
            _game = new GameCore(render, _input, null);

            this.KeyDown += GameForm_KeyDown;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            _input.HandleKey(e.KeyCode);
        }

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(71, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(482, 306);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // GameForm
            // 
            ClientSize = new System.Drawing.Size(1027, 481);
            Controls.Add(pictureBox1);
            Name = "GameForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }
    }
}