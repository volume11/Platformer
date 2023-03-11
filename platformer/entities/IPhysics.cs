using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    interface IKinematicBody : ICollidingBody
    {
        Vector2 Velocity {get; set;}
        bool IsOnGround {get; set;}
        bool IsOnLeftWall {get; set;}
        bool IsOnRightWall {get; set;}
    }

    interface ICollidingBody : IEntity
    {
        Vector2 CollisionBoxSize {get;}
        void PhysicsBodyCollided(IEntity body) {}
    }
}