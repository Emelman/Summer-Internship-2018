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
    public class Enemy_model: Core_model
    {
        public event EventHandler OnDirectionChanged = (sender, e) => { };
        public event EventHandler OnPositionChanged = (sender, e) => { };

        public override Point Position
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
        public override int Direction
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
