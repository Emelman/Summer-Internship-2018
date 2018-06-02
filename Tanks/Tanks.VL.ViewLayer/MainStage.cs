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

        PacmanController control;

        KolobokView hero;
        List<TankView> enemyToDraw;
        List<BrickView> wallToDraw;

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
            control = new PacmanController(args);
            hero = new KolobokView();
            control.InitHeroViewEvents(hero);
            enemyToDraw = new List<TankView>();
            wallToDraw = new List<BrickView>();
            var enenmyModels = control.GetEnemyModels();
            for (var i = 0; i < enenmyModels.Count; i++)
            {
                var enemy = enenmyModels[i];
                TankView foe = new TankView();
                foe.Id = enemy.GetId;
                foe.Direction = enemy.Direction;
                foe.Position = enemy.Position;
                foe.Square = enemy.Square;
                control.InitEnemyViewEvents(foe);
                enemyToDraw.Add(foe);
            }

            var brickModels = control.GetBricksModels();
            for(var i = 0; i < brickModels.Count; i++)
            {
                var brick = brickModels[i];
                BrickView wall = new BrickView();
                wall.Id = brick.GetId;
                wall.Position = brick.Position;
                wall.Square = brick.Square;
                wallToDraw.Add(wall);
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
            gameTimer.Tick += new EventHandler(GameUpdate);
            gameTimer.Interval = 17;
            gameTimer.Start();
        }

        private void GameUpdate(Object sender, EventArgs e)
        {
            gameTimer.Stop();
            update();
            render();
            gameTimer.Start();
        }

        private void update()
        {
            hero.UpdateLogick();
            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                enemyToDraw[i].UpdateLogick();
            }
            for (var i = 0; i < wallToDraw.Count; i++)
            {
                if(ServiceLib.CheckWallCollision(control.GetHeroModel(), control.GetBrickById(wallToDraw[i].Id)))
                {
                    if(hero.Position.X > wallToDraw[i].Position.X)
                    {
                        if (hero.Position.Y > wallToDraw[i].Position.Y)
                        {
                            if (wallToDraw[i].Position.X + wallToDraw[i].Square.Width - hero.Position.X > wallToDraw[i].Position.Y + wallToDraw[i].Square.Height - hero.Position.Y)
                            {

                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (hero.Position.Y > wallToDraw[i].Position.Y)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }

            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                //if (ServiceLib.CheckCollision(control.GetHeroModel(), control.GetEnemyById(enemyToDraw[i].Id)))
                //{
                //    ServiceLib.WarningMessage("GAME OVER!", "Warning", MessageBoxButtons.OK);
                //}
                for (var j = i+1; j < enemyToDraw.Count; j++)
                {
                    if (ServiceLib.CheckCollision(control.GetEnemyById(enemyToDraw[i].Id), 
                        control.GetEnemyById(enemyToDraw[j].Id)))
                    {
                        break;
                    }
                }

                for (var m = 0; m < wallToDraw.Count; m++) 
                {
                    if (ServiceLib.CheckWallCollision(control.GetEnemyById(enemyToDraw[i].Id), 
                        control.GetBrickById(wallToDraw[m].Id)))
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
                for (var i = 0; i < wallToDraw.Count; i++)
                {
                    wallToDraw[i].DrawYourSelf(g);
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
