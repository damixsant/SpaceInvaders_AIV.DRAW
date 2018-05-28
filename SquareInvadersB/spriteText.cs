using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class spriteText
    {

        private SpriteObj[] sprites;
        private string text;

        public Vector2 position;
        public string Text
        {
            get { return text; }
            set { setText(value);  }
        }

        public spriteText(Vector2 pos, string text = "")
        {
            position = pos;
            sprites = new SpriteObj[10];
            if(text != "")
            {
                setText(text);
            }

        }
        public void Draw()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if(sprites[i]== null)
                {
                    return;
                   
                }
                sprites[i].Draw();


            }
        }

        public void setText(string text)
        {
            this.text = text;
            int numChars = text.Length;
            int charX = (int)position.X;
            int charY = (int)position.Y;

            if (numChars > sprites.Length)
            {
                numChars = sprites.Length;
            }

            for (int i = 0; i < numChars; i++)
            {
                char number = text[i];
                sprites[i] = new SpriteObj("Assets/numbers_" + number + ".png", charX, charY);
                charX += sprites[i].width;

            }
            

        }

    }
}
