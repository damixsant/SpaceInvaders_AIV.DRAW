using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Menu
    {
        Vector2 pos1;
        Vector2 pos2;

        int width;
        int height;

        public Menu()
        {
            width = 800;
            height = 200; 
            pos1 = new Vector2((GfxTools.Win.width/2)-(width/2), 300);
            pos2 = new Vector2((GfxTools.Win.width / 2) - (width / 2), 550);




        }

        public void draw()
        {

            //GfxTools.DrawRect((int)pos1.X, (int)pos1.Y, width, height, 250, 250, 250);

            //GfxTools.DrawRect((int)pos2.X, (int)pos2.Y, width, height, 250, 0, 250);
        }

        public int clickButton()
        {


            if (GfxTools.Win.mouseX > pos1.X && GfxTools.Win.mouseX < pos1.X + width && GfxTools.Win.mouseY > pos1.Y && GfxTools.Win.mouseY < pos1.Y + height)
                return 1;
            else
                if (GfxTools.Win.mouseX > pos2.X && GfxTools.Win.mouseX < pos2.X + width && GfxTools.Win.mouseY > pos2.Y && GfxTools.Win.mouseY < pos2.Y + height)
                return 2;

            return 0;

        }
    }
}
