using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.game_models
{
    class Enemy_model:IMoving
    {
        private int id;
        private Point position;
        private int direction;
        private int speed;

        public int Id { get => id; set => id = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Direction { get => direction; set => direction = value; }
        public Point Position { get => position; set => position = value; }

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
            if (position.Y > 600)
            {
                position.Y = 600;
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
            if (position.X > 600)
            {
                position.X = 600;
            }
            return position;
        }

        public void UpdateFromModel(Enemy foe)
        {
            foe.Position = Position;
            foe.Direction = Direction;
        }


    }
}
