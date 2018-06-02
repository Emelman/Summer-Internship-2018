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

        KolobokView hero;
        List<TankView> enemyToDraw;

        Timer gameTimer;

        public MainStage(string[] args)
        {
            InitGameStates(args);
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0])+17, int.Parse(args[1])+40);
        }

        private void InitGameStates(string[] args)
        {
            InitGameObjects(args);
            InitGraphics(args);
            StartGameUpdateLogick();
        }

        private void InitGameObjects(string[] args)
        {
            contrl = new PacmanController(args);
            hero = new KolobokView();
            contrl.InitHeroViewEvents(hero);
            enemyToDraw = new List<TankView>();
            var enenmyModels = contrl.GetEnemy_Models();
            for (var i = 0; i < enenmyModels.Count; i++)
            {
                var enemy = enenmyModels[i];
                TankView foe = new TankView();
                foe.Id = enemy.GetId;
                foe.Direction = enemy.Direction;
                foe.Position = enemy.Position;
                foe.Square = enemy.Square;
                contrl.InitEnemyViewEvents(foe);
                enemyToDraw.Add(foe);
            }
        }
        private void InitGraphics(string[] args)
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
            hero.UpdateLogick();
            hero.ReadModel(contrl.GetHeroModel());
            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                enemyToDraw[i].UpdateLogick();
                enemyToDraw[i].ReadModel(contrl.GetEnemyById(enemyToDraw[i].Id));
            }
            for(var i = 0; i < enemyToDraw.Count; i++)
            {
                //if (contrl.CheckCollision(contrl.GetHeroModel(), contrl.GetEnemyById(enemyToDraw[i].Id)))
                //{
                //    ServiceLib.WarningMessage("GAME OVER!", "Warning", MessageBoxButtons.OK);
                //}
                for(var j = i+1; j < enemyToDraw.Count; j++)
                {
                    if (contrl.CheckCollision(contrl.GetEnemyById(enemyToDraw[i].Id), contrl.GetEnemyById(enemyToDraw[j].Id)))
                    {
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
