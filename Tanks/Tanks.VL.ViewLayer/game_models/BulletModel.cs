using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.game_models
{
    public class BulletModel : CoreModel
    {
        public delegate void SetData(DataTransfer e);
        public event SetData OnDirectionChanged;
        public event SetData OnPositionChanged;
        public Boolean isEnemyBullet;

        public override Point Position
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
        public override int Direction
        {
            get
            {
                return direction;
            }
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
