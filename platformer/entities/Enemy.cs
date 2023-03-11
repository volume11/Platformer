using System.Numerics;

namespace platformer.entities
{
    class Enemy : IKinematicBody
    {
        public Vector2 Position {get; set;}

        Vector2 _velocity = new Vector2();
        int dir = -1;
        public Vector2 Velocity {get => _velocity; set => _velocity = value;}

        public bool IsOnGround {get; set;}
        public bool IsOnLeftWall {get; set;}
        public bool IsOnRightWall {get; set;}
        

        public Vector2 CollisionBoxSize => new Vector2(20, 20);
        public World World {get; set;}

        public void Update()
        {
            if (IsOnLeftWall || IsOnRightWall)
            {
                dir *= -1;
            }

            _velocity.X = 20 * dir;
        }

        public void Render()
        {
            Raylib_cs.Raylib.DrawRectangleV(Position, CollisionBoxSize, Raylib_cs.Color.DARKBLUE);
        }
    }
}