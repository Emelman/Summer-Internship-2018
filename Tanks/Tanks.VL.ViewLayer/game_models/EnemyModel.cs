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
    public class EnemyModel: CoreModel
    {
        public delegate void SetData(DataTransfer e);
        public event SetData OnDirectionChanged;
        public event SetData OnPositionChanged;

        public override Point Position
        {
            get
            {
                return position;
            }
            set
            {
                if(position != value)
                {
                    position = value;
                    OnPositionChanged?.Invoke(new DataTransfer(GetId, Direction, position));
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
                if (direction != value)
                {
                    direction = value;
                    OnDirectionChanged?.Invoke(new DataTransfer(GetId, direction));
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
