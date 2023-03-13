using System;
using Raylib_cs;

namespace platformer.screens
{
    class WinScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;
        public event Action CloseGame;

        public void Render()
        {
            Raylib.DrawText("You Win!", 100, 100, 100, Color.BLACK);
        }
    }
}