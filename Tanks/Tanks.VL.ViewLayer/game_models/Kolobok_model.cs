using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_models
{
    public class Kolobok_model:IMoving
    {
        private Point position;
        private int direction;
        private int speed;
        private Size square;

        public event EventHandler OnDirectionChanged = (sender, e) => { };
        public event EventHandler OnPositionChanged = (sender, e) => { };
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPositionChanged(this, EventArgs.Empty);
                }
            }
        }
        public int Direction
        {
            get => direction;
            set
            {
                direction = value;
                OnDirectionChanged(this, EventArgs.Empty);
            }
        }
        public int Speed { get => speed; set => speed = value; }
        public Size Square { get => square; set => square = value; }

        public Point MoveUp()
        {
            var pt = Position;
            pt.Y -= Speed;
            if (pt.Y < 0)
            {
                pt.Y = 0;
            }
            Position = pt;
            return Position;
        }

        public Point MoveDown()
        {
            var pt = Position;
            pt.Y += Speed;
            if(pt.Y > 550)
            {
                pt.Y = 550;
            }
            Position = pt;
            return Position;
        }

        public Point MoveLeft()
        {
            var pt = Position;
            pt.X -= Speed;
            if (pt.X < 0)
            {
                pt.X = 0;
            }
            Position = pt;
            return Position;
        }

        public Point MoveRight()
        {
            var pt = Position;
            pt.X += Speed;
            if (pt.X > 550)
            {
                pt.X = 550;
            }
            Position = pt;
            return Position;
        }

        public void SetDirection(int direct)
        {
            Direction = direct;
        }

        
    }
}
