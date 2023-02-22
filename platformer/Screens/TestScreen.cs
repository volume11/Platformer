using Raylib_cs;

namespace platformer.screens
{
    public class TestScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;

        int score;

        public void Start()
        {
            SoundManager.PlayMusic("bgm");
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
            {
                score += 1;
                SoundManager.PlaySound("sound");
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                ChangeScreen?.Invoke(new TestSubScreen());
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                SoundManager.SetVolume(0);
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                SoundManager.SetVolume(1);
            }
        }

        public void Render()
        {
            Raylib.DrawText("Hello world!", 10, 10, 30, Color.BLACK);
            Raylib.DrawText($"Score: {score}", 10, 50, 20, Color.BLACK);
            Raylib.DrawTexture(AssetManager.getTexture("test").Value, 50, 50, Color.WHITE);
        }
    }
}