namespace BoulderDash.GUIApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            timer1 = new System.Windows.Forms.Timer(components);
            Start_Button = new Button();
            Backgroung = new PictureBox();
            DrawPanel = new Panel();
            IsGamePausedLabel = new Label();
            ScoreLable = new Label();
            ((System.ComponentModel.ISupportInitialize)Backgroung).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Interval = 40;
            timer1.Tick += timer1_Tick;
            // 
            // Start_Button
            // 
            Start_Button.BackColor = SystemColors.Info;
            Start_Button.FlatStyle = FlatStyle.Popup;
            Start_Button.Font = new Font("Segoe UI Symbol", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Start_Button.Location = new Point(966, 514);
            Start_Button.Name = "Start_Button";
            Start_Button.Size = new Size(124, 55);
            Start_Button.TabIndex = 0;
            Start_Button.Text = "Start";
            Start_Button.UseVisualStyleBackColor = true;
            Start_Button.Click += Start_Button_Click;
            // 
            // Backgroung
            // 
            Backgroung.BackColor = SystemColors.ControlDark;
            Backgroung.BackgroundImageLayout = ImageLayout.Center;
            Backgroung.Image = (Image)resources.GetObject("Backgroung.Image");
            Backgroung.Location = new Point(-21, 192);
            Backgroung.Name = "Backgroung";
            Backgroung.Size = new Size(1280, 704);
            Backgroung.SizeMode = PictureBoxSizeMode.StretchImage;
            Backgroung.TabIndex = 1;
            Backgroung.TabStop = false;
            // 
            // DrawPanel
            // 
            DrawPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DrawPanel.AutoSize = true;
            DrawPanel.Location = new Point(1170, 12);
            DrawPanel.Name = "DrawPanel";
            DrawPanel.Size = new Size(0, 74);
            DrawPanel.TabIndex = 2;
            DrawPanel.Visible = false;
            DrawPanel.Paint += DrawPanel_Paint;
            // 
            // IsGamePausedLabel
            // 
            IsGamePausedLabel.BackColor = SystemColors.ControlText;
            IsGamePausedLabel.CausesValidation = false;
            IsGamePausedLabel.Font = new Font("Segoe UI Symbol", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            IsGamePausedLabel.ForeColor = SystemColors.MenuHighlight;
            IsGamePausedLabel.Location = new Point(0, 0);
            IsGamePausedLabel.Name = "IsGamePausedLabel";
            IsGamePausedLabel.Size = new Size(166, 31);
            IsGamePausedLabel.TabIndex = 0;
            IsGamePausedLabel.Text = "Game paused";
            IsGamePausedLabel.UseMnemonic = false;
            // 
            // ScoreLable
            // 
            ScoreLable.AutoSize = true;
            ScoreLable.Font = new Font("Segoe UI Emoji", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ScoreLable.ForeColor = SystemColors.Highlight;
            ScoreLable.Location = new Point(200, 0);
            ScoreLable.Name = "ScoreLable";
            ScoreLable.Size = new Size(27, 31);
            ScoreLable.TabIndex = 3;
            ScoreLable.Text = "1";
            ScoreLable.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1280, 704);
            Controls.Add(ScoreLable);
            Controls.Add(IsGamePausedLabel);
            Controls.Add(DrawPanel);
            Controls.Add(Start_Button);
            Controls.Add(Backgroung);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            ((System.ComponentModel.ISupportInitialize)Backgroung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private Button Start_Button;
        private PictureBox Backgroung;
        private Panel DrawPanel;
        private Label IsGamePausedLabel;
        private Label ScoreLable;
    }
}
