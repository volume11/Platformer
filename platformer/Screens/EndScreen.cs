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
            Raylib.DrawText("Level Complete!", 10, 10, 40, Color.BLACK);

            Raylib.DrawText($"Score: {data.score}", 10, 60, 20, Color.BLACK);
            Raylib.DrawText($"Time: {MathF.Round(data.time, 2)}", 10, 85, 20, Color.BLACK);
            Raylib.DrawText(data.collectable ? "Collectable got!" : "Collectable missed!", 200, 60, 20, Color.BLACK);

            Raylib.DrawText($"Press R to restart", 10, 110, 30, Color.BLACK);
        }
    }
}