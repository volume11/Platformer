using System;

using Raylib_cs;

namespace platformer.screens
{
    class EndScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;
        public event Action CloseGame;

        LevelData data;

        public EndScreen(LevelData data)
        {
            this.data = data;
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                ChangeScreen?.Invoke(new LevelScreen());
            }
        }

        public void Render()
        {
            Raylib.DrawText($"Score: {data.score}", 10, 10, 20, Color.BLACK);
            Raylib.DrawText($"Time: {MathF.Round(data.time, 2)}", 10, 35, 20, Color.BLACK);

            Raylib.DrawText($"Press R to restart", 10, 60, 30, Color.BLACK);
        }
    }
}