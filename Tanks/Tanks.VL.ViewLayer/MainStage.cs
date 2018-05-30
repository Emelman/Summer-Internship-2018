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
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer
{
    public partial class MainStage : Form
    {

        Timer gameTimer;
        Stopwatch watch = new Stopwatch();
        public MainStage(string[] args)
        {
            InitializeComponent();
            this.Size = new Size(int.Parse(args[0]), int.Parse(args[1]));

            Kolobok hero = new Kolobok(this);
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
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            watch.Start();

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

    }
}
