using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IMoving
    {
        Point MoveUp(CoreModel model);
        Point MoveDown(CoreModel model);
        Point MoveLeft(CoreModel model);
        Point MoveRight(CoreModel model);
    }
}
