using BoulderDash.Core.Utilites;
using System.Windows.Forms;

namespace BoulderDash.WinForms.Input
{
    public class WinFormsInput : GameInput
    {
        private Actions _currentAction = Actions.Nothing;

        public void HandleKey(Keys key)
        {
            _currentAction = key switch
            {
                Keys.W or Keys.Up => Actions.MoveUp,
                Keys.S or Keys.Down => Actions.MoveDown,
                Keys.A or Keys.Left => Actions.MoveLeft,
                Keys.D or Keys.Right => Actions.MoveRight,
                Keys.Enter => Actions.Interact,
                Keys.Escape => Actions.LeaveGame,
                _ => Actions.Nothing,
            };
        }

        public override Actions HandleInput()
        {
            var action = _currentAction;
            _currentAction = Actions.Nothing;
            return action;
        }

        public override string MenuInput(GameStates gameStates)
        {
            return "1";
        }
    }
}