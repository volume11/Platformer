using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class Thrower : IEnemy
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}
        public Vector2 CollisionBoxSize => new Vector2(20, 60);
        public Vector2 Velocity {get; set;}
        public bool IsOnGround {get; set;}
        public bool IsOnWall {get; set;}
        public bool IsOnLeftWall {get; set;}
        public bool IsOnRightWall {get; set;}

        public int Score => 2000;

        float timer;
        float throwTime = 2;

        public void Update()
        {
            timer += Raylib.GetFrameTime();
            if (timer > throwTime)
            {
                timer -= throwTime;

                //THROW
                BouncingBall b = new BouncingBall(new Vector2(-200, -20));
                b.Position = Position;
                World.entityContainer.AddEntity(b);
            }
        }

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.RED);
        }
    }
}