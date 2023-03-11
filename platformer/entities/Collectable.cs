using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class Collectable : ICollidingBody
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}
        public Vector2 CollisionBoxSize => new Vector2(30, 30);

        public void Render()
        {
            Vector2 center = Position + CollisionBoxSize;
            Raylib.DrawCircleV(Position, 15, Color.BLUE);
        }

        public void PhysicsBodyCollided(IEntity body)
        {
            if (body is Player)
            {
                World.entityContainer.RemoveEntity(this);
                World.AddScore(2000, this);
                World.collectedCollectable();
            }
        }
    }
}