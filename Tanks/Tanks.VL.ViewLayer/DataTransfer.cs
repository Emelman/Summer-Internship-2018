using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer
{
    public class DataTransfer : EventArgs
    {
        public DataTransfer(int _id, int _direction, Point pt = new Point())
        {
            Id = _id;
            Direction = _direction;
            Position = pt;
        }
        public int Id { get; private set; }
        public int Direction { get; private set; }
        public Point Position { get; private set; }
    }
}