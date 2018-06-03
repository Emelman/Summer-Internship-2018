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

        public PacmanController control;

        KolobokView heroToDraw;
        List<TankView> enemyToDraw;
        List<BrickView> wallToDraw;
        List<BulletView> bulletsToDraw;
        List<AppleView> applesToDraw;

        Timer gameTimer;

        public List<BulletView> BulletsToDraw { get => bulletsToDraw; set => bulletsToDraw = value; }

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
            heroToDraw = new KolobokView(this);
            control.InitHeroViewEvents(heroToDraw);
            enemyToDraw = new List<TankView>();
            wallToDraw = new List<BrickView>();
            bulletsToDraw = new List<BulletView>();
            applesToDraw = new List<AppleView>();

            var enenmyModels = control.GetEnemyModels();
            for (var i = 0; i < enenmyModels.Count; i++)
            {
                var enemy = enenmyModels[i];
                TankView foe = new TankView(this);
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

        private void StopGameUpdateLogick()
        {
            gameTimer.Stop();
            gameTimer.Tick -= GameUpdate;
        }

        private void GameUpdate(Object sender, EventArgs e)
        {
            gameTimer.Stop();
            UpdateFromModel();
            GameCollisions();
            Render();
            gameTimer.Start();
        }
        private void UpdateFromModel()
        {
            heroToDraw.UpdateLogick();
            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                enemyToDraw[i].UpdateLogick();
            }
            for(var i = 0; i < bulletsToDraw.Count; i++)
            {
                bulletsToDraw[i].Update();
            }
        }
        private void GameCollisions()
        {
            for (var i = 0; i < enemyToDraw.Count; i++)
            {
                //hero colision test with enemys
                var enemyToCheck = control.GetEnemyById(enemyToDraw[i].Id);
                if (CollisionTests.CheckCollision(control.GetHeroModel(), enemyToCheck))
                {
                    ServiceLib.WarningMessage("GAME OVER!", "Warning", MessageBoxButtons.OK);
                }
                //enemy collision test with each other
                for (var j = i + 1; j < enemyToDraw.Count; j++)
                {
                    if (CollisionTests.CheckCollision(enemyToCheck,
                        control.GetEnemyById(enemyToDraw[j].Id)))
                    {
                        break;
                    }
                }

                for (var f = 0; f < bulletsToDraw.Count; f++)
                {
                    if (!bulletsToDraw[f].isEnemyBullet)
                    {
                        if (CollisionTests.CheckBulletCollsion(control.GetBulletById(bulletsToDraw[f].Id),
                            enemyToCheck))
                        {
                            control.DeleteEnemy(enemyToDraw[i].Id);
                            control.DeleteBullet(bulletsToDraw[f].Id);
                            enemyToDraw.Remove(enemyToDraw[i]);
                            bulletsToDraw.Remove(bulletsToDraw[f]);
                            break;
                        }
                    }
                }
            }



            for (var i = 0; i < wallToDraw.Count; i++)
            {
                if (CollisionTests.CheckHeroWallCollision(control.GetHeroModel(), control.GetBrickById(wallToDraw[i].Id)))
                {
                    if (heroToDraw.Position.X > wallToDraw[i].Position.X)
                    {
                        if (heroToDraw.Position.Y > wallToDraw[i].Position.Y)
                        {
                            if (wallToDraw[i].Position.X + wallToDraw[i].Square.Width - heroToDraw.Position.X > wallToDraw[i].Position.Y + wallToDraw[i].Square.Height - heroToDraw.Position.Y)
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
                        if (heroToDraw.Position.Y > wallToDraw[i].Position.Y)
                        {

                        }
                        else
                        {

                        }
                    }
                }

                for (var m = 0; m < enemyToDraw.Count; m++)
                {
                    if (CollisionTests.CheckEnemyWallCollision(control.GetEnemyById(enemyToDraw[m].Id),
                        control.GetBrickById(wallToDraw[i].Id)))
                    {
                        break;
                    }
                }
            }
        }

        private void Render()
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
                for(var i = 0; i < bulletsToDraw.Count; i++)
                {
                    bulletsToDraw[i].DrawYourSelf(g);
                }
                heroToDraw.DrawYourSelf(g);
            }
            mainStageBox.Image = drawStage;
        }

        private void MainStage_KeyDown(object sender, KeyEventArgs e)
        {
            heroToDraw.KeyPressed(sender, e);
        }

        private void MainStage_KeyUp(object sender, KeyEventArgs e)
        {
            heroToDraw.KeyNotPressed(sender, e);
        }
    }
}
