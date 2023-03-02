namespace platformer.screens
{
    class LevelScreen : IScreen
    {
        public event ScreenChangeEvent ChangeScreen;

        World world;

        public LevelScreen()
        {
            world = new World();
        }

        public void Update()
        {
            world.Update();
        }

        public void Render()
        {
            world.Render();
        }
    }
}