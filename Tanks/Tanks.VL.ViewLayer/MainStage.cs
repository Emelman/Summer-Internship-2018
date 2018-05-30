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
        KolobokControl contrl;
        Kolobok_model model = new Kolobok_model();
        Kolobok hero;

        Timer gameTimer;
        Stopwatch watch = new Stopwatch();
        public MainStage(string[] args)
        {
            contrl = new KolobokControl(model);
            hero = new Kolobok(this,contrl);
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));

            
            hero.CreateBitmapAtRuntime();

            watch.Start();
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

        private void render()
        {
            if(this == null) throw new NotImplementedException();
        }

        private void update()
        {
            var something = 10;
            if(something == 11)
            {
                throw new NotImplementedException();
            }
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
