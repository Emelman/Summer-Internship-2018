using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer
{
    public class CollisionTests
    {
        public static Boolean CheckCollision(CoreModel model1, CoreModel model2)
        {
            var rectM1 = new Rectangle(model1.Position, model1.Square);
            var rectM2 = new Rectangle(model2.Position, model2.Square);
            if (rectM1.IntersectsWith(rectM2))
            {
                model1.Direction = ServiceLib.SwitchOppositeDirection(model1);
                model2.Direction = ServiceLib.SwitchOppositeDirection(model2); ///!!!! model1 !?!?!?
                return true;
            }
            return false;
        }

        public static Boolean CheckEnemyWallCollision(CoreModel enemy, CoreModel wall)
        {
            var rectM1 = new Rectangle(enemy.Position, enemy.Square);
            var rectM2 = new Rectangle(wall.Position, wall.Square);
            if (rectM1.IntersectsWith(rectM2))
            {
                enemy.Direction = ServiceLib.SwitchNextDirection(enemy);
                return true;
            }
            return false;
        }

        public static Boolean CheckHeroWallCollision(CoreModel hero, CoreModel wall)
        {
            var rectM1 = new Rectangle(hero.Position, hero.Square);
            var rectM2 = new Rectangle(wall.Position, wall.Square);
            if (rectM1.IntersectsWith(rectM2))
            {
                return true;
            }
            return false;
        }

        public static Boolean CheckBulletCollsion(BulletModel modelB, CoreModel model)
        {
            var rectM1 = new Rectangle(modelB.Position, modelB.Square);
            var rectM2 = new Rectangle(model.Position, model.Square);
            if (rectM1.IntersectsWith(rectM2))
            {
                return true;
            }
            return false;
        }

    }
}
