using System;

namespace platformer.screens
{
    public delegate void ScreenChangeEvent(IScreen screen);
    public interface IScreen
    {
        event ScreenChangeEvent ChangeScreen;
        event Action CloseGame;
        void Start() {}
        void Update() {}
        void Render() {}
        void End() {}
    }
}