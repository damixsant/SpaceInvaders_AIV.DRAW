﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace SpaceInvaders
{
    class backgorundMenu
    {
        SpriteObj spriteBg;


        public backgorundMenu(string fileName)
        {
            spriteBg = new SpriteObj(fileName);

        }

        public void DrawBg()
        {
            spriteBg.Draw();
        }
    }
}
