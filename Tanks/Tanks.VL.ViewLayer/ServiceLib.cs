using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer
{
    class ServiceLib
    {
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public static DialogResult WarningMessage(string sentence1, string sentence2, MessageBoxButtons specificBox = MessageBoxButtons.YesNo)
        {
            var res = MessageBox.Show(
                sentence1,
                sentence2,
                specificBox,
                MessageBoxIcon.Warning);
            return res;
        }
        public static int SwitchOppositeDirection(CoreModel model)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    return (int)EnumDirections.Direction.DOWN;
                case (int)EnumDirections.Direction.DOWN:
                    return (int)EnumDirections.Direction.UP;
                case (int)EnumDirections.Direction.LEFT:
                    return (int)EnumDirections.Direction.RIGHT;
                case (int)EnumDirections.Direction.RIGHT:
                    return (int)EnumDirections.Direction.LEFT;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        public static int SwitchNextDirection(CoreModel model)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    return (int)EnumDirections.Direction.RIGHT;
                case (int)EnumDirections.Direction.DOWN:
                    return (int)EnumDirections.Direction.LEFT;
                case (int)EnumDirections.Direction.LEFT:
                    return (int)EnumDirections.Direction.UP;
                case (int)EnumDirections.Direction.RIGHT:
                    return (int)EnumDirections.Direction.DOWN;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }


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

        public static Boolean CheckWallCollision(CoreModel enemy, CoreModel wall)
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
    }
}
