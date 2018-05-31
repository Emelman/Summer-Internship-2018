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
    public class PacmanController
    {
        private Kolobok_model model;

        public PacmanController(Kolobok_model model)
        {
            this.model = model;
        }

        public void moveUp()
        {
            model.MoveUp();
        }

        public void moveDown()
        {
            model.MoveDown();
        }

        public void moveLeft()
        {
            model.MoveLeft();
        }

        public void moveRight()
        {
            model.MoveRight();
        }

        public void SetDirection(int direction)
        {
            model.SetDirection(direction);
        }
    }
}
