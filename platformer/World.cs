using System.Numerics;
using System.Collections.Generic;

using platformer.entities;

using Raylib_cs;

namespace platformer
{
    class World
    {
        public EntityContainer entityContainer;

        Tilemap tilemap;

        Player player;
        Camera2D camera;

        public World()
        {
            entityContainer = new EntityContainer(this);

            //Test Code
            player = new Player();
            entityContainer.AddEntity(player);

            Coin c = new Coin();
            c.Position = new Vector2(100, 100);
            entityContainer.AddEntity(c);

            tilemap = new Tilemap(50, 50, 20);
            camera = new Camera2D(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2, player.Position, 0, 1);

            tilemap.Tiles[100 * 5 + 5] = 1;
        }

        public void Update()
        {
            foreach (IEntity entity in entityContainer.Entities)
            {
                entity.Update();

                
                if (entity is IPhysics)
                {
                    IPhysics physics = entity as IPhysics;

                    physics.IsOnWall = false;

                    //TILEMAP COLLISION
                    for (int i = 1; i <= 4; i++)
                    {
                        Vector2 testPosition = physics.Position + new Vector2(physics.Velocity.X, 0) * Raylib.GetFrameTime() * 1/4;
                        if (!tilemap.CheckCollision(testPosition, physics.CollisionBoxSize))
                        {
                            physics.Position = testPosition;
                        }
                        else
                        {
                            physics.Velocity = new Vector2(0, physics.Velocity.Y);
                            physics.IsOnWall = true;
                            continue;
                        }
                    }

                    physics.IsOnGround = false;

                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 testPosition = physics.Position + new Vector2(0, physics.Velocity.Y) * Raylib.GetFrameTime() * 1/10;
                        if (!tilemap.CheckCollision(testPosition, physics.CollisionBoxSize))
                        {
                            physics.Position = testPosition;
                        }
                        else
                        {
                            physics.Velocity = new Vector2(physics.Velocity.X, 0);
                            physics.IsOnGround = true;
                            continue;
                        }
                    }

                    //ENTITY COLLISION
                    foreach (IEntity othere in entityContainer.Entities)
                    {
                        if (othere == entity) continue;

                        if (othere is IPhysics)
                        {
                            IPhysics otherPhysics = othere as IPhysics;

                            Rectangle rec1 = new Rectangle(physics.Position.X, physics.Position.Y, physics.CollisionBoxSize.X, physics.CollisionBoxSize.Y);
                            Rectangle rec2 = new Rectangle(otherPhysics.Position.X, otherPhysics.Position.Y, otherPhysics.CollisionBoxSize.X, otherPhysics.CollisionBoxSize.Y);
                            
                            if (Raylib.CheckCollisionRecs(rec1, rec2))
                            {
                                physics.PhysicsBodyCollided(othere);
                            }
                        }
                    }
                }
            }

            camera.target = player.Position;

            entityContainer.Flush();
        }

        public void Render()
        {
            Raylib.BeginMode2D(camera);

            tilemap.Render();

            foreach (IEntity entity in entityContainer.Entities)
            {
                entity.Render();
            }

            Raylib.EndMode2D();
        }
    }
}