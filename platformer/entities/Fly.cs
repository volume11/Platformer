using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class Fly : IEnemy
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}
        public Vector2 CollisionBoxSize => new Vector2(10, 10);
        public Vector2 Velocity {get; set;}

        public bool IsOnGround {get; set;}
        public bool IsOnLeftWall {get; set;}
        public bool IsOnRightWall {get; set;}

        public int Score => 1000;

        public void Update()
        {
            //Stop gravity
            Velocity -= World.gravity;

            Velocity += Vector2.Normalize(World.Player.Position - Position) * 3;
        }

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.RED);
        }
    }
}