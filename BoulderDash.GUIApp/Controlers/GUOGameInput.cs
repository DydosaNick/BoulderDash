using BoulderDash.Core.Utilites;
using System.Threading;

namespace BoulderDash.GUIApp.Controlers
{
    public class GUIGameInput : GameInput
    {
        private Actions _nextAction = Actions.Nothing;
        private readonly object _actionLock = new object();

        public void SetNextAction(Actions action)
        {
            lock (_actionLock)
            {
                _nextAction = action;
            }
        }

        public override Actions HandleInput()
        {
            lock (_actionLock)
            {
                Actions currentAction = _nextAction;
                _nextAction = Actions.Nothing;
                return currentAction;
            }
        }

        public override string MenuInput(GameStates gameStates)
        {
            return string.Empty;
        }
    }
}
