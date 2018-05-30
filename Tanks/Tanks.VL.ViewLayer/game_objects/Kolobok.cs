using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.controller;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class Kolobok : ObjectsAbilitys
    {
        public static Boolean upPressed = false;
        public static Boolean downPressed = false;
        public static Boolean leftPressed = false;
        public static Boolean rightPressed = false;
        public static Boolean shootPressed = false;

        PictureBox pictureBox1 = new PictureBox();
        Form stage;
        static Image pic = Image.FromFile("../../pics/hero.png");

        Point position;
        string direction;
        int shootCd;
        int countShootCd;
        private KolobokControl contrl;

        public Kolobok(Form myStage, KolobokControl contrl)
        {
            stage = myStage;
            this.contrl = contrl;
            position = new Point(130, 120);
        }

        public void KeyNotPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                upPressed = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                downPressed = false;
                
            }
            if (e.KeyCode == Keys.A)
            {
                leftPressed = false;
                
            }
            else if (e.KeyCode == Keys.D)
            {
                rightPressed = false;
                
            }

            if (e.KeyCode == Keys.Space)
            {
                shootPressed = false;
            }
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                upPressed = true;
                MoveYourSelf(contrl.moveUp());
            }
            else if (e.KeyCode == Keys.S)
            {
                downPressed = true;
                MoveYourSelf(contrl.moveDown());
            }
            if (e.KeyCode == Keys.A)
            {
                leftPressed = true;
                MoveYourSelf(contrl.moveLeft());
            }
            else if (e.KeyCode == Keys.D)
            {
                rightPressed = true;
                MoveYourSelf(contrl.moveRight());
            }

            if (e.KeyCode == Keys.Space)
            {
                shootPressed = true;
            }
        }

        public void CreateBitmapAtRuntime()
        {
            stage.Controls.Add(pictureBox1);
            pictureBox1.Size = new Size(600, 600);
            //pictureBox1.Location = new Point(130, 70);
            Bitmap bmp = new Bitmap(600,600);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                
                g.DrawImage(pic, position);
            }
            pictureBox1.Image = bmp;
        }

        public void ChooseDirection()
        {
            throw new NotImplementedException();
        }

        public void MoveYourSelf(Point newPos)
        {
            position = newPos;
            pictureBox1.Size = new Size(600, 600);
            //pictureBox1.Location = new Point(130, 70);
            Bitmap bmp = new Bitmap(600, 600);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(pic, position);
            }
            pictureBox1.Image = bmp;
        }

        public void Shoot()
        {
            if(shootCd <= countShootCd)
            {
                Bullet piu = new Bullet();
            }
        }


        private void DrawExample()
        {
            pictureBox1.Size = new Size(210, 110);
            Bitmap flag = new Bitmap(200, 100);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int red = 0;
            int white = 11;
            while (white <= 100)
            {
                flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
                flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
                red += 20;
                white += 20;
            }
            pictureBox1.Image = flag;
        }

    }
}
