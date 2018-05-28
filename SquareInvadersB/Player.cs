using Aiv.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace SpaceInvaders
{
    class Player
    {
        Vector2 position;
        int width;
        int height;
        //Rect baseRect;
        //Rect cannonRect;

        SpriteObj spriteobj;
        float speed;
        const float maxSpeed =300.0f;
        int distToSide;
       
        BulletPlayer[] bulletPlayer;

        SoundPlayer musicP;

        float counter;
        float shootDelay;
        Color color;
        float ray;
        int nrg;

        int score;

        public bool IsAlive;

        public Player(Vector2 pos, Color col)
        {
            position = pos;
            
            distToSide = 20;
            shootDelay = 0.3f;
            color = col;
            
            nrg = 10;
            score = 0;

            IsAlive = true;
            spriteobj = new SpriteObj("Assets/playerYellow.png", pos);
            spriteobj.Translate(-spriteobj.width / 2, -spriteobj.height);

            width = spriteobj.width;
            height = spriteobj.height;
            ray = width / 2;
            

            musicP = new SoundPlayer();
            musicP.SoundLocation = "Assets/shoot.wav";
            musicP.Load();

           

            //baseRect = new Rect(position.X - width / 2, position.Y - height / 2, width, height / 2, color);
            //int cannWidth = width / 3;
            //cannonRect = new Rect(position.X - cannWidth / 2, position.Y - height, cannWidth, height / 2, color);


            bulletPlayer = new BulletPlayer[30];

            Color bulletCol = new Color(200, 0, 0);
            for (int i = 0; i < bulletPlayer.Length; i++)
            {
                bulletPlayer[i] = new BulletPlayer(10, 20,bulletCol);
            }
        }

        public void Input()
        {
            counter += GfxTools.Win.deltaTime;

            

            if (GfxTools.Win.GetKey(KeyCode.Right)|| GfxTools.Win.GetKey(KeyCode.D))
            {
                speed = maxSpeed;
            }
            else if (GfxTools.Win.GetKey(KeyCode.Left)|| GfxTools.Win.GetKey(KeyCode.A))
            {
                speed = -maxSpeed;
            }
            else
                speed = 0;

            if (GfxTools.Win.GetKey(KeyCode.Space)|| GfxTools.Win.GetKey(KeyCode.Up)|| GfxTools.Win.mouseLeft)
            {
                if (/*IsFirePressed == false && */counter >= shootDelay)
                {
                    //IsFirePressed = true;
                    musicP.Play();
                    Shoot();
                    counter = 0;
                }
            }
            //else if (IsFirePressed)
            //{
            //    IsFirePressed = false;
            //}
        }

        private BulletPlayer GetFreeBullet()
        {
            for (int i = 0; i < bulletPlayer.Length; i++)
            {
                if (bulletPlayer[i].IsAlive == false)
                {
                    return bulletPlayer[i];
                }
            }
            return null;
        }

        public void Shoot()
        {
            BulletPlayer b = GetFreeBullet();
            if (b != null)
            {
                b.Shoot(new Vector2(position.X,position.Y-height-15),new Vector2(0,-1000));
            }
        }

        public bool OnHit()
        {
            nrg--;
            if (nrg <= 0)
            {
                IsAlive = false;
            }

            return !IsAlive;//return true if player is dead
        }
        

        public void Update()
        {
            float deltaX= speed * GfxTools.Win.deltaTime;
            position.X += deltaX;
            float maxX = position.X + width / 2;
            float minX = position.X - width / 2;

            if (maxX > GfxTools.Win.width-distToSide)
            {
                float overflowX = maxX - (GfxTools.Win.width-distToSide);
                position.X -= overflowX;
                deltaX -= overflowX;
            }
            else if (minX < distToSide)
            {
                float overflowX = minX - distToSide;
                position.X -= overflowX;
                deltaX -= overflowX;
            }

            //rectangles update
            //baseRect.Translate(deltaX, 0);
            //cannonRect.Translate(deltaX, 0);

            spriteobj.Translate(deltaX, 0);


         

            for (int i = 0; i < bulletPlayer.Length; i++)
            {
                if (bulletPlayer[i].IsAlive)
                {
                   
                    bulletPlayer[i].Update();

                    if(Game.BarriersCollides(bulletPlayer[i].Position, bulletPlayer[i].GetWidth()))
                    {
                        bulletPlayer[i].IsAlive = false;
                    }

                    if (EnemyMgr.CollideWithBullet(bulletPlayer[i]))
                    {
                        bulletPlayer[i].IsAlive = false;

                    }
                }
            }
        }

        public void Draw()
        {
            //baseRect.Draw();
            //cannonRect.Draw();

            spriteobj.Draw();

            for (int i = 0; i < bulletPlayer.Length; i++)
            {
                if(bulletPlayer[i].IsAlive)
                    bulletPlayer[i].Draw();
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRay()
        {
            return ray;
        }

        //public int GetScore()
        //{
        //    return score;
        //}

      
    }
}
