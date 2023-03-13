using System;

using Raylib_cs;

namespace platformer.screens
{
    class MenuScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;
        public event Action CloseGame;

        public void Start()
        {
            PersistentData.Load();
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
            {
                ChangeScreen?.Invoke(new LevelScreen(PersistentData.CurrentLevel));
            }
        }

        public void Render()
        {
            Raylib.DrawText(PersistentData.CurrentLevel, 10, 10, 10, Color.BLACK);
        }
    }
}