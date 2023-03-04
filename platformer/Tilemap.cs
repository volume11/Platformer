using System.Numerics;

using Raylib_cs;

using System;

namespace platformer
{
    class Tilemap
    {
        int[] tiles;
        int width;
        int height;
        int tileSize;

        public int[] Tiles => tiles;

        public Tilemap(int width, int height, int tileSize)
        {
            this.width = width;
            this.height = height;
            this.tiles = new int[width * height];
            this.tileSize = tileSize;
        }

        public void Render()
        {
            for (int i = 0; i < width * height; i++)
            {
                int x = i % width * tileSize;
                int y = i / width * tileSize;

                if (tiles[i] == 0)
                {
                    Raylib.DrawRectangleV(new Vector2(x, y), new Vector2(20), Color.WHITE);
                }
                else
                {
                    Raylib.DrawRectangleV(new Vector2(x, y), new Vector2(20), Color.BLACK);
                }
            }
        }

        public bool CheckCollision(Vector2 position, Vector2 size)
        {
            int left = (int)MathF.Floor(position.X / tileSize);
            int right = (int)MathF.Floor((position.X + size.X) / tileSize);
            int top = (int)MathF.Floor(position.Y / tileSize);
            int bottom = (int)MathF.Floor((position.Y + size.Y) / tileSize);

            if (position.X < 0 || position.X + size.X >= width * tileSize || position.Y < 0 || position.Y + size.Y >= height * tileSize) return true;

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    if (tiles[x + y * width] == 1) return true;
                }
            }

            return false;
        }
    }
}