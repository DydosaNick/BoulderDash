using BoulderDash.ConsoleApp.Render;
using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Input
{
    public class ConsoleInput : GameInput
    {
        private Keys keys = new Keys();

        private Messages messages = new Messages();
        public override Actions HandleInput()
        {
            if (!Console.KeyAvailable) return Actions.Nothing;

            try
            {
                var action = keys.KeyBindings[Console.ReadKey(true).Key];
                return action;
            }
            catch (Exception)
            {
                return Actions.Nothing;
            }
        }
            
        public override string MenuInput(GameStates gameState)
        {
            Console.Clear();

            Console.WriteLine(messages.MessagesDictionary[gameState]);

            return Console.ReadLine();
        }
    }
}


