using BoulderDash.Core.Controlers;
using BoulderDash.Core.GameObjects;
using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.World
{
    public class GameWorld
    {
        public GameObject[,]? Map;

        public GameObject[,]? PreviousMap;

        public int Width { get; private set; }
        public int Height { get; private set; }

        private string LevelString = "./Levels/";

        public bool playerFound = false;

        public int CurrentLevel { get; set; } = 1;

        public int MaxLevel { get; set; } = 0;
        public GameWorld() { }
        public GameWorld(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new GameObject[width, height];
            PreviousMap = new GameObject[width, height];
        }

        public string GetLevelString(string str)
        {
            return $"{LevelString}level{str}.txt";
        }

        private string GetCurrentLevelString()
        {
            return $"{LevelString}level{CurrentLevel}.txt";
        }

        public void CheckMaxLevel()
        {
            int maxLevel = 1;
            while (File.Exists(GetLevelString($"{maxLevel}")))
            {
                maxLevel++;
            }

            MaxLevel = maxLevel - 1;
        }

        public void UpdateWorld(Actions key, PlayerControler playerControler, GameWorld gameWorld, GameStateConroler gameStateConroler)
        {
            playerControler.PlayerAction(key, gameWorld, gameStateConroler);

            gameStateConroler.CheckOrChangeGameState(key);

            UpdateMap(gameStateConroler);
        }

        public void LoadLevel(int x)
        {
            CurrentLevel = x;
            LoadCurrentLevel();
            PreviousMap = null;
        }

        public void LoadNextLevel()
        {
            CurrentLevel++;
            LoadCurrentLevel();
            PreviousMap = null;
        }

        public void LoadCurrentLevel()
        {
            playerFound = false;
            string[] lines = File.ReadAllLines(GetCurrentLevelString());
            Height = lines.Length;
            Width = lines.Max(l => l.Length);
            Map = new GameObject[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char c = x < lines[y].Length ? lines[y][x] : ' ';
                    if (c == '@')
                    {
                        if (playerFound)
                        {
                            Map[x, y] = CreateGameObject(' ', x, y);
                            continue;
                        }
                        else
                        {
                            playerFound = true;
                        }
                    }

                    Map[x, y] =  CreateGameObject(c, x, y);
                }
            }
        }
        public GameObject CreateGameObject(char obj, int x, int y)
        {
            switch (obj)
            {
                case '0':
                    return new Rock(x, y);
                case '*':
                    return new Diamond(x, y);
                case '#':
                    return new Wall(x, y);
                case '@':
                    return new Player(x, y);
                case ' ':
                    return new Air(x, y);
                case 'B':
                    return new Bomb(x, y);
                case 'E':
                    return new Exit(x, y);
                case 'X':
                    return new Block(x, y);
                default:
                    return new Air(x, y);
            }
        }

        public void UpdateMap(GameStateConroler gameStateConroler)
        {
            bool isExitFound = false;
            for (int x = 0; x < Width; x++)
            {
                for (int y = Height - 1; y >= 0; y--)
                {
                    if(Map[x, y] is Entity entity && Map[x, y] is not Player)
                    {
                        entity.Action(this, Map[x,y].Position);
                    }
                    if (Map[x, y] is Exit exit)
                    {
                        isExitFound = true;
                    }
                }
            }

            if (!isExitFound)
            {
                gameStateConroler.ChangeGameState(GameStates.NoEscape);
            }
        }

        public void UpdatePreviousMap()
        {
            PreviousMap = new GameObject[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    PreviousMap[x, y] = Map[x, y];
                }
            }
        }

    }
}
