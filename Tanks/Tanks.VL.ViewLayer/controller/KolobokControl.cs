using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Tanks.VL.ViewLayer.controller
{
    
    public class KolobokControl
    {
        public Boolean upPressed = false;
        public Boolean downPressed = false;
        public Boolean leftPressed = false;
        public Boolean rightPressed = false;
        public Boolean shootPressed = false;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public KolobokControl()
        {
            KeyDown += KeyPressed;
            KeyUp += KeyNotPressed;
        }
        private void KeyNotPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                upPressed = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                downPressed = false;
            }
            if (e.KeyCode == Keys.A)
            {
                leftPressed = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                rightPressed = false;
            }

            if(e.KeyCode == Keys.Space)
            {
                shootPressed = false;
            }
        }

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                upPressed = true;
            }
            else if(e.KeyCode == Keys.S)
            {
                downPressed = true;
            }
            if(e.KeyCode == Keys.A)
            {
                leftPressed = true;
            }
            else if(e.KeyCode == Keys.D)
            {
                rightPressed = true;
            }

            if(e.KeyCode == Keys.Space)
            {
                shootPressed = true;
            }
        }
    }

    

}
