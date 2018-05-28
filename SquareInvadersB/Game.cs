using Aiv.Draw;
using System.Threading;
using System.Media;


namespace SpaceInvaders
{



    static class Game
    {
        static Window window;
        static Player player;
        static float totalTime;
        static spriteText scoretext;
        static int score;
        static SoundPlayer music;
        static int switcher;
        static Menu m;
        static backgorund bg;
        static backgorundMenu bg2;
        static int flag = 0;
        static Barrier[] barriers;
        //test



        static Game()
        {
           
            window = new Window(1900, 1000, "Space Invaders", PixelFormat.RGB);
            score = 0;
            
            GfxTools.Init(window);

            Vector2 playerPos;
            playerPos.X = window.width / 2;
            playerPos.Y = window.height - 20;

            EnemyMgr.Init(45, 3);

            switcher = 0;

            player = new Player(playerPos, new Color(22, 22, 200));
            scoretext = new spriteText(new Vector2(220,48 ),"000000");


            PlayerSound.Play("Assets/spaceship.wav", "suono");

            //music = new SoundPlayer();
            //music.SoundLocation = "Assets/spaceship.wav";
            //music.Load();

            //music.PlayLooping();

            bg = new backgorund("Assets/space_3.png");
            bg2 = new backgorundMenu("Assets/space_menu.png");
            m = new Menu();

            barriers = new Barrier[3];

            for (int i = 0; i < barriers.Length; i++)
            {

                barriers[i] = new Barrier(new Vector2((GfxTools.Win.width) *(i+1)/4, (GfxTools.Win.height / 2) + 300));


            }

            //test



        }

        public static Player GetPlayer()
        {
            return player;
        }
        public static void AddScore(int amount)
        {
            if (score + amount >= 0)
            {
                score += amount;
                scoretext.setText(score.ToString("D6"));
            }
            else
            {
                score = 0;
                scoretext.setText(score.ToString("D6"));
            }
                

        }
       public static int GetSwitcher()
        {
            return switcher;
        }
        public static void SetSwitcher(int n)
        {
            switcher=n;
        }
        public static bool BarriersCollides(Vector2 center,float ray)
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                if(barriers[i].Collides(center,ray))
                return true;
            }
            return false;
        }

        public static void DrawBarrier()
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i].Draw();
            }
        }

        public static void Play()
        {




            while (window.opened){
               
                if (flag == 0)
                {
                    //GfxTools.Clean();
                    bg2.DrawBg();
                    m.draw();
                    window.Blit();
                   
                    if (GfxTools.Win.mouseLeft)
                    {
                        if (m.clickButton() == 1)
                        {
                            flag = 1;
                        }
                        else if (m.clickButton() == 2)
                        {
                            return;
                        }
                    }
                }
                else if(flag==1)
                {
                  
                    
                    totalTime += GfxTools.Win.deltaTime;
                    //GfxTools.Clean();
                    bg.DrawBg();


                    //Input
                  ;

                    if (window.GetKey(KeyCode.Esc))
                        return;

                    player.Input();

                    //Update
                    EnemyMgr.Update();
                    player.Update();

                    if (!player.IsAlive || EnemyMgr.GetAlives() <= 0 || EnemyMgr.Landed)
                    {
                        Thread.Sleep(3000);
                        return;
                    }

                    //Draw
                    scoretext.Draw();
                    EnemyMgr.Draw();
                    player.Draw();
                    Game.DrawBarrier();

                   
                   

                    window.Blit();
                }
             
                }
          

        }

        public static int GetScore()
        {
            return score - (int)(totalTime * 0.25);
        }
    }
}
