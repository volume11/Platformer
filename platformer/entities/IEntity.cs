using System.Numerics;

namespace platformer.entities
{
    interface IEntity
    {
        World World {get; set;}
        Vector2 Position {get; set;}

        void Start() {}
        void Update() {}
        void Render() {}
        void End() {}
    }
}