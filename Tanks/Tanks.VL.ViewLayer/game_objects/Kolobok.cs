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
        PictureBox pictureBox1 = new PictureBox();
        Form stage;
        static Image pic = Image.FromFile("../../pics/hero.png");
        KolobokControl contrl;


        Point position;
        string direction;
        int shootCd;
        int countShootCd;

        public Kolobok(Form myStage)
        {
            stage = myStage;
            contrl = new KolobokControl();
        }

        
        public void CreateBitmapAtRuntime()
        {
            stage.Controls.Add(pictureBox1);
            pictureBox1.Image = pic;
        }

        public void ChooseDirection()
        {
            throw new NotImplementedException();
        }

        public void MoveYourSelf()
        {
            throw new NotImplementedException();
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
