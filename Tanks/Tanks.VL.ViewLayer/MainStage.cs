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
        Bitmap drawStage;

        PacmanController contrl;
        List<EnemyController> foeControls;

        Level_model lvlModel;

        KolobokView hero;
        List<TankView> enemyToDraw;

        Timer gameTimer;

        public MainStage(string[] args)
        {
            initGameStates(args);
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0])+17, int.Parse(args[1])+40);
        }

        private void initGameStates(string[] args)
        {
            initModel(args);
            initView();
            initControllers();
            initGraphics(args);
            StartGameUpdateLogick();
        }

        private void initModel(string[] args)
        {
            lvlModel = new Level_model(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));
        }

        private void initView()
        {
            hero = new KolobokView();
            enemyToDraw = lvlModel.GetEnemyViewModels();
        }

        private void initControllers()
        {
            contrl = new PacmanController(lvlModel.Hero, hero);
            var enenmyModels = lvlModel.GetEnemy_Models();
            foeControls = new List<EnemyController>();
            for(var i=0; i < enemyToDraw.Count; i++)
            {
                var ctrl = new EnemyController(enenmyModels[i], enemyToDraw[i]);
                foeControls.Add(ctrl);
            }
        }

        private void initGraphics(string[] args)
        {
            //draw scene 
            mainStageBox = new PictureBox();
            mainStageBox.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));
            this.Controls.Add(mainStageBox);
            drawStage = new Bitmap(int.Parse(args[0]), int.Parse(args[1]));
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
            update();
            render();
            gameTimer.Start();
        }

        private void update()
        {
            contrl.Update();
            
            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                enemyToDraw[i].UpdateLogick();
            }
            for(var i = 0; i < foeControls.Count; i++)
            {
                for(var j = i+1; j < foeControls.Count; j++)
                {
                    if (foeControls[i].CheckCollision(foeControls[j].EnemyModelPosition()))
                    {
                        foeControls[j].SwitchDirection();
                        i++;
                        break;
                    }
                }
            }
        }

        private void render()
        {
            //Bitmap bmp = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(drawStage))
            {
                g.Clear(Color.Gray);
                for (var i = 0; i < enemyToDraw.Count; i++)
                {
                    enemyToDraw[i].DrawYourSelf(g);
                }
                hero.DrawYourSelf(g);
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
