using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class BrickView : GameObject, ICommonAbilitys
    {
        private Image pic = AllGameImages.enemyTank;
        private Rectangle rect = new Rectangle(770, 0, 50, 50);
        public BrickView()
        {
            
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            throw new NotImplementedException();
        }

        public void DrawYourSelf(Graphics g)
        {
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rect, GraphicsUnit.Pixel);
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}
