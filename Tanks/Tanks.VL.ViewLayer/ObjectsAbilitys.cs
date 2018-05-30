using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer
{
    public interface ObjectsAbilitys
    {
        void MoveYourSelf(Point pt);
        void Shoot();
        void ChooseDirection();

    }
}
