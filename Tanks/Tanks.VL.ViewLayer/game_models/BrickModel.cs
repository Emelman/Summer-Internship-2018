using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.game_models
{
    public class BrickModel : CoreModel
    {
        public override Point Position { get => position; set => position = value; }
        public override int Direction { get => direction; set => direction = value; }
    }
}
