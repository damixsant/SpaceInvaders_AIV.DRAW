using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class BulletAliens
    {
        public int width;
        int height;
        Vector2 velocity;
        Color color;
        SpriteObj sprite;
        Animation animation;
        
   


        public Vector2 Position;
        public bool IsAlive;

        public BulletAliens(int w, int h, Color col)
        {
            Position = new Vector2(0, 0);
            velocity = Position;
            sprite = new SpriteObj();

            color = col;

            string[] animationfiles = { "Assets/A_F/bulletA1.png", "Assets/A_F/bulletA2.png",
                "Assets/A_F/bulletA3.png", "Assets/A_F/bulletA4.png", "Assets/A_F/bulletA5.png",
                "Assets/A_F/bulletA6.png", "Assets/A_F/bulletA7.png", "Assets/A_F/bulletA8.png",
                "Assets/A_F/bulletA9.png", "Assets/A_F/bulletA10.png", "Assets/A_F/bulletA11.png",
                "Assets/A_F/bulletA12.png", "Assets/A_F/bulletA13.png", "Assets/A_F/bulletA14.png",
                "Assets/A_F/bulletA15.png" };

            

            animation = new Animation(animationfiles, 24, sprite, 12f);
            width = sprite.width;
            height = sprite.height;

            //test
        }
      

        public void Update()
        {
            animation.Update2();

            Position.X += velocity.X * GfxTools.Win.deltaTime;
            Position.Y += 10+ (velocity.Y * GfxTools.Win.deltaTime);


            
        
            if (Position.Y + height / 2 < 0 || Position.Y - height / 2 >= GfxTools.Win.height)
            {
                IsAlive = false;
            }
            sprite.Position = Position;


           
          

        }

        
        //public void set()
        //{


        //    if (Game.GetSwitcher() == 0)
        //    {
        //        sprite = new SpriteObj("Assets/alienBullet_" + 0 + ".png");
        //        Game.SetSwitcher(1);
        //    }
        //    else
        //    {
        //        sprite = new SpriteObj("Assets/alienBullet_" + 1 + ".png");
        //        Game.SetSwitcher(0);
        //    }

        //}

        public void Draw()
        {
            

            sprite.Draw();
           
            //GfxTools.DrawRect((int)(Position.X - width / 2), (int)(Position.Y - height / 2), width, height, color.R, color.G, color.B);
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
            sprite.Position= new Vector2(Position.X - sprite.width/2,Position.Y-sprite.height/2);
        }

        public int GetWidth()
        {
            return width;
        }
    }
}
