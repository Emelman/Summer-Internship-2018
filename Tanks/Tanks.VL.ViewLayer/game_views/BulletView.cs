using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.game_objects
{
    public class BulletView : GameObject, IViewObjectsCommon
    {
        public delegate void StartMoving(BulletView sender, EventArgs e);
        public event StartMoving OnMoving;
        public delegate void SetDirection(DataTransfer e);
        public event SetDirection PickDirection;
        public Boolean isEnemyBullet;
        private Rectangle[] rects = { new Rectangle(967, 304, 13, 16),
            new Rectangle(1015, 304, 13, 16),
            new Rectangle(988, 304, 16, 13),
            new Rectangle(1036, 304, 16, 13) };

        public BulletView()
        {
        }

        public void Update()
        {
            OnMoving(this, EventArgs.Empty);
        }

        public void ChooseDirection(EnumDirections.Direction dir)
        {
            maxCD = ServiceLib.GetRandomNumber(1, 10) * 4;
            PickDirection(new DataTransfer(Id, (int)dir));
        }

        public void DrawYourSelf(Graphics g)
        {
            g.DrawImage(AllGameImages.all_sprites, Position.X, Position.Y, rects[Direction] , GraphicsUnit.Pixel);
        }

        public void ReadDirectionFromModel(DataTransfer e)
        {
            Direction = e.Direction;
        }

        public void ReadPositionFromModel(DataTransfer e)
        {
            Position = e.Position;
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void SpawnBulletView(BulletView e)
        {
            throw new NotImplementedException();
        }
    }
}
