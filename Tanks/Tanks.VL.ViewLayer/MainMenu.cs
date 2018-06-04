using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks.VL.ViewLayer
{
    class MainMenu
    {
        PictureBox box;
        Bitmap drawStage;
        Button newGame;
        MainStage main;
        public MainMenu(MainStage _main)
        {
            main = _main;
            InitMainMenu();
        }

        private void InitMainMenu()
        {
            box = new PictureBox();
            box.Size = new Size(int.Parse(main.args[0]), int.Parse(main.args[1]));
            main.Controls.Add(box);
            drawStage = new Bitmap(int.Parse(main.args[0]), int.Parse(main.args[1]));

            using (Graphics g = Graphics.FromImage(drawStage))
            {
                g.Clear(Color.Black);

                g.DrawImage(AllGameImages.all_sprites, 200, 200, new Rectangle(285, 0, 45, 45), GraphicsUnit.Pixel);
                g.DrawImage(AllGameImages.all_sprites, 250, 200, new Rectangle(770, 0, 50, 50), GraphicsUnit.Pixel);
                g.DrawImage(AllGameImages.all_sprites, 300, 200, new Rectangle(815, 334, 48, 48), GraphicsUnit.Pixel);
                g.DrawImage(AllGameImages.all_sprites, 350, 200, new Rectangle(670, 0, 45, 45), GraphicsUnit.Pixel);
                g.DrawImage(AllGameImages.all_sprites, 400, 200, new Rectangle(817, 145, 48, 48), GraphicsUnit.Pixel);
            }
            box.Image = drawStage;

            newGame = new Button();
            newGame.BackColor = Color.Gray;
            newGame.Size = new Size(80, 40);
            newGame.Text = "New Game";
            newGame.Location = new Point(300 - newGame.Width / 2, 450);
            box.Controls.Add(newGame);
            newGame.Click += new EventHandler(main.InitGameStates);
        }

        public void Dispose()
        {
            newGame.Click -= main.InitGameStates;
            main.Controls.Remove(box);
        }
    }


}
