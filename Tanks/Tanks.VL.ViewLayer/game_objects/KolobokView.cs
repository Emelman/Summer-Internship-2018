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
        public static Boolean shootPressed = false;

        public delegate void StartMoving(KolobokView sender, EventArgs e);
        public event StartMoving OnMoving;
        public delegate void SetDirection(int sender);
        public event SetDirection PickDirection;
        //public event EventHandler OnMoving = (sender, e) => { };

        int shootCd;
        int countShootCd;
        private Image pic = AllGameImages.heroTank;
        private Rectangle[] rects = { new Rectangle(0, 3, 45, 45), new Rectangle(191,0,45,45), new Rectangle(100, 0, 45, 45), new Rectangle(285,0,45,45) };

        public KolobokView()
        {
            shootCd = 0;
            countShootCd = 30;
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

            if (shootPressed && e.KeyCode == Keys.Space)
            {
                shootPressed = false;
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
                shootPressed = true;
            }
        }

        public void UpdateLogick()
        {
            MoveToDirection();
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
            if(shootCd <= countShootCd)
            {
                shootCd = 0;
                Bullet piu = new Bullet();
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

        

        //private void DrawExample()
        //{
        //    pictureBox1.Size = new Size(210, 110);
        //    Bitmap flag = new Bitmap(200, 100);
        //    Graphics flagGraphics = Graphics.FromImage(flag);
        //    int red = 0;
        //    int white = 11;
        //    while (white <= 100)
        //    {
        //        flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
        //        flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
        //        red += 20;
        //        white += 20;
        //    }
        //    pictureBox1.Image = flag;
        //}

        //public void Example()
        //{
        //    using (Image img = Image.FromFile(input))
        //    {
        //        //rotate the picture by 90 degrees and re-save the picture as a Jpeg
        //        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
        //        img.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    }

        //    Bitmap returnBitmap = new Bitmap(b.Height, b.Width);
        //    Graphics g = Graphics.FromImage(returnBitmap);
        //    g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
        //    g.RotateTransform(90);
        //    g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
        //    g.DrawImage(b, new Point(0, 0));
        //    return returnBitmap;
        //}
    }
}
