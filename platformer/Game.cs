using Raylib_cs;

using platformer.screens;

namespace platformer
{
    class Game
    {
        IScreen currentScreen = null;
        IScreen queuedScreen = null;

        public void Run()
        {
            //INITIALIZE
            Raylib.InitWindow(500, 500, "Platformer");
            Raylib.SetTargetFPS(200);

            //----TEST CODE----
            currentScreen = new TestScreen();
            currentScreen.ChangeScreen += ChangeScreen;
            currentScreen.Start();
            //-----------------

            while (!Raylib.WindowShouldClose())
            {
                if (currentScreen == null)
                {
                    //if there is no current screen, exit the game
                    break;
                }

                currentScreen.Update();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                currentScreen.Render();
                Raylib.EndDrawing();

                if (queuedScreen != null)
                {
                    //Unbind previous screen change screen event to avoid it persisting in memory after the screen is changed
                    currentScreen.ChangeScreen -= ChangeScreen;
                    currentScreen.End();
                    currentScreen = queuedScreen;
                    currentScreen.ChangeScreen += ChangeScreen;
                    queuedScreen = null;
                    currentScreen.Start();
                }
            }
        }

        void ChangeScreen(IScreen screen)
        {
            queuedScreen = screen;
        }
    }
}