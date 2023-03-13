using Raylib_cs;

using System;

namespace platformer.screens
{
    class LevelScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;
        public event Action CloseGame;

        World world;

        LevelData data;

        public LevelScreen(string LevelID)
        {
            world = new World(LevelID);
            data = world.LevelData;

            world.OnLevelEnd += OnLevelEnd;
        }

        public void Start()
        {
            //SoundManager.PlayMusic("bgm");
        }

        public void Update()
        {
            world.Update();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                ChangeScreen?.Invoke(new LevelScreen(data.levelName));
            }
        }

        public void Render()
        {
            world.Render();

            Raylib.DrawText($"Score: {data.score}", 40, 40, 10, Color.BLACK);
            Raylib.DrawText($"Time: {MathF.Round(data.time, 2)}", 40, 55, 10, Color.BLACK);
        }

        void OnLevelEnd()
        {
            ChangeScreen?.Invoke(new EndScreen(data));
        }
    }
}