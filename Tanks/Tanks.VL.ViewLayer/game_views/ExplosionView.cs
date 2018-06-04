using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_objects;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_views
{
    public class ExplosionView : GameObject, IViewObjectsCommon
    {
        private Rectangle[] rect = { new Rectangle(775, 389, 37, 37), new Rectangle(814, 385, 48, 48), new Rectangle(863, 382, 52, 52), new Rectangle(814, 385, 48, 48) , new Rectangle(775, 389, 37, 37) };

        private int frame;
        public Boolean stopAnim;
        public ExplosionView()
        {
            frame = 0;
            stepCD = 0;
            maxCD = 3;
            stopAnim = false;
        }
        public void ChooseDirection(EnumDirections.Direction dir)
        {
            throw new NotImplementedException();
        }

        public void DrawYourSelf(Graphics g)
        {
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rect[frame], GraphicsUnit.Pixel);
            if (stepCD >= maxCD)
            {
                stepCD = 0;
                g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rect[frame], GraphicsUnit.Pixel);
                frame++;
                if (frame >= 5)
                {
                    stopAnim = true;
                }
            }
            else stepCD++;
        }

        public void ReadDirectionFromModel(DataTransfer e)
        {
            throw new NotImplementedException();
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            Position = e.Position;
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
