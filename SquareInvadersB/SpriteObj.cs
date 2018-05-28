﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;


namespace SpaceInvaders
{
    class SpriteObj
    {

        private Sprite sprite;
        private Vector2 position;

        public Sprite Sprite { get { return sprite; } }

        public Vector2 Position { get { return position; } set { position = value; } }

        public int  width { get { return sprite.width; } }
        public int height { get { return sprite.height; } }

        public SpriteObj(string fileNmae, int x = 0, int y = 0)
        {
            sprite = new Sprite(fileNmae);
            position.X = x;
            position.Y = y;
            
        }
        public SpriteObj(string fileName)
        {
            sprite = new Sprite(fileName);
          
        }

      

        public SpriteObj(string fileName, Vector2 position):
            this(fileName, (int)position.X, (int)position.Y)
        {

        }

        public SpriteObj(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Translate(float x,float y)
        {
            position.X += x;
            position.Y += y;

        }
        public SpriteObj()
        {

        }
        public void SetSprite(Sprite newSprite)
        {
             sprite = newSprite;
        }
        public Sprite GetSprite()
        {
            return sprite;
        }

        public void Translate(Vector2 offset)
        {
            position.X += offset.X;
            position.Y += offset.Y;
        }

        public void Draw()
        {
            GfxTools.DrawSprite(sprite, (int)position.X, (int)position.Y); 
        }

        
           
        
    }
}
