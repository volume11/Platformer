using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    interface IPhysics : IEntity
    {
        Vector2 Velocity {get; set;}
        Vector2 CollisionBoxSize {get;}
        bool IsOnGround {get; set;}

        void PhysicsBodyCollided(IEntity body) {}
    }
}