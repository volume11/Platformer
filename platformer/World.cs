using System.Numerics;
using System.Collections.Generic;
using System;
using System.IO;

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

        public World(string LevelID)
        {
            Level level = LevelLoader.Load(LevelID, this);
            this.entityContainer = level.entityContainer;
            this.tilemap = level.tilemap;
            this.Player = level.player;

            LevelData = new LevelData();
            LevelData.levelName = LevelID;
            camera = new Camera2D(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()) / 2, Player.Position, 0, 1);
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