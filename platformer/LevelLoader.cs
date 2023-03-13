using System.IO;
using System.Collections.Generic;
using System;

using platformer.entities;
using System.Numerics;

namespace platformer
{
    static class LevelLoader
    {
        static Dictionary<string, Type> entityLookup = new Dictionary<string, Type>{
            {"walker", typeof(Walker)},
            {"fly", typeof(Fly)},
            {"thrower", typeof(Thrower)},
        };

        public static Level Load(string LevelID, World world)
        {
            Level level = new Level();

            level.entityContainer = new EntityContainer(world);

            StreamReader reader = new StreamReader($"assets/levels/{LevelID}.txt");
            string[] mapSize = reader.ReadLine().Split();
            int width = int.Parse(mapSize[0]);
            int height = int.Parse(mapSize[1]);
            level.tilemap = new Tilemap(width, height, 20);

            for (int i = 0; i < height; i++)
            {
                string tileline = reader.ReadLine();
                for (int ii = 0; ii < width; ii++)
                {
                    level.tilemap.Tiles[i * width + ii] = tileline[ii] == '~' ? 0 : 1;
                }
            }
            
            string[] playerPosition = reader.ReadLine().Split();
            int x = int.Parse(playerPosition[0]);
            int y = int.Parse(playerPosition[1]);
            level.player = new Player();
            level.player.Position = new Vector2(x, y);
            level.entityContainer.AddEntity(level.player);

            string[] exitPosition = reader.ReadLine().Split();
            int ex = int.Parse(exitPosition[0]);
            int ey = int.Parse(exitPosition[1]);
            End e = new End();
            e.Position = new Vector2(ex, ey);
            level.entityContainer.AddEntity(e);

            string[] collectablePosition = reader.ReadLine().Split();
            int cx = int.Parse(collectablePosition[0]);
            int cy = int.Parse(collectablePosition[1]);
            Collectable c = new Collectable();
            c.Position = new Vector2(cx, cy);
            level.entityContainer.AddEntity(c);

            while (!reader.EndOfStream)
            {
                string[] entityArgs = reader.ReadLine().Split();
                IEntity entity = (IEntity)Activator.CreateInstance(entityLookup[entityArgs[0]]);
                entity.Position = new Vector2(int.Parse(entityArgs[1]), int.Parse(entityArgs[2]));
                level.entityContainer.AddEntity(entity);
            }

            reader.Close();

            return level;
        }
    }
}