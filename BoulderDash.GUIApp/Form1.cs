using BoulderDash.Core;
using BoulderDash.Core.Utilites;
using BoulderDash.GUIApp.Controlers;
using BoulderDash.GUIApp.Render;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BoulderDash.Core.World;
using BoulderDash.Core.GameObjects.Entities;
using static System.Windows.Forms.Design.AxImporter;

namespace BoulderDash.GUIApp
{
    public partial class GameForm : Form
    {
        private GameCore _gameCore;
        private GUIGameInput _guiGameInput;
        private GUIGameRender _guiGameRender;
        private GUIGameAudio _guiGameAudio;

        private System.Windows.Forms.Timer _gameTimer;

        private Label lblScore;
        private Label lblLevel;
        private Label lblMessage;
        private Panel pnlMenu;
        private TextBox txtLevelInput;
        private Button btnStartGame;
        private PictureBox gamePictureBox;

        private bool _isGameRunning = false;

        public GameForm()
        {
            InitializeComponent();
            SetupGameUI();
            InitializeGameCore();
            SetupGameTimer();
            SubscribeToEvents();
            ShowMenu();
        }

        private void SetupGameUI()
        {
            this.Text = "Boulder Dash";
            this.ClientSize = new Size(800, 600);
            this.DoubleBuffered = true;

            gamePictureBox = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(780, 500),
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };
            this.Controls.Add(gamePictureBox);

            lblScore = new Label { Text = "Score: 0", Location = new Point(10, 520), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), ForeColor = Color.White };
            lblLevel = new Label { Text = "Level: 1", Location = new Point(150, 520), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold), ForeColor = Color.White };
            lblMessage = new Label { Text = "Welcome to Boulder Dash!", Location = new Point(10, 550), AutoSize = true, Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = Color.LightBlue };

            this.Controls.Add(lblScore);
            this.Controls.Add(lblLevel);
            this.Controls.Add(lblMessage);

            pnlMenu = new Panel
            {
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 200) / 2),
                Size = new Size(300, 200),
                BackColor = Color.DarkGray,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = true
            };
            this.Controls.Add(pnlMenu);

            Label lblMenuTitle = new Label { Text = "Select Level:", Location = new Point(80, 30), AutoSize = true, Font = new Font("Arial", 16, FontStyle.Bold), ForeColor = Color.Yellow };
            txtLevelInput = new TextBox { Location = new Point(50, 80), Size = new Size(200, 30), Text = "1", TextAlign = HorizontalAlignment.Center };
            btnStartGame = new Button { Text = "Start Game", Location = new Point(100, 130), Size = new Size(100, 40) };

            pnlMenu.Controls.Add(lblMenuTitle);
            pnlMenu.Controls.Add(txtLevelInput);
            pnlMenu.Controls.Add(btnStartGame);

            this.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void InitializeGameCore()
        {
            _guiGameInput = new GUIGameInput();
            _guiGameRender = new GUIGameRender(gamePictureBox, lblScore, lblLevel, lblMessage);
            _guiGameAudio = new GUIGameAudio();

            _gameCore = new GameCore(_guiGameRender, _guiGameInput, _guiGameAudio);
            _gameCore.gameWorld.CheckMaxLevel();
        }

        private void SetupGameTimer()
        {
            _gameTimer = new System.Windows.Forms.Timer();
            _gameTimer.Interval = 80;
            _gameTimer.Tick += GameLoop_Tick;
        }

        private void SubscribeToEvents()
        {
            this.KeyDown += GameForm_KeyDown;
            gamePictureBox.Paint += GamePictureBox_Paint;
            btnStartGame.Click += BtnStartGame_Click;
            this.Resize += GameForm_Resize;
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            pnlMenu.Location = new Point((this.ClientSize.Width - pnlMenu.Width) / 2, (this.ClientSize.Height - pnlMenu.Height) / 2);

            gamePictureBox.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 100);
            gamePictureBox.Location = new Point(10, 10);

            lblScore.Location = new Point(10, this.ClientSize.Height - 70);
            lblLevel.Location = new Point(150, this.ClientSize.Height - 70);
            lblMessage.Location = new Point(10, this.ClientSize.Height - 40);

            gamePictureBox.Invalidate();
        }

        private void ShowMenu()
        {
            pnlMenu.Visible = true;
            gamePictureBox.Visible = false;
            lblScore.Visible = false;
            lblLevel.Visible = false;
            lblMessage.Text = _guiGameRender.Messages.MessagesDictionary[GameStates.Menu].Item2;
            lblMessage.ForeColor = _guiGameRender.Messages.MessagesDictionary[GameStates.Menu].Item1;
            lblMessage.Visible = true;
            _gameTimer.Stop();
            _isGameRunning = false;
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLevelInput.Text, out int level) && level > 0 && level <= _gameCore.gameWorld.MaxLevel)
            {
                _gameCore.gameWorld.LoadLevel(level);
                StartGame();
            }
            else
            {
                lblMessage.Text = _guiGameRender.Messages.MessagesDictionary[GameStates.InvalidInput].Item2;
                lblMessage.ForeColor = _guiGameRender.Messages.MessagesDictionary[GameStates.InvalidInput].Item1;
            }
        }

        private void StartGame()
        {
            pnlMenu.Visible = false;
            gamePictureBox.Visible = true;
            lblScore.Visible = true;
            lblLevel.Visible = true;
            lblMessage.Visible = true;
            _isGameRunning = true;
            _gameTimer.Start();
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            if (!_isGameRunning) return;

            GameStates currentGameState = _gameCore.gameStateConroler.CurrentGameState;

            switch (currentGameState)
            {
                case GameStates.Playing:
                    _gameCore.GameLoop();
                    gamePictureBox.Invalidate();
                    UpdateGameInfoUI();
                    break;
                case GameStates.LevelCompleted:
                    _gameCore.gameRender.ShowMessage(currentGameState);
                    if (_gameCore.gameWorld.CurrentLevel >= _gameCore.gameWorld.MaxLevel)
                    {
                        _gameCore.gameStateConroler.ChangeGameState(GameStates.GameCompleted);
                    }
                    else
                    {
                        _gameCore.gameWorld.LoadNextLevel();
                        _gameCore.gameStateConroler.ChangeGameState(GameStates.Playing);
                        gamePictureBox.Invalidate();
                    }
                    break;
                case GameStates.GameCompleted:
                case GameStates.PlayerDied:
                case GameStates.PlayerExitByHimself:
                case GameStates.NoEscape:
                case GameStates.InvalidInput:
                    _gameCore.gameRender.ShowMessage(currentGameState);
                    _gameTimer.Stop();
                    _isGameRunning = false;
                    ShowMenu();
                    break;
                default:
                    break;
            }
        }

        private void UpdateGameInfoUI()
        {
            lblScore.Text = $"Score: {Player.DiamondScore}";
            lblLevel.Text = $"Level: {_gameCore.gameWorld.CurrentLevel}";
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_isGameRunning && e.KeyCode != Keys.Escape) return;

            Actions action = Actions.Nothing;
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    action = Actions.MoveUp;
                    break;
                case Keys.S:
                case Keys.Down:
                    action = Actions.MoveDown;
                    break;
                case Keys.A:
                case Keys.Left:
                    action = Actions.MoveLeft;
                    break;
                case Keys.D:
                case Keys.Right:
                    action = Actions.MoveRight;
                    break;
                case Keys.Enter:
                    action = Actions.Interact;
                    break;
                case Keys.Escape:
                    if (_isGameRunning)
                    {
                        action = Actions.LeaveGame;
                        _gameCore.gameStateConroler.EvaluateGameStateActions(action);
                    }
                    else
                    {
                    }
                    break;
            }

            _guiGameInput.SetNextAction(action);
        }

        private void GamePictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_gameCore?.gameWorld?.Map == null || !_isGameRunning) return;

            _guiGameRender.Render(_gameCore.gameWorld);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _gameTimer.Stop();
            _gameTimer.Dispose();
            base.OnFormClosing(e);
        }
    }
}
