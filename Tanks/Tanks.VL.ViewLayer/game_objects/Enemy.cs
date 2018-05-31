using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.controller;

namespace Tanks.VL.ViewLayer.game_objects
{
    class Enemy : GameObject, ICommonAbilitys
    {
        EnemyController contrl;
        int stepCD;
        int shootCd;
        int countShootCd;
        private Image pic = AllGameImages.enemyTank;

        public Enemy(EnemyController ctrl)
        {
            contrl = ctrl; 
        }

        public void ChooseDirection()
        {
            if (stepCD >= 60)
            {
                stepCD = 0;
                contrl.SetDirection(ServiceLib.GetRandomNumber(0, 3));
            }
            else stepCD++;
            
        }

        public void UpdateLogick()
        {
            switch (Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    contrl.MoveUp();
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    contrl.MoveDown();
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    contrl.MoveLeft();
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    contrl.MoveRight();
                    break;
                default:
                    throw (new ArgumentException("No such argument!"));
            }
            ChooseDirection();
        }
        public void MoveYourSelf(Graphics g)
        {
            g.DrawImage(pic, Position);
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}
