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
    public class KolobokModel: CoreModel
    {
        public delegate void SetData(DataTransfer e);
        public event SetData OnDirectionChanged;
        public event SetData OnPositionChanged;
        override public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPositionChanged?.Invoke(new DataTransfer(GetId, Direction, position));
            }
        }
        override public int Direction
        {
            get => direction;
            set
            {
                direction = value;
                OnDirectionChanged?.Invoke(new DataTransfer(GetId, direction));
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
