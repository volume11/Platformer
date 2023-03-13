using System.Numerics;
using System.Collections.Generic;
using System;

using platformer.entities;

using Raylib_cs;

namespace platformer
{
    class World
    {
        public event Action OnLevelEnd;

        public EntityContainer entityContainer;

        Tilemap tilemap;

        Camera2D camera;

        public Vector2 gravity => new Vector2(0, 3f);

        public Player Player {get;}

        public LevelData LevelData;

        public World()
        {
            entityContainer = new EntityContainer(this);
            LevelData = new LevelData();

            //Test Code
            Player = new Player();
            Player.Position = new Vector2(20, 48 * 20 - 5);
            entityContainer.AddEntity(Player);

            Collectable col = new Collectable();
            col.Position = new Vector2(90, 90);
            entityContainer.AddEntity(col);

            Coin c = new Coin();
            c.Position = new Vector2(20 * 20, 46 * 20);
            entityContainer.AddEntity(c);

            c = new Coin();
            c.Position = new Vector2(19 * 20, 46 * 20);
            entityContainer.AddEntity(c);

            //End e = new End();
            //e.Position = new Vector2(40 * 20, 48 * 20);
            //entityContainer.AddEntity(e);

            tilemap = new Tilemap(50, 50, 20);
            camera = new Camera2D(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2, Player.Position, 0, 1);

            Walker walker = new Walker();
            walker.Position = new Vector2(800, 47 * 20 - 5);
            entityContainer.AddEntity(walker);

            for (int i = 0; i < 40; i++)
            {
                tilemap.Tiles[50 * i + 6] = 1;
            }

            tilemap.Tiles[50 * 49 + 10] = 1;

            tilemap.Tiles[50 * 48 + 15] = 1;
            tilemap.Tiles[50 * 49 + 15] = 1;

            tilemap.Tiles[50 * 48 + 22] = 1;
            tilemap.Tiles[50 * 49 + 22] = 1;
        }

        public void Update()
        {
            LevelData.time += Raylib.GetFrameTime();

            foreach (IEntity entity in entityContainer.Entities)
            {
                entity.Update();
                
                if (entity is IKinematicBody)
                {
                    IKinematicBody physics = entity as IKinematicBody;

                    physics.Velocity += gravity;

                    physics.IsOnLeftWall = false;
                    physics.IsOnRightWall = false;

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
                            physics.IsOnLeftWall = physics.Velocity.X < 0;
                            physics.IsOnRightWall = !physics.IsOnLeftWall;

                            physics.Velocity = new Vector2(0, physics.Velocity.Y);

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
                }

                if (entity is ICollidingBody)
                {
                    ICollidingBody physics = entity as ICollidingBody;

                    //ENTITY COLLISION
                    foreach (IEntity othere in entityContainer.Entities)
                    {
                        if (othere == entity) continue;

                        if (othere is ICollidingBody)
                        {
                            ICollidingBody otherPhysics = othere as ICollidingBody;

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

            camera.target = Player.Position;

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

        public void EndLevel()
        {
            OnLevelEnd?.Invoke();
        }

        public void AddScore(int score, IEntity source)
        {
            ScorePopup popup = new ScorePopup(score);
            popup.Position = source.Position;
            entityContainer.AddEntity(popup);
            LevelData.score += score;
        }

        public void collectedCollectable()
        {
            LevelData.collectable = true;
        }
    }
}