using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    static class EnemyMgr
    {
        static Alien[] aliens;
        static int numAliens;
        static int numRows;
        static int aliensPerRow;
        static int alienWidth;
        static int alienHeight;
        static Bullet[] bullets;
        static BulletAliens[] bulletsAliens;
        static int numAlives;
       
        

        public static bool Landed;

        public static void Init(int numOfAliens, int numOfRows)
        {
            numAliens = numOfAliens;
            numRows = numOfRows;
            aliensPerRow = numAliens / numRows;
            numAlives = numAliens;

           
            

            aliens = new Alien[numAliens];

            int startX = 300;
            int posY = 150;
            int dist = 30;
            alienWidth =60;
            alienHeight = 40;

            Color green = new Color(255, 255, 0);

            for (int i = 0; i < aliens.Length; i++)
            {
                if(i!=0 && i%aliensPerRow==0)
                {
                    posY += alienHeight+dist;
                }
                int alienX = startX + (i%aliensPerRow) * (alienWidth + dist);
                aliens[i] = new Alien(new Vector2(alienX, posY), new Vector2(20, 0), alienWidth, alienHeight, green);

                if (i>=numOfAliens-aliensPerRow)
                {
                    aliens[i].CanShoot = true;
                }
            }

            bulletsAliens = new BulletAliens[aliensPerRow];

            Color bulletCol = new Color(250,200,0);


            for (int i = 0; i < bulletsAliens.Length; i++)
            {
                bulletsAliens[i] = new BulletAliens(20, 15, bulletCol);
            }

            

            // aliens[0]= new Alien(tools, new Vector2(tools.Win.width / 2, 40), new Vector2(250, 0), 40, 40, new Color(0, 255, 0));
        }
       

        public static void Update()
        {
            bool endReached = false;
            float tmpOverflowX = 0;
            float overflowX = 0;

            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsVisible)
                {
                    if (aliens[i].Update(ref tmpOverflowX))
                    {
                        endReached = true;
                        overflowX = tmpOverflowX;
                    }
                }
            }

            if (endReached)//at least one alien has reached the end of the screen (or the start!)
            {
                for (int i = 0; i < aliens.Length; i++)
                {
                    //Position.X -= overflowX;
                    //deltaX -= overflowX;
                    aliens[i].Translate(new Vector2(-overflowX, 50));
                    aliens[i].Velocity.X = -aliens[i].Velocity.X;
                }
            }

            Player player = Game.GetPlayer();
            for (int i = 0; i < bulletsAliens.Length; i++)
            {
                if (bulletsAliens[i].IsAlive)
                {
                    
                    bulletsAliens[i].Update();

                    if (Game.BarriersCollides(bulletsAliens[i].Position, bulletsAliens[i].GetWidth()/2))
                    {
                        bulletsAliens[i].IsAlive = false;
                    }
                    //check collision with player
                    if (bulletsAliens[i].Collides(player.GetPosition(), player.GetRay()))
                    {
                        bulletsAliens[i].IsAlive = false;

                        player.OnHit();
                    }

                }
            }

          

            




        }

        private static BulletAliens GetFreeBullet()
        {
            for (int i = 0; i < bulletsAliens.Length; i++)
            {
                if (bulletsAliens[i].IsAlive == false)
                {
                    return bulletsAliens[i];
                }
            }
            return null;
        }

        public static void Shoot(Alien shooter)
        {
            BulletAliens b = GetFreeBullet();
           
            if (b != null)
            {
                b.Shoot(new Vector2(shooter.Position.X, shooter.Position.Y + shooter.GetHeight()/2 + 15), new Vector2(0, 250));
            }
        }

        public static void Draw()
        {
           

            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsVisible)
                    aliens[i].Draw();
            }

            for (int i = 0; i < bulletsAliens.Length; i++)
            {
                if (bulletsAliens[i].IsAlive)
                    bulletsAliens[i].Draw();
            }
        }

        public static int GetAlives()
        {
            return numAlives;
        }

        private static void IncAliensSpeed(float percentage)
        {
            for (int i = 0; i < aliens.Length; i++)
            {
                aliens[i].Velocity.X *= percentage;
            }
        }


       


        public static bool CollideWithBullet(BulletPlayer bulletPlayer)
        {
            for (int i = 0; i < aliens.Length; i++)
            {
              
                if (aliens[i].IsAlive)
                {
                    //Vector2 dist = aliens[i].Position.Sub(bullet.Position);
                    //if (dist.GetLength() <= aliens[i].GetWidth()/2 + bullet.GetWidth()/2)
                    if(bulletPlayer.Collides(aliens[i].Position, aliens[i].GetWidth()/2))
                    {
                        //alien dies
                        if (aliens[i].OnHit())
                        {
                           
                            Game.AddScore(5);
                            IncAliensSpeed(1.05f);
                            //he's dead
                            if (aliens[i].CanShoot)
                            {
                                int prevAlienIndex = i - aliensPerRow;
                                while (prevAlienIndex >= 0)
                                {
                                    if (aliens[prevAlienIndex].IsAlive)
                                    {
                                        aliens[prevAlienIndex].CanShoot = true;
                                        break;
                                    }

                                    prevAlienIndex -= aliensPerRow;
                                }
                            }

                            numAlives--;

                        }
                       

                        return true;
                    }
                }
                    
            }
            return false;
        }

    }
}
