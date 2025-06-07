// WinFormsInput.cs
using BoulderDash.Core.Utilites;
using System;
using System.Windows.Forms;

namespace BoulderDash.WinFormsApp.Input
{
    public class WinFormsInput : GameInput
    {
        private Actions currentAction = Actions.Nothing;
        private Form parentForm;

        public WinFormsInput(Form form)
        {
            parentForm = form;
            parentForm.KeyPreview = true;
            parentForm.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            currentAction = e.KeyCode switch
            {
                Keys.W or Keys.Up => Actions.MoveUp,
                Keys.S or Keys.Down => Actions.MoveDown,
                Keys.A or Keys.Left => Actions.MoveLeft,
                Keys.D or Keys.Right => Actions.MoveRight,
                Keys.Escape => Actions.LeaveGame,
                Keys.Enter => Actions.Interact,
                _ => Actions.Nothing
            };
        }

        public override Actions HandleInput()
        {
            Actions action = currentAction;
            currentAction = Actions.Nothing; // Сбрасываем после получения
            return action;
        }

        public override string MenuInput()
        {
            using (var inputForm = new LevelSelectForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    return inputForm.SelectedLevel;
                }
                return "1"; // По умолчанию первый уровень
            }
        }
    }

    // Форма для выбора уровня
    public class LevelSelectForm : Form
    {
        private string selectedLevel = "1";
        private TextBox levelTextBox;
        private Button okButton;
        private Button cancelButton;
        private Label welcomeLabel;
        private Label levelLabel;

        public string SelectedLevel
        {
            get { return selectedLevel; }
        }

        public LevelSelectForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Boulder Dash - Select Level";
            this.Size = new System.Drawing.Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            welcomeLabel = new Label();
            welcomeLabel.Text = "Welcome to Boulder Dash!";
            welcomeLabel.Location = new System.Drawing.Point(50, 20);
            welcomeLabel.Size = new System.Drawing.Size(200, 20);
            welcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            levelLabel = new Label();
            levelLabel.Text = "From which level you want to start:";
            levelLabel.Location = new System.Drawing.Point(20, 50);
            levelLabel.Size = new System.Drawing.Size(180, 20);

            levelTextBox = new TextBox();
            levelTextBox.Text = "1";
            levelTextBox.Location = new System.Drawing.Point(200, 50);
            levelTextBox.Size = new System.Drawing.Size(50, 20);

            okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new System.Drawing.Point(100, 80);
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += OkButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.Location = new System.Drawing.Point(180, 80);
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.DialogResult = DialogResult.Cancel;

            this.Controls.Add(welcomeLabel);
            this.Controls.Add(levelLabel);
            this.Controls.Add(levelTextBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            selectedLevel = levelTextBox.Text;
        }
    }
}