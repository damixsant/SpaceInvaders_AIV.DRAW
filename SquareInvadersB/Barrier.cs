using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Barrier
    {
        Vector2 position;
        public SpriteObj spriteobj;
        int width;
        int height;
        float ray;
        

        public Barrier(Vector2 pos)
        {
            position = pos;

            spriteobj = new SpriteObj("Assets/barrier.png",position);
            spriteobj.Translate(-spriteobj.width / 2, -spriteobj.height);

            width = spriteobj.width;
            height = spriteobj.height;

            ray = width / 2;

        }

        public bool PixelCollides(Vector2 center, float ray, bool erase = false)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 pixelPos = new Vector2(position.X - width / 2 + x, position.Y - height / 2 + y);
                    float pixToBulletDist = pixelPos.Sub(center).GetLength();
                    if (pixToBulletDist <= ray)
                    {
                       int pixelAlfaIndex = (y * width + x) * 4 + 3;
                        if (erase)
                        {//must erase pixels inside the explosion's circle

                            spriteobj.GetSprite().bitmap[pixelAlfaIndex] = 0;
                        }
                        else
                        {
                            if (spriteobj.GetSprite().bitmap[pixelAlfaIndex] != 0)
                            {
                                return true;
                            }
                        }
                      
                    }
                }

            }
            return false;

        }

        public bool Collides(Vector2 center, float ray)
        {
            bool collision = false;
            Vector2 dist = position.Sub(center);
            if (dist.GetLength() <= width / 2 + ray)
            {
                if (PixelCollides(center, ray))
                {
                    collision = true;
                    PixelCollides(center, 15, true);
                }
            }
            return collision;
        }

        public void Draw()
        {
            spriteobj.Draw();
        }

    }
}
