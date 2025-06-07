using BoulderDash.WinFormsApp;
using BoulderDash.WinFormsApp.Input;
using BoulderDash.WinFormsApp.Render;
using System;
using System.Windows.Forms;

namespace BoulderDash.WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Включаем визуальные стили Windows
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Создаем главную форму игры
                using (var gameForm = new GameForm())
                {
                    // Получаем рендер и ввод из формы
                    var gameRender = gameForm.GameRender;
                    var gameInput = gameForm.GameInput;

                    // Создаем игровой движок (замените на ваш класс)
                    var gameEngine = new GameEngine(gameRender, gameInput);

                    // Показываем форму
                    gameForm.Show();

                    // Запускаем игровой цикл в отдельном потоке или используем Timer
                    var gameTimer = new Timer();
                    gameTimer.Interval = 100; // 10 FPS
                    gameTimer.Tick += (sender, e) =>
                    {
                        try
                        {
                            gameEngine.Update();

                            // Если игра завершена, закрываем приложение
                            if (gameEngine.IsGameOver)
                            {
                                gameTimer.Stop();
                                gameForm.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Game error: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gameTimer.Stop();
                            gameForm.Close();
                        }
                    };

                    gameTimer.Start();

                    // Запускаем основной цикл сообщений Windows
                    Application.Run(gameForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal error: {ex.Message}", "Boulder Dash - Fatal Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Если у вас нет класса GameEngine, вот простая заглушка
    public class GameEngine
    {
        private readonly WinFormsRender render;
        private readonly WinFormsInput input;
        private bool isGameOver = false;

        public bool IsGameOver => isGameOver;

        public GameEngine(WinFormsRender gameRender, WinFormsInput gameInput)
        {
            render = gameRender;
            input = gameInput;
        }

        public void Update()
        {
            // Обрабатываем ввод
            var action = input.HandleInput();

            // Здесь должна быть логика обновления игрового мира
            // Например:
            // gameWorld.ProcessAction(action);
            // render.Render(gameWorld);

            // Временная заглушка - завершаем игру при нажатии Escape
            if (action == BoulderDash.Core.Utilites.Actions.LeaveGame)
            {
                isGameOver = true;
            }
        }
    }
}