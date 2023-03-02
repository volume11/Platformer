using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class Player : IEntity, IPhysics
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}

        public Vector2 Velocity {get; set;}

        public Vector2 CollisionBoxSize => new Vector2(20, 20);
        public bool IsOnGround {get; set;}

        float speed = 100;
        float jumpAcceleration = 200;

        public void Update()
        {
            Velocity += new Vector2(0, 1f);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J) && IsOnGround)
            {
                Velocity = new Vector2(Velocity.X, -jumpAcceleration);
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                Velocity = new Vector2(-speed, Velocity.Y);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                Velocity = new Vector2(speed, Velocity.Y);
            }
            else
            {
                Velocity = new Vector2(0, Velocity.Y);
            }
        }

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.RED);
        }

        public void PhysicsBodyCollided(IEntity body)
        {
            World.entityContainer.RemoveEntity(body);
        }
    }
}