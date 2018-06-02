using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IController
    {
        void DirectionChanged(Core_model model, int direction);
        void PositionChanged(Point pt);
    }
}
