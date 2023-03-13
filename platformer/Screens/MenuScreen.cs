using System;

using Raylib_cs;

namespace platformer.screens
{
    class MenuScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;
        public event Action CloseGame;

        int index = 0;

        public void Start()
        {
            PersistentData.Load();
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
            {
                index = (index + 2) % 3;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
            {
                index = (index + 1) % 3;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
            {
                if (index == 0)
                {
                    ChangeScreen?.Invoke(new LevelScreen(PersistentData.CurrentLevel));
                }
                else if (index == 1)
                {
                    PersistentData.CurrentLevel = AssetManager.GetFirstLevel();
                    PersistentData.Save();
                    ChangeScreen?.Invoke(new LevelScreen(PersistentData.CurrentLevel));
                }
                else if (index == 2)
                {
                    CloseGame?.Invoke();
                }
                
            }
        }

        public void Render()
        {
            Raylib.DrawText($"Continue: {PersistentData.CurrentLevel}", 100, 100, 100, Color.BLACK);
            Raylib.DrawText($"New Game", 100, 210, 100, Color.BLACK);
            Raylib.DrawText($"Exit", 100, 320, 100, Color.BLACK);

            Raylib.DrawRectangle(90, 110 + 100 * index, 20, 20, Color.BLACK);
        }
    }
}