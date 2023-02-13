using Raylib_cs;

namespace platformer.screens
{
    public class TestScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;

        int score;

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
            {
                score += 1;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                ChangeScreen?.Invoke(new TestSubScreen());
            }
        }

        public void Render()
        {
            Raylib.DrawText("Hello world!", 10, 10, 30, Color.BLACK);
            Raylib.DrawText($"Score: {score}", 10, 50, 20, Color.BLACK);
        }
    }
}