using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class End : ICollidingBody
    {
        public World World {get; set;}
        public Vector2 CollisionBoxSize => new Vector2(20, 40);
        public Vector2 Position {get; set;}

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.GREEN);
        }

        public void PhysicsBodyCollided(IEntity body)
        {
            if (body is Player)
            {
                World.EndLevel();
            }
        }
    }
}