using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer
{
    public class DataTransfer : EventArgs
    {
        public DataTransfer(int _id, int _direction)
        {
            id = _id;
            direction = _direction;
        }

        public int id { get; private set; }
        public int direction { get; private set; }

    }
}