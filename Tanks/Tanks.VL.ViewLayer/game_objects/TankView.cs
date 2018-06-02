using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    class TankView : GameObject, ICommonAbilitys
    {
        public delegate void StartMoving(TankView sender, EventArgs e);
        public event StartMoving OnMoving;
        public delegate void SetDirection(int sender);
        public event SetDirection PickDirection;

        int stepCD;
        int maxCD;
        int shootCd;
        int countShootCd;
        int switchCd;
        int maxSwithCd;
        private Image pic = AllGameImages.enemyTank;
        private Rectangle[] rects = { new Rectangle(383, 3, 45, 45),
            new Rectangle(575, 0, 45, 45),
            new Rectangle(485, 0, 45, 45),
            new Rectangle(670, 0, 45, 45) };

        public TankView()
        {
            stepCD = 0;
            maxCD = ServiceLib.GetRandomNumber(1, 5) * 60;
        }

        public void UpdateLogick()
        {
            FollowDirection();
            TurnRandomDirection();
        }

        private void FollowDirection()
        {
            switch (Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    OnMoving(this, EventArgs.Empty);
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    OnMoving(this, EventArgs.Empty);
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    OnMoving(this, EventArgs.Empty);
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    OnMoving(this, EventArgs.Empty);
                    break;
                default:
                    throw (new ArgumentException("No such Direction!"));
            }
        }

        private void TurnRandomDirection()
        {
            if (stepCD >= maxCD)
            {
                stepCD = 0;
                maxCD = ServiceLib.GetRandomNumber(1, 10) * 4;
                PickDirection(ServiceLib.GetRandomNumber(0, 4));
            }
            else
            {
                stepCD++;
            }
        }

        public void DrawYourSelf(Graphics g)
        {
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rects[Direction], GraphicsUnit.Pixel);
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            maxCD = ServiceLib.GetRandomNumber(1, 10) * 4;
            PickDirection((int)dir);
        }
    }
}
