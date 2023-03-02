using System.Numerics;
using Raylib_cs;

namespace platformer.entities
{
    class Coin : IEntity, IPhysics
    {
        public World World {get; set;}
        
        public Vector2 Position {get; set;}
        public Vector2 Velocity {get; set;}

        public Vector2 CollisionBoxSize => new Vector2(40, 40);
        public bool IsOnGround {get; set;}
        public bool IsOnWall {get; set;}

        public void Render()
        {
            Vector2 center = Position + CollisionBoxSize / 2;
            Raylib.DrawCircleV(center, 20, Color.GOLD);
        }
    }
}