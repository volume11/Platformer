using System.Numerics;
using Raylib_cs;

namespace platformer.entities
{
    class Coin : ICollidingBody
    {
        public World World {get; set;}
        
        public Vector2 Position {get; set;}

        public Vector2 CollisionBoxSize => new Vector2(20, 20);

        public void Render()
        {
            Vector2 center = Position + CollisionBoxSize / 2;
            Raylib.DrawCircleV(center, 10, Color.GOLD);
        }
    }
}