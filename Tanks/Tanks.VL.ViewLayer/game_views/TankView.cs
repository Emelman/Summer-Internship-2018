using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class TankView : GameObject, IViewObjectsCommon
    {
        public delegate void StartMoving(TankView sender, EventArgs e);
        public event StartMoving OnMoving;
        public delegate void SetDirection(DataTransfer e);
        public event SetDirection PickDirection;

        public delegate void CreateBulletModel(TankView sender);
        public event CreateBulletModel StartShoot;
        public delegate void CreateBulletView(BulletView e);
        public CreateBulletView SpawnBullet;

        private MainStage main;
        private Image pic = AllGameImages.enemyTank;
        private Rectangle[] rects = { new Rectangle(383, 3, 45, 45),
            new Rectangle(575, 0, 45, 45),
            new Rectangle(485, 0, 45, 45),
            new Rectangle(670, 0, 45, 45) };

        public TankView(MainStage _main)
        {
            main = _main;
            stepCD = 0;
            maxCD = ServiceLib.GetRandomNumber(1, 5) * 60;
            SpawnBullet = new CreateBulletView(SpawnBulletView);
        }

        public void UpdateLogick()
        {
            FollowDirection();
            TurnRandomDirection();
            //Shoot();
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
                PickDirection(new DataTransfer(Id, ServiceLib.GetRandomNumber(0, 4)));
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
            if (shootCd <= countShootCd)
            {
                shootCd = 0;
                countShootCd = ServiceLib.GetRandomNumber(1, 5) * 60;
                StartShoot?.Invoke(this);
            }
            else
            {
                shootCd++;
            }
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            maxCD = ServiceLib.GetRandomNumber(1, 10) * 4;
            PickDirection(new DataTransfer(Id,(int)dir));
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            Position = e.Position;
        }

        public void ReadDirectionFromModel(DataTransfer e)
        {
            Direction = e.Direction;
        }

        public void SpawnBulletView(BulletView bullet)
        {
            var bullets = main.BulletsToDraw;
            main.control.InitBulletsViewEvents(bullet);
            bullets.Add(bullet);
            main.BulletsToDraw = bullets;
        }
    }
}
