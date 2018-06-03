using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IViewObjectsCommon
    {
        void DrawYourSelf(Graphics g);
        void Shoot();
        void SpawnBulletView(BulletView bullet);
        void ChooseDirection(EnumDirections.Direction dir);
        void ReadPositionFromModel(DataTransfer e);
        void ReadDirectionFromModel(DataTransfer e);
    }
}
