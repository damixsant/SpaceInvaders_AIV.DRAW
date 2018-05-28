using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Alien
    {
        int width;
        int height;
        
       Color color;
        //Rect sprite;
        int distToSide;
        Pixel[] sprite;
        Pixel[] sprite2;
        float nextShoot;
        int vite;

        public Vector2 Velocity;
        public Vector2 Position;
        public bool IsAlive;
        public bool IsVisible;
        public bool CanShoot;

        public Alien(Vector2 pos, Vector2 vel, int w, int h, Color col)
        {
            Position = pos;
            Velocity = vel;
            width = w;
            height = h;
            color = col;
            IsAlive = true;
            IsVisible = true ;
            distToSide = 15;
            vite = 1;

            nextShoot = RandomGenerator.GetRandom(2, 12);


            //sprite = new Rect(Position.X - width / 2, Position.Y - height / 2, width, height, tools, color);

            //alien1
            byte[] pixelArr = {  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
            };
            byte[] pixelArr2 = { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
                                 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0,
                                 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0
            };



            int numPixels = 0;
            int numPixels2 = 0;

            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (pixelArr[i] == 1)
                    numPixels++;
            }


            for (int i = 0; i < pixelArr2.Length; i++)
            {
                if (pixelArr2[i] == 1)
                    numPixels2++;
            }



            sprite = new Pixel[numPixels];

            int verticalPixel = 8;
            int horizontalPixel = 11;
            int pixelSize = height / verticalPixel;
            width = horizontalPixel * pixelSize;

            float startPosX = Position.X - (float)width / 2;
            float posY = Position.Y - height / 2;


            int sp = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (i != 0 && i % horizontalPixel == 0)
                    posY += pixelSize;

                if (pixelArr[i] != 0)
                {
                    float pixelX = startPosX + (i % horizontalPixel) * (pixelSize);
                    sprite[sp] = new Pixel(new Vector2(pixelX, posY), pixelSize, color);
                    sp++;
                }
            }

            

        }

      

        public bool OnHit()
        {
            vite--;
            
            if (vite == 0)
            {

                IsAlive = false;

                for (int i = 0; i < sprite.Length; i++)
                {
                    Vector2 startvel = sprite[i].Position.Sub(Position);
                    startvel.X *= RandomGenerator.GetRandom(10, 35);
                    startvel.Y *= RandomGenerator.GetRandom(10, 28);
                    sprite[i].Velocity = startvel;
                    sprite[i].IsGravity = true;
                }
                return true;
            }


            return false;
           
        }

        

        public bool Update(ref float overflowX)
        {
            bool endReached = false;

            if (IsAlive)
            {
                float deltaX = Velocity.X * GfxTools.Win.deltaTime;
                float deltaY = Velocity.Y * GfxTools.Win.deltaTime;
                Position.X += deltaX;
                Position.Y += deltaY;

                float maxX = Position.X + width / 2;
                float minX = Position.X - width / 2;



               

                if (maxX > GfxTools.Win.width - distToSide)
                {
                    overflowX = maxX - (GfxTools.Win.width - distToSide);

                    endReached = true;
                }
                else if (minX < distToSide)
                {
                    overflowX = minX - distToSide;

                    endReached = true;
                }

                //sprite.Translate(deltaX, deltaY);
                TranslateSprite(new Vector2(deltaX, deltaY));

                if (Position.Y + height / 2 >= Game.GetPlayer().GetPosition().Y)
                {
                    EnemyMgr.Landed = true;
                }
                else if (CanShoot)
                {
                    nextShoot -= GfxTools.Win.deltaTime;
                    if (nextShoot <= 0)
                    {
                        
                        EnemyMgr.Shoot(this);
                        nextShoot = RandomGenerator.GetRandom(1, 8);
                    }
                }

            }
            else if(IsVisible)
            {
                for (int i = 0; i < sprite.Length; i++)
                {
                    
                    sprite[i].update();
                }


            }

            

            return endReached;
        }

        public void Draw()
        {
            
                for (int i = 0; i < sprite.Length; i++)
                {
                    sprite[i].Draw();

                }
           
        }
       

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        private void TranslateSprite(Vector2 transVect)
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i].Translate(transVect.X, transVect.Y);
              
            }
        }

        public void Translate(Vector2 transVect)
        {
            Position.X += transVect.X;
            Position.Y += transVect.Y;

            //sprite.Translate(transVect.X, transVect.Y);
            TranslateSprite(transVect);
        }

    }
}
