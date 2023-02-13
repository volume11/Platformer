namespace platformer.screens
{
    public delegate void ScreenChangeEvent(IScreen screen);
    public interface IScreen
    {
        event ScreenChangeEvent ChangeScreen;
        void Start() {}
        void Update() {}
        void Render() {}
        void End() {}
    }
}