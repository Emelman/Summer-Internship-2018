using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class KolobokView : GameObject, IViewObjectsCommon
    {
        public static Boolean upPressed = false;
        public static Boolean downPressed = false;
        public static Boolean leftPressed = false;
        public static Boolean rightPressed = false;
        public static Boolean spacePressed = false;

        public delegate void StartMoving(KolobokView sender, EventArgs e);
        public event StartMoving OnMoving;
        public delegate void SetDirection(int sender);
        public event SetDirection PickDirection;

        public delegate void CreateBulletModel(KolobokView sender);
        public event CreateBulletModel StartShoot;

        public delegate void CreateBulletView(BulletView e);
        public CreateBulletView SpawnBullet;

        private MainStage main;
        private Rectangle[] rects = { new Rectangle(0, 3, 45, 45), new Rectangle(191,0,45,45), new Rectangle(100, 0, 45, 45), new Rectangle(285,0,45,45) };

        public KolobokView(MainStage _main)
        {
            shootCd = 0;
            countShootCd = 10;
            main = _main;
            SpawnBullet = new CreateBulletView(SpawnBulletView);
        }

        public void KeyNotPressed(object sender, KeyEventArgs e)
        {
            if (upPressed && e.KeyCode == Keys.W)
            {
                upPressed = false;
            }
            if (downPressed && e.KeyCode == Keys.S)
            {
                downPressed = false;
            }
            if (leftPressed && e.KeyCode == Keys.A)
            {
                leftPressed = false;
            }
            if (rightPressed && e.KeyCode == Keys.D)
            {
                rightPressed = false;
            }

            if (spacePressed && e.KeyCode == Keys.Space)
            {
                spacePressed = false;
            }
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!upPressed && e.KeyCode == Keys.W)
            {
                ChooseDirection(EnumDirections.Direction.UP);
                upPressed = true;
            }
            else if (!downPressed && e.KeyCode == Keys.S)
            {
                ChooseDirection(EnumDirections.Direction.DOWN);
                downPressed = true;
            }
            else if (!leftPressed && e.KeyCode == Keys.A)
            {
                ChooseDirection(EnumDirections.Direction.LEFT);
                leftPressed = true;
            }
            else if (!rightPressed && e.KeyCode == Keys.D)
            {
                ChooseDirection(EnumDirections.Direction.RIGHT);
                rightPressed = true;
            }

            if (e.KeyCode == Keys.Space)
            {
                spacePressed = true;
            }
        }

        public void UpdateLogick()
        {
            MoveToDirection();
            Shoot();
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            Position = e.Position;
        }
        public void ReadDirectionFromModel(DataTransfer e)
        {
            Direction = e.Direction;
        }

        private void MoveToDirection()
        {
            switch (Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    if (upPressed)
                    {
                        // Dispatch the 'on moved' event
                        OnMoving(this, EventArgs.Empty);
                    }
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    if (downPressed)
                    {
                        OnMoving(this, EventArgs.Empty);
                    }
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    if (leftPressed)
                    {
                        OnMoving(this, EventArgs.Empty);
                    }
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    if (rightPressed)
                    {
                        OnMoving(this, EventArgs.Empty);
                    }
                    break;
                default:
                    throw (new ArgumentException("No such argument!"));
            }
        }

        public void DrawYourSelf(Graphics g)
        {
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rects[Direction],GraphicsUnit.Pixel);
        }

        public void Shoot()
        {
            if(shootCd >= countShootCd)
            {
                if (spacePressed)
                {
                    shootCd = 0;
                    StartShoot?.Invoke(this);
                }
            }
            else
            {
                shootCd++;
            }
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            PickDirection((int)dir);
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
