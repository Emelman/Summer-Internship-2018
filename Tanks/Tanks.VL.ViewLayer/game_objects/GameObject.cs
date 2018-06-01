using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class GameObject
    {
        private int id;
        private Point position;
        private int direction;
        private Size square;

        public int Id { get => id; set => id = value; }
        public Point Position { get => position; set => position = value; }
        public int Direction { get => direction; set => direction = value; }
        public Size Square { get => square; set => square = value; }
    }
}
