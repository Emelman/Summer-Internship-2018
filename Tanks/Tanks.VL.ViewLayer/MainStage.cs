using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer
{
    public partial class MainStage : Form
    {
        PictureBox mainStageBox;
        PacmanController contrl;
        EnemyController foeControl;
        KolobokView hero;
        Bitmap drawStage;
        Level_model lvlModel;


        Timer gameTimer;
        public MainStage(string[] args)
        {
            initGameStates(args);
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0])+17, int.Parse(args[1])+40);
        }

        private void initGameStates(string[] args)
        {
            lvlModel = new Level_model(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));
            contrl = new PacmanController(lvlModel.Hero);
            hero = new KolobokView(contrl);
            foeControl = new EnemyController();
            //draw scene 
            mainStageBox = new PictureBox();
            mainStageBox.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));
            this.Controls.Add(mainStageBox);
            drawStage = new Bitmap(int.Parse(args[0]), int.Parse(args[1]));

            StartGameUpdateLogick();
        }

        private void StartGameUpdateLogick()
        {
            gameTimer = new Timer();
            gameTimer.Tick += new EventHandler(gameUpdate);
            gameTimer.Interval = 17;
            gameTimer.Start();
        }

        private void gameUpdate(Object sender, EventArgs e)
        {
            gameTimer.Stop();
            var enemys = lvlModel.GetEnemyViewModels(foeControl);
            update(enemys);
            render(enemys);
            gameTimer.Start();
        }

        private void update(List<Enemy> enemys)
        {
            hero.UpdateLogick();
            lvlModel.Hero.UpdateFromModel(hero);

            for (var i = 0; i < enemys.Count; i++)
            {

            }
        }

        private void render(List<Enemy> enemys)
        {
            //Bitmap bmp = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(drawStage))
            {
                g.Clear(Color.Gray);
                //hero.ChooseDirection();
                hero.MoveYourSelf(g);

                for (var i = 0; i < enemys.Count; i++)
                {
                    enemys[i].MoveYourSelf(g);
                }
            }
            mainStageBox.Image = drawStage;
        }

        private void MainStage_KeyDown(object sender, KeyEventArgs e)
        {
            hero.KeyPressed(sender, e);
        }

        private void MainStage_KeyUp(object sender, KeyEventArgs e)
        {
            hero.KeyNotPressed(sender, e);
        }
    }
}
