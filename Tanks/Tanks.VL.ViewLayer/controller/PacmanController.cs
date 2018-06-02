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
            var model = globalModel.GetHeroModel();
            model.OnDirectionChanged += view.ReadDirectionFromModel;
            model.OnPositionChanged += view.ReadPositionFromModel;
            view.ReadPositionFromModel(new DataTransfer(model.GetId, model.Direction, model.Position));
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


        public Point MoveUp(CoreModel model)
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

        public Point MoveDown(CoreModel model)
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

        public Point MoveLeft(CoreModel model)
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

        public Point MoveRight(CoreModel model)
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

        public void DirectionChanged(CoreModel model, int direction)
        {
            model.Direction = direction;
        }

        public void PositionChanged(Point pt)
        {
            throw new NotImplementedException();
        }

        public List<EnemyModel> GetEnemyModels()
        {
            return globalModel.GetEnemyModels();
        }

        public EnemyModel GetEnemyById(int id)
        {
            return globalModel.GetEnemyById(id);
        }

        public KolobokModel GetHeroModel()
        {
            return globalModel.GetHeroModel();
        }



        public void InitEnemyViewEvents(TankView view)
        {
            view.OnMoving += GameObjectChangedPosition;
            view.PickDirection += GameObjectChangedDirection;
            var model = GetEnemyById(view.Id);
            model.OnDirectionChanged += view.ReadDirectionFromModel;
            model.OnPositionChanged += view.ReadPositionFromModel;
            view.ReadPositionFromModel(new DataTransfer(model.GetId, model.Direction, model.Position));
        }

        private void GameObjectChangedDirection(DataTransfer e)
        {
            var model = globalModel.GetEnemyById(e.Id);
            model.SetDirection(e.Direction);
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

        public List<BrickModel> GetBricksModels()
        {
            return globalModel.GetBricksModels();
        }

        public List<AppleModel> GetApplesModels()
        {
            return globalModel.GetApplesModels();
        }

        public List<BulletModel> GetBulletsModels()
        {
            return globalModel.GetBulletsModels();
        }

        public BrickModel GetBrickById(int id)
        {
            return globalModel.GetBrickById(id);
        }
    }
}
