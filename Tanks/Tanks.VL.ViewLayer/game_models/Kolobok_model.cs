using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.game_models
{
    public class Kolobok_model:IMoving
    {
        private Point position;
        private int direction;
        private int speed;

        public Point Position { get => position; set => position = value; }
        public int Direction { get => direction; set => direction = value; }
        public int Speed { get => speed; set => speed = value; }

        public Point MoveUp()
        {
            position.Y -= Speed;
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            return position;
        }

        public Point MoveDown()
        {
            position.Y += Speed;
            if(position.Y > 550)
            {
                position.Y = 550;
            }
            return position;
        }

        public Point MoveLeft()
        {
            position.X -= Speed;
            if (position.X < 0)
            {
                position.X = 0;
            }
            return position;
        }

        public Point MoveRight()
        {
            position.X += Speed;
            if (position.X > 550)
            {
                position.X = 550;
            }
            return position;
        }

        public void SetDirection(int direct)
        {
            Direction = direct;
        }

        public void UpdateFromModel(KolobokView kolobok)
        {
            kolobok.Position = Position;
            kolobok.Direction = Direction;
        }
    }
}
