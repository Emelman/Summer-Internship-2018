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
    public class Kolobok_model: Core_model
    {
        public event EventHandler OnDirectionChanged = (sender, e) => { };
        public event EventHandler OnPositionChanged = (sender, e) => { };
        override public Point Position
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
        override public int Direction
        {
            get => direction;
            set
            {
                if (direction != value)
                {
                    direction = value;
                    OnDirectionChanged(this, EventArgs.Empty);
                }
            }
        }

        public void DirectionChanged(int direction)
        {
            Direction = direction;
        }

        public void PositionChanged(Point pt)
        {
            Position = pt;
        }
    }
}
