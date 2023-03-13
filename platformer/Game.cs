using Raylib_cs;

using platformer.screens;

namespace platformer
{
    class Game
    {
        IScreen currentScreen = null;
        IScreen queuedScreen = null;

        bool shouldClose;

        public void Run()
        {
            //INITIALIZE
            Raylib.InitWindow(1600, 800, "Platformer");
            Raylib.InitAudioDevice();
            Raylib.SetTargetFPS(200);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);

            AssetManager.Init();
            PersistentData.Load();

            queuedScreen = new MenuScreen();

            while (!shouldClose)
            {
                if (queuedScreen != null)
                {
                    if (currentScreen != null)
                    {
                        //Unbind previous screen change screen event to avoid it persisting in memory after the screen is changed
                        currentScreen.ChangeScreen -= ChangeScreen;
                        currentScreen.CloseGame -= EndGame;
                        currentScreen.End();
                    }

                    currentScreen = queuedScreen;
                    currentScreen.ChangeScreen += ChangeScreen;
                    currentScreen.CloseGame += EndGame;
                    queuedScreen = null;
                    currentScreen.Start();
                }

                if (currentScreen == null)
                {
                    //if there is no current screen, exit the game
                    break;
                }

                currentScreen.Update();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);
                currentScreen.Render();

                Raylib.DrawFPS(0, 0);
                Raylib.EndDrawing();

                Raylib.UpdateMusicStream(SoundManager.currentTrack);

                if (Raylib.WindowShouldClose())
                {
                    shouldClose = true;
                }
            }

            Raylib.CloseWindow();
            Raylib.CloseAudioDevice();
        }

        void ChangeScreen(IScreen screen)
        {
            queuedScreen = screen;
        }

        void EndGame()
        {
            shouldClose = true;
        }
    }
}