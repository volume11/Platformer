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
            Raylib.InitAudioDevice();
            Raylib.SetTargetFPS(200);

            AssetManager.Init();
            PersistentData.Load();

            //----TEST CODE----
            queuedScreen = new TestScreen();
            //-----------------

            while (!Raylib.WindowShouldClose())
            {
                if (queuedScreen != null)
                {
                    if (currentScreen != null)
                    {
                        //Unbind previous screen change screen event to avoid it persisting in memory after the screen is changed
                        currentScreen.ChangeScreen -= ChangeScreen;
                        currentScreen.End();
                    }

                    currentScreen = queuedScreen;
                    currentScreen.ChangeScreen += ChangeScreen;
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
                Raylib.ClearBackground(Color.WHITE);
                currentScreen.Render();
                Raylib.EndDrawing();

                Raylib.UpdateMusicStream(SoundManager.currentTrack);
            }
        }

        void ChangeScreen(IScreen screen)
        {
            queuedScreen = screen;
        }
    }
}