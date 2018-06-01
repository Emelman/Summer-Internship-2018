using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.game_models
{
    class Enemy_model
    {
        private int id;
        private Point position;
        private int direction;
        private int speed;
        private Size square;

        public event EventHandler OnDirectionChanged = (sender, e) => { };
        public event EventHandler OnPositionChanged = (sender, e) => { };

        public int Id { get => id; set => id = value; }
        public int Speed { get => speed; set => speed = value; }
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
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                OnDirectionChanged(this, EventArgs.Empty);
            }
        }

        public Size Square { get => square; set => square = value; }

        public void SetDirection(int direct)
        {
            Direction = direct;
        }

        public void ChangePosition(Point pt)
        {
            Position = pt;
        }
    }
}
