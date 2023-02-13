using Raylib_cs;

namespace platformer.screens
{
    public class TestSubScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                ChangeScreen?.Invoke(new TestScreen());
            }
        }

        public void Render()
        {
            Raylib.DrawText("This is the subscreen", 10, 10, 30, Color.BLACK);
        }
    }
}