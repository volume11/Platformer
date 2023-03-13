using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class Sawblade : ICollidingBody
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}
        public Vector2 CollisionBoxSize => new Vector2(40, 40);

        public void Render()
        {
            Vector2 center = Position + CollisionBoxSize / 2;
            Raylib.DrawCircleV(center, 20, Color.RED);
        }

        public void PhysicsBodyCollided(IEntity body)
        {
            if (body is Player)
            {
                World.entityContainer.RemoveEntity(body);
            }
        }
    }
}