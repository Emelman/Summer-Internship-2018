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

        public static Boolean CheckGameObjectsWallCollision(Point position1, Size square1, Point position2, Size square2)
        {
            var rectM1 = new Rectangle(position1, square1);
            var rectM2 = new Rectangle(position2, square2);
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

        /*
        public static Boolean GetWallSideCollision()
        {
            if (heroToDraw.Position.X >= wallToDraw[i].Position.X)
            {
                if (heroToDraw.Position.Y >= wallToDraw[i].Position.Y)
                {
                    if (wallToDraw[i].Position.X + wallToDraw[i].Square.Width - heroToDraw.Position.X >=
                        wallToDraw[i].Position.Y + wallToDraw[i].Square.Height - heroToDraw.Position.Y)
                    {
                        heroToDraw.Position.Y += speed;
                    }
                    else
                    {
                        heroToDraw.Position.X += speed;
                    }
                }
                else
                {
                    if (wallToDraw[i].Position.X + wallToDraw[i].Square.Width - heroToDraw.Position.X >=
                        wallToDraw[i].Position.Y + wallToDraw[i].Square.Height - (heroToDraw.Position.Y + heroToDraw.Square.Height))
                    {
                        heroToDraw.Position.Y -= speed;
                    }
                    else
                    {
                        heroToDraw.Position.X -= speed;
                    }
                }
            }
            else
            {
                if (heroToDraw.Position.Y >= wallToDraw[i].Position.Y)
                {
                    if (heroToDraw.Position.X + heroToDraw.Square.Width - wallToDraw[i].Position.X >=
                        wallToDraw[i].Position.Y + wallToDraw[i].Square.Height - heroToDraw.Position.Y)
                    {
                        heroToDraw.Position.Y += speed;
                    }
                    else
                    {
                        heroToDraw.Position.X -= speed;
                    }
                }
                else
                {
                    if (heroToDraw.Position.X + heroToDraw.Square.Width - wallToDraw[i].Position.X >=
                        heroToDraw.Position.Y + heroToDraw.Square.Height - wallToDraw[i].Position.Y)
                    {
                        heroToDraw.Position.Y -= speed;
                    }
                    else
                    {
                        heroToDraw.Position.X -= speed;
                    }
                }
            }
        }*/

    }
}
