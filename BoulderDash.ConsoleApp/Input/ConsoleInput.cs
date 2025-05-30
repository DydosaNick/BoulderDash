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
        public override Actions HandleInput()
        {
            if (!Console.KeyAvailable) return Actions.Nothing;

            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W: case ConsoleKey.UpArrow: return Actions.MoveUp;
                case ConsoleKey.S: case ConsoleKey.DownArrow: return Actions.MoveDown;
                case ConsoleKey.A: case ConsoleKey.LeftArrow: return Actions.MoveLeft; 
                case ConsoleKey.D: case ConsoleKey.RightArrow: return Actions.MoveRight;
                case ConsoleKey.Escape: return Actions.LeaveGame;
                case ConsoleKey.Enter: return Actions.Interact;
            }

            return Actions.Nothing;
        }
            
        public override string MenuInput()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Boulder Dash!");
            Console.WriteLine("From wich level you want to start");

            string input = Console.ReadLine();
            return input;
        }
    }
}


