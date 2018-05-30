using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.game_models
{
    public class Kolobok_model
    {
        public Point position;
        public String direction;
        public int speed = 6;

        public Point MoveUp()
        {
            position.Y -= speed;
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            return position;
        }

        public Point MoveDown()
        {
            position.Y += speed;
            if(position.Y > 600)
            {
                position.Y = 600;
            }
            return position;
        }

        public Point MoveLeft()
        {
            position.X -= speed;
            if (position.X < 0)
            {
                position.X = 0;
            }
            return position;
        }

        public Point MoveRight()
        {
            position.X += speed;
            if (position.X > 600)
            {
                position.X = 600;
            }
            return position;
        }
    }
}
