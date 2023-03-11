using System.Numerics;
using System;

using Raylib_cs;

namespace platformer.entities
{
    class Player : IEntity, IKinematicBody
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}

        Vector2 _Velocity;
        public Vector2 Velocity {get => _Velocity; set => _Velocity = value; }

        public Vector2 CollisionBoxSize => new Vector2(20, 40);
        public bool IsOnGround {get; set;}
        public bool IsOnLeftWall {get; set;}
        public bool IsOnRightWall {get; set;}

        float acceleration = 15;
        float airAcceleration = 3;
        float jumpAcceleration = 300;
        float wallSlide = 10;
        float maxSpeed = 200;
        float groundDrag = 0.8f;
        float airDrag = 0.98f;

        public void Update()
        {
            if (IsOnLeftWall || IsOnRightWall)
            {
                //Falling downwards -> wallslide
                if (Velocity.Y > 0)
                {
                    _Velocity.Y = wallSlide;
                }

                //On a wall and jumping -> walljump
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
                {
                    _Velocity = new Vector2(maxSpeed * (IsOnLeftWall ? 1 : -1), -jumpAcceleration);
                }
            }

            //On ground and jumping -> jump
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J) && IsOnGround)
            {
                _Velocity.Y = -jumpAcceleration;
            }


            float movementDirection = 0;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                movementDirection -= 1;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                movementDirection += 1;
            }

            //Accelerate in a direction
            _Velocity.X += movementDirection * (IsOnGround ? acceleration : airAcceleration);

            //Apply drag if the user has not made an input
            if (movementDirection == 0)
            {
                _Velocity.X *= IsOnGround ? groundDrag : airDrag;
            }

            //Cap player velocity to a max speed in either direction
            if (Velocity.X > maxSpeed)
            {
                _Velocity.X = maxSpeed;
            }
            if (Velocity.X < -maxSpeed)
            {
                _Velocity.X = -maxSpeed;
            }
        }

        public void Render()
        {
            Raylib.DrawRectangleV(Position, CollisionBoxSize, Color.RED);
        }

        public void PhysicsBodyCollided(IEntity body)
        {
            if (body is Coin)
            {
                World.entityContainer.RemoveEntity(body);
                World.AddScore(100, body);
            }
  
            if (body is Enemy)
            {
                if (Velocity.Y > 0)
                {
                    World.entityContainer.RemoveEntity(body);
                    _Velocity.Y = -100;
                    World.AddScore(1000, body);
                }
                else
                {
                    World.entityContainer.RemoveEntity(this);
                }
            }
        }
    }
}