using System.Numerics;
using System;

using Raylib_cs;

namespace platformer.entities
{
    class BouncingBall : IKinematicBody
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}
        Vector2 _Velocity;
        public Vector2 Velocity {get => _Velocity; set => _Velocity = value;}
        public Vector2 CollisionBoxSize => new Vector2(20, 20);

        public bool IsOnGround {get; set;}
        public bool IsOnLeftWall {get; set;}
        public bool IsOnRightWall {get; set;}

        int bounceCount;

        float xMagnitude;

        public BouncingBall(Vector2 initVel)
        {
            Velocity = initVel;
            xMagnitude = MathF.Abs(Velocity.X);
        }

        public void Update()
        {
            if (IsOnGround)
            {
                _Velocity.Y = -200;
                bounceCount++;
            }

            if (IsOnLeftWall)
            {
                _Velocity.X = xMagnitude;
                bounceCount++;
            }
            if (IsOnRightWall)
            {
                _Velocity.X = -xMagnitude;
                bounceCount++;
            }


            if (bounceCount >= 3)
            {
                World.entityContainer.RemoveEntity(this);
            }
        }

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.BLUE);
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