using System.IO;

using platformer.entities;
using System.Numerics;

namespace platformer
{
    static class LevelLoader
    {
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
                string line = reader.ReadLine();
                for (int ii = 0; ii < width; ii++)
                {
                    level.tilemap.Tiles[i * width + ii] = line[ii] == '~' ? 0 : 1;
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

            reader.Close();

            return level;
        }
    }
}