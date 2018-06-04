﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class BrickView : GameObject, IViewObjectsCommon
    {
        public Boolean isWater;
        private Rectangle[] rect = { new Rectangle(770, 0, 50, 50), new Rectangle(817, 145, 48, 48) };
        public BrickView()
        {
            
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            throw new NotImplementedException();
        }

        public void DrawYourSelf(Graphics g)
        {
            int index;
            if (isWater)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rect[index], GraphicsUnit.Pixel);
        }

        public void ReadDirectionFromModel(DataTransfer e)
        {
            throw new NotImplementedException();
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            Position = e.Position;
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void SpawnBulletView(BulletView bullet)
        {
            throw new NotImplementedException();
        }
    }
}
