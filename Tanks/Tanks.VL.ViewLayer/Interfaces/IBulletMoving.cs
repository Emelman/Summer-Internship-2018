using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IBulletMoving
    {
        Point MoveBulletUp(CoreModel model);
        Point MoveBulletDown(CoreModel model);
        Point MoveBulletLeft(CoreModel model);
        Point MoveBulletRight(CoreModel model);
    }
}
