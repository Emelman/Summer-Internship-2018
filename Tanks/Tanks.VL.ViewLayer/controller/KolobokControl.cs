using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.controller
{
    
    public class KolobokControl
    {
        
        private Kolobok_model model;

        public KolobokControl(Kolobok_model model)
        {
            this.model = model;
        }

        public Point moveUp()
        {
            return model.MoveUp();
        }

        public Point moveDown()
        {
            return model.MoveDown();
        }

        public Point moveLeft()
        {
            return model.MoveLeft();
        }

        public Point moveRight()
        {
            return model.MoveRight();
        }
    }

    

}
