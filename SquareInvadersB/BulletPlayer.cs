using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class BulletPlayer
    {
        public int width;
        int height;
        Vector2 velocity;
        Color color;
        SpriteObj sprite;


        public Vector2 Position;
        public bool IsAlive;

        public BulletPlayer(int w, int h, Color col)
        {
            Position = new Vector2(0, 0);
            velocity = Position;
            sprite = new SpriteObj("Assets/bullet.png");
            width = sprite.width;
            height = sprite.height;
            color = col;

        }


        public void Update()
        {


            Position.X += velocity.X * GfxTools.Win.deltaTime;
            Position.Y += 50+(velocity.Y * GfxTools.Win.deltaTime);


            sprite.Translate(Position.X, Position.Y);

            if (Position.Y + height / 2 < 0 || Position.Y - height / 2 >= GfxTools.Win.height)
            {
                IsAlive = false;
            }
            sprite.Position = Position;

        }


        

        public void Draw()
        {

            sprite.Draw();
        }

        public bool Collides(Vector2 center, float ray)
        {
            Vector2 dist = Position.Sub(center);
            return (dist.GetLength() <= width / 2 + ray);
        }

        public void Shoot(Vector2 startPos, Vector2 startVelocity)
        {
            Position = startPos;
            velocity = startVelocity;
            IsAlive = true;
        }

        public int GetWidth()
        {
            return width;
        }
    }
}
