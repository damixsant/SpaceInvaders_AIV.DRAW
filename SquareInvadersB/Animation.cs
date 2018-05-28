using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class Animation
    {
        SpriteObj owner;
        Sprite[] sprites;
        float frameDur;
        int currentFrameIndex;
        float counter;
        int nFiles;
        float fps;

        public bool Loop { get; set; }
        public bool isPlaying { get; private set; }

       
        public int currentFrame
        {
            get
            {
                return currentFrameIndex;
            }
            set
            {
                currentFrameIndex = value;

                if (currentFrameIndex >= nFiles)
                    OnanimationEnd();
                else
                    owner.SetSprite(sprites[currentFrameIndex]);
            }
        }

       

        public Animation(string[] files,float Fps,SpriteObj own,float frameDuration)
        {
            owner = own;

          

            nFiles = files.Length;

            frameDur = frameDuration;

            fps = Fps;
           

            sprites = new Sprite[nFiles];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new Sprite(files[i]);
            }

            owner.SetSprite(sprites[0]);

            if (fps > 0.0f)
            {
                frameDur = 1 / fps;

            }

            else
                frameDur = 0.0f;


        }


        public void Start()
        {
            currentFrame = 0;
            isPlaying = true;

        }
        public void Stop()
        {
            currentFrame = 0;
            isPlaying = false;

        }
        public void Pause()
        {
            isPlaying = false;
        }

        

        private void OnanimationEnd()
        {
            if (Loop)
            {
                currentFrame = 0;
            }
            else
            {
                Stop();
            }
        }

        public void Update()
        {


            if(owner!=null && isPlaying)
            {
                if(frameDur != 0.0f)
                {
                    counter += GfxTools.Win.deltaTime;
                    if(counter >= frameDur)
                    {
                        counter = 0;
                        currentFrame = (currentFrame + 1);

                    }
                }
                else
                {
                    owner.SetSprite(sprites[currentFrame]);
                }
            }
          



        }

      
        public void Update2()
        {

            if (frameDur != 0.0f)
            {
                counter += GfxTools.Win.deltaTime;
                if (counter >= frameDur)
                {
                    currentFrame++;
                    owner.SetSprite(sprites[currentFrame]);

                }



            }

        }


        }
}
