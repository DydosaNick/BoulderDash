namespace BoulderDash.GUIApp
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Name = "GameForm";
            Text = "Boulder Dash";
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        #endregion
    }
}
