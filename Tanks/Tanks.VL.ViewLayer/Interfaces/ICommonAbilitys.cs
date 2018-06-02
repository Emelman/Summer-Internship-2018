using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface ICommonAbilitys
    {
        void DrawYourSelf(Graphics g);
        void Shoot();
        void ChooseDirection(EnumDirections.Direction dir);

    }
}
