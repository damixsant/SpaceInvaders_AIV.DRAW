using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Pixel
    {
        Vector2 position;
        Vector2 velocity;

        public Vector2 Position { get { return position; } }
        public Vector2 Velocity {get { return velocity; } set { velocity=value; }  }
        public bool IsGravity { get; set; }


        int width;
        Color color;
       
        public Pixel(Vector2 pos, int w, Color col)
        {
            position = pos;
            width = w;
            color = col;
          
            velocity.X = 0;
            velocity.Y = 0;
        }

        public void update()
        {

            if (IsGravity)
            {
                velocity.Y += 800 * GfxTools.Win.deltaTime;
            }
            position.X += Velocity.X * GfxTools.Win.deltaTime;
            position.Y += Velocity.Y * GfxTools.Win.deltaTime;


            

          

        }

        public void Draw()
        {
            GfxTools.DrawRect((int)position.X, (int)position.Y, width, width, color.R, color.G, color.B);
        }

        public void Translate(float x, float y)
        {
            position.X += x;
            position.Y += y;
        }
        

    }
}
