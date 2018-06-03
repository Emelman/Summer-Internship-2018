using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class AppleView : GameObject, IViewObjectsCommon
    {
        public AppleView()
        {
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            throw new NotImplementedException();
        }

        public void DrawYourSelf(Graphics g)
        {
            throw new NotImplementedException();
        }

        public void ReadDirectionFromModel(DataTransfer e)
        {
            throw new NotImplementedException();
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void SpawnBulletView(BulletView bullet)
        {
            throw new NotImplementedException();
        }
    }
}
