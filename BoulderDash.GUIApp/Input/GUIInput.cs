using BoulderDash.Core.Utilites;
using Timer = System.Windows.Forms.Timer;

namespace BoulderDash.GUIApp.Input
{
    public class GUIInput : GameInput
    {
        public GUIInput(Timer timer)
        {
            timer1 = timer;
        }

        private List<Actions> LastActions = new();

        Timer timer1;

        public void AddActionToBuffer(Actions action)
        {
            LastActions.Add(action);
        }

        public override Actions HandleInput()
        {

            if (LastActions.Count == 0)
            {
                return Actions.Nothing;
            }
            var firstEl = LastActions.First();

            LastActions.RemoveAt(0);

            return firstEl;
        }
    }
}
