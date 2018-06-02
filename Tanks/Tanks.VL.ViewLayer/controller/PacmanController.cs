using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.controller
{
    public class PacmanController:IController,IMoving,IEnemyController,IData
    {
        private GameData globalModel;

        public PacmanController(string[] args)
        {
            globalModel = new GameData(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));
        }

        public void InitHeroViewEvents(KolobokView view)
        {
            view.OnMoving += ModelCHangePosition;
            view.PickDirection += ModelChangeDirection;
        }

        private void ModelChangeDirection(int sender)
        {
            var model = globalModel.GetHeroModel();
            model.DirectionChanged(sender);
        }

        private void ModelCHangePosition(GameObject sender, EventArgs e)
        {
            var hModel = globalModel.GetHeroModel();
            switch (hModel.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    hModel.PositionChanged(MoveUp(hModel));
                    //tempPt = MoveUp();
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    hModel.PositionChanged(MoveDown(hModel));
                    //tempPt = MoveDown();
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    hModel.PositionChanged(MoveLeft(hModel));
                    //tempPt = MoveLeft();
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    hModel.PositionChanged(MoveRight(hModel));
                    //tempPt = MoveRight();
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }


        public Point MoveUp(Core_model model)
        {
            var pt = model.Position;
            pt.Y -= model.Speed;
            if (pt.Y < 0)
            {
                pt.Y = 0;
                DirectionChanged(model, (int)EnumDirections.Direction.RIGHT);
            }
            return pt;
        }

        public Point MoveDown(Core_model model)
        {
            var pt = model.Position;
            pt.Y += model.Speed;
            if (pt.Y > 550)
            {
                pt.Y = 550;
                DirectionChanged(model, (int)EnumDirections.Direction.LEFT);
            }
            return pt;
        }

        public Point MoveLeft(Core_model model)
        {
            var pt = model.Position;
            pt.X -= model.Speed;
            if (pt.X < 0)
            {
                pt.X = 0;
                DirectionChanged(model, (int)EnumDirections.Direction.UP);
            }
            return pt;
        }

        public Point MoveRight(Core_model model)
        {
            var pt = model.Position;
            pt.X += model.Speed;
            if (pt.X > 550)
            {
                pt.X = 550;
                DirectionChanged(model, (int)EnumDirections.Direction.DOWN);
            }
            return pt;
        }

        public void DirectionChanged(Core_model model, int direction)
        {
            model.Direction = direction;
        }

        public void PositionChanged(Point pt)
        {
            throw new NotImplementedException();
        }

        public List<Enemy_model> GetEnemy_Models()
        {
            return globalModel.GetEnemy_Models();
        }

        public Enemy_model GetEnemyById(int id)
        {
            return globalModel.GetEnemyById(id);
        }

        public Kolobok_model GetHeroModel()
        {
            return globalModel.GetHeroModel();
        }



        public void InitEnemyViewEvents(TankView view)
        {
            view.OnMoving += GameObjectChangedPosition;
            view.PickDirection += GameObjectChangedDirection;
        }

        private void GameObjectChangedDirection(DataTransfer e)
        {
            var model = globalModel.GetEnemyById(e.id);
            model.SetDirection(e.direction);
        }
        private void GameObjectChangedPosition(GameObject sender, EventArgs e)
        {
            var model = globalModel.GetEnemyById(sender.Id);
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    model.ChangePosition(MoveUp(model));
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    model.ChangePosition(MoveDown(model));
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    model.ChangePosition(MoveLeft(model));
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    model.ChangePosition(MoveRight(model));
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        public Boolean CheckCollision(Core_model model1, Core_model model2)
        {
            var rectM1 = new Rectangle(model1.Position, model1.Square);
            var rectM2 = new Rectangle(model2.Position, model2.Square);
            if (rectM1.IntersectsWith(rectM2))
            {
                DirectionChanged(model1, ServiceLib.SwitchDirection(model1));
                DirectionChanged(model2, ServiceLib.SwitchDirection(model2)); ///!!!! model1 !?!?!?
                return true;
            }
            return false;
        }
        
    }
}
