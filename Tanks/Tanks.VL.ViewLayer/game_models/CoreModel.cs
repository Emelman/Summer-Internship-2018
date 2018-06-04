using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_models
{
    public abstract class CoreModel:IModel
    {
        private int id;
        protected Point position;
        protected int direction;
        protected int speed;
        protected Size square;
        private Boolean toDelete;

        public int Speed { get => speed; set => speed = value; }
        public Size Square { get => square; set => square = value; }
        abstract public Point Position { get; set; }
        abstract public int Direction { get; set; }
        public int GetId { get => id; set => id = value; }
        public bool ToDelete { get => toDelete; set => toDelete = value; }
    }
}
