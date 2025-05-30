using BoulderDash.Core.Controlers;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core
{
    public  class GameCore
    {
        public GameCore(GameRender gameRender, GameInput gameInput, GameAudio gameAudio) 
        {
            this.gameRender = gameRender;
            this.gameInput = gameInput;
            this.gameAudio = gameAudio;
        }

        public GameRender gameRender;
        public GameInput gameInput;
        public GameAudio gameAudio;
        public GameWorld gameWorld = new();
        public PlayerControler playerControler = new();
        public GameStateConroler gameStateConroler = new();        

        public void Run()
        {
            gameStateConroler.ControlGameState(this);
        }

        public void GameLoop()
        {
            Actions key = gameInput.HandleInput();
            gameWorld.UpdateWorld(key, playerControler, gameWorld, gameStateConroler);
            gameRender.Render(gameWorld);
            gameWorld.UpdatePreviousMap();
            Thread.Sleep(100);
        }        
    }
}
