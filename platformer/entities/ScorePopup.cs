using System.Numerics;

using Raylib_cs;

namespace platformer.entities
{
    class ScorePopup : IEntity
    {
        public World World {get; set;}
        public Vector2 Position {get; set;}

        int score;
        float currentTime;
        float lifeTime = 1;

        public ScorePopup(int score)
        {
            this.score = score;
        }

        public void Update()
        {
            currentTime += Raylib.GetFrameTime();
            if (currentTime >= lifeTime)
            {
                World.entityContainer.RemoveEntity(this);
            }

            Position += new Vector2(-1, 0) * Raylib.GetFrameTime();
        }

        public void Render()
        {
            Raylib.DrawText(score.ToString(), (int)Position.X, (int)Position.Y, 10, Color.BLACK);
        }
    }
}