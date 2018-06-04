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

        Button toMainMenu;
        TextBox gameEndLabel;
        TextBox gameScoreLabel;
        Boolean showStatistics = false;
        Boolean isWinner = false;
        MainMenu menu;
        Timer gameTimer;
        public string[] args;

        public List<BulletView> BulletsToDraw { get => bulletsToDraw; set => bulletsToDraw = value; }
        public MainStage(string[] _args)
        {
            this.args = _args;
            menu = new MainMenu(this);
            toMainMenu = new Button();
            gameEndLabel = new TextBox();
            gameScoreLabel = new TextBox();
            this.Size = new Size(int.Parse(args[0]) + 17, int.Parse(args[1]) + 40);
        }

        public void InitGameStates(object sender, EventArgs e)
        {
            InitializeComponent();
            menu.Dispose();
            InitGameObjects(args);
            InitGraphics(args);
            StartGameUpdateLogick();
            
            this.Size = new Size(int.Parse(args[0]) + 17, int.Parse(args[1]) + 40);
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
            var appleModels = control.GetApplesModels();
            for(var i =0; i < appleModels.Count; i++)
            {
                var apple = appleModels[i];
                AppleView app = new AppleView();
                app.Id = apple.GetId;
                app.Position = apple.Position;
                app.Square = apple.Square;
                applesToDraw.Add(app);
            }
        }
        private void InitGraphics(string[] args)
        {
            //draw scene 
            mainStageBox = new PictureBox();
            mainStageBox.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));
            this.Controls.Add(mainStageBox);
            drawStage = new Bitmap(int.Parse(args[0]), int.Parse(args[1]));
            button1.Parent = mainStageBox;
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

            gameEndLabel.Font = new Font(gameEndLabel.Font.FontFamily, 46,FontStyle.Bold);
            gameEndLabel.TextAlign = HorizontalAlignment.Center;
            if (!isWinner)
            {
                gameEndLabel.Text = "Game Over";
            }
            else
            {
                gameEndLabel.Text = "Game Complete";
            }
            gameEndLabel.Width = 450;
            gameEndLabel.BackColor = Color.Gray;
            gameEndLabel.SelectionLength = 0;
            gameEndLabel.Location = new Point(this.Width/2 - gameEndLabel.Width / 2, 150);
            mainStageBox.Controls.Add(gameEndLabel);

            gameScoreLabel.Font = new Font(gameEndLabel.Font.FontFamily, 25, FontStyle.Bold);
            gameScoreLabel.TextAlign = HorizontalAlignment.Center;
            gameScoreLabel.Text = "Your score: " + control.GetScore().ToString();
            gameScoreLabel.Width = 300;
            gameScoreLabel.BackColor = Color.Gray;
            gameScoreLabel.SelectionLength = 0;
            gameScoreLabel.Location = new Point(this.Width / 2 - gameScoreLabel.Width / 2, 250);
            mainStageBox.Controls.Add(gameScoreLabel);

            toMainMenu.Size = new Size(100, 40);
            toMainMenu.Location = new Point(this.Width/2 - toMainMenu.Width / 2, 450);
            toMainMenu.Text = "TO MAIN MENU";
            toMainMenu.BackColor = Color.Gray;
            toMainMenu.Click += new EventHandler(CallMainMenu);
            mainStageBox.Controls.Add(toMainMenu);
            
        }

        private void CallMainMenu(object sender, EventArgs e)
        {
            toMainMenu.Click -= CallMainMenu;
            mainStageBox.Controls.Remove(gameEndLabel);
            mainStageBox.Controls.Remove(gameScoreLabel);
            mainStageBox.Controls.Remove(toMainMenu);
            this.Controls.Remove(mainStageBox);
            menu.InitMainMenu();
        }


        private void GameUpdate(Object sender, EventArgs e)
        {
            gameTimer.Stop();
            UpdateFromModel();
            GameCollisions();
            Render();
            if(showStatistics)
            {
                control.GetStatisticBoard().UpdateModel();
            }
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
                if (bulletsToDraw[i].ToDelete)
                {
                    control.DeleteBullet(bulletsToDraw[i].Id);
                    BulletsToDraw.Remove(bulletsToDraw[i]);
                }
                else
                {
                    bulletsToDraw[i].Update();
                }
            }
            if(control.GetEnemyModels().Count <= 0)
            {
                isWinner = true;
                StopGameUpdateLogick();
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
                    //ServiceLib.WarningMessage("GAME OVER!", "Warning", MessageBoxButtons.OK);
                    StopGameUpdateLogick();
                    return;
                }

                for (var f = 0; f < bulletsToDraw.Count; f++)
                {
                    var bullet = control.GetBulletById(bulletsToDraw[f].Id);
                    if (!bulletsToDraw[f].isEnemyBullet)
                    {
                        if (CollisionTests.CheckBulletCollsion(bullet,
                            enemyToCheck))
                        {
                            control.DeleteEnemy(enemyToDraw[i].Id);
                            control.DeleteBullet(bulletsToDraw[f].Id);
                            enemyToDraw.Remove(enemyToDraw[i]);
                            bulletsToDraw.Remove(bulletsToDraw[f]);
                            f++;
                            break;
                        }
                    }
                    else
                    {
                        if (CollisionTests.CheckBulletCollsion(bullet, control.GetHeroModel()))
                        {
                            //ServiceLib.WarningMessage("GAME OVER!", "Warning", MessageBoxButtons.OK);
                            StopGameUpdateLogick();
                            return;
                        } 
                    }
                }
            }

            for (var f = 0; f < bulletsToDraw.Count; f++)
            {
                var bullet = control.GetBulletById(bulletsToDraw[f].Id);
                for (var j = 0; j < wallToDraw.Count; j++)
                {
                    if (CollisionTests.CheckBulletCollsion(bullet,
                        control.GetBrickById(wallToDraw[j].Id)))
                    {
                        control.DeleteBrick(wallToDraw[j].Id);
                        control.DeleteBullet(bulletsToDraw[f].Id);
                        wallToDraw.Remove(wallToDraw[j]);
                        bulletsToDraw.Remove(bulletsToDraw[f]);
                        break;
                    }
                }
            }

            for (var i=0; i < applesToDraw.Count; i++)
            {
                if (CollisionTests.CheckHeroWallCollision(control.GetHeroModel(), control.GetAppleById(applesToDraw[i].Id)))
                {
                    control.DeleteApple(applesToDraw[i].Id);
                    applesToDraw.Remove(applesToDraw[i]);
                    control.UpdateGameScore();
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
                for(var i=0; i < applesToDraw.Count; i++)
                {
                    applesToDraw[i].DrawYourSelf(g);
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

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (showStatistics)
            {
                showStatistics = false;
                control.GetStatisticBoard().Hide();
                mainStageBox.Focus();
                this.KeyPreview = true;
            }
            else
            {
                showStatistics = true;
                control.GetStatisticBoard().Show();
                mainStageBox.Focus();
                this.KeyPreview = true;
            }
        }
    }
}
