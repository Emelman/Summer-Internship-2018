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
    public class PacmanController:IController,IMoving,IEnemyController,IData,IBulletMoving
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
            view.StartShoot += ModelCreateHeroBullet;
            var model = GetHeroModel();
            model.OnDirectionChanged += view.ReadDirectionFromModel;
            model.OnPositionChanged += view.ReadPositionFromModel;
            view.ReadPositionFromModel(new DataTransfer(model.GetId, model.Direction, model.Position));
        }

        private void ModelCreateHeroBullet(KolobokView sender)
        {
            var model = GetHeroModel();
            var pt = model.Position;
            pt = SetBulletPosition(model.Direction, pt);
            var modelBullet = AddBullet(pt, model.Direction, false);
            var bullet = new BulletView();
            bullet.isEnemyBullet = modelBullet.isEnemyBullet;
            bullet.Position = modelBullet.Position;
            bullet.Direction = modelBullet.Direction;
            bullet.Id = modelBullet.GetId;
            sender.SpawnBullet?.Invoke(bullet);
            InitBulletsViewEvents(bullet);
        }
        private void ModelChangeDirection(int sender)
        {
            var model = GetHeroModel();
            model.DirectionChanged(sender);
        }

        private void ModelCHangePosition(GameObject sender, EventArgs e)
        {
            var hModel = GetHeroModel();
            Point pt;
            switch (hModel.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    pt = MoveUp(hModel);
                    if(WallDetector(hModel, pt))
                    {
                        PositionChanged(hModel, pt);
                    }
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    pt = MoveDown(hModel);
                    if (WallDetector(hModel, pt))
                    {
                        PositionChanged(hModel, pt);
                    }
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    pt = MoveLeft(hModel);
                    if (WallDetector(hModel, pt))
                    {
                        PositionChanged(hModel, pt);
                    }
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    pt = MoveRight(hModel);
                    if (WallDetector(hModel, pt))
                    {
                        PositionChanged(hModel, pt);
                    }
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        private Boolean WallDetector(CoreModel hModel,Point pt)
        {
            var walls = GetBricksModels();
            for (var i = 0; i < walls.Count; i++)
            {
                if (CollisionTests.CheckGameObjectsWallCollision(pt, hModel.Square, walls[i].Position, walls[i].Square))
                {
                    return false;
                }
            }
            return true;
        }

        public void DirectionChanged(CoreModel model, int direction)
        {
            model.Direction = direction;
        }

        public void PositionChanged(CoreModel model, Point pt)
        {
            model.Position = pt;
        }

        public void InitEnemyViewEvents(TankView view)
        {
            view.OnMoving += EnemyTankChangedPosition;
            view.PickDirection += GameObjectChangedDirection;
            view.StartShoot += GameObjectCreateBullet;
            var model = GetEnemyById(view.Id);
            model.OnDirectionChanged += view.ReadDirectionFromModel;
            model.OnPositionChanged += view.ReadPositionFromModel;
            view.ReadPositionFromModel(new DataTransfer(model.GetId, model.Direction, model.Position));
        }

        public void InitBulletsViewEvents(BulletView view)
        {
            view.OnMoving += EnemyBulletChangedPosition;
            var model = GetBulletById(view.Id);
            model.OnDirectionChanged += view.ReadDirectionFromModel;
            model.OnPositionChanged += view.ReadPositionFromModel;
            var data = new DataTransfer(model.GetId, model.Direction, model.Position);
            view.ReadPositionFromModel(data);
            view.ReadDirectionFromModel(data);
        }

        private void EnemyBulletChangedPosition(BulletView sender, EventArgs e)
        {
            var model = globalModel.GetBulletById(sender.Id);
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    model.PositionChanged(MoveBulletUp(model));
                    sender.ToDelete = model.ToDelete;
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    model.PositionChanged(MoveBulletDown(model));
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    model.PositionChanged(MoveBulletLeft(model));
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    model.PositionChanged(MoveBulletRight(model));
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        private void GameObjectCreateBullet(GameObject sender)
        {
            var model = GetEnemyById(sender.Id);
            var pt = model.Position;
            pt = SetBulletPosition(model.Direction, pt);
            var modelBullet = AddBullet(pt, model.Direction, true);
            var bullet = new BulletView();
            bullet.isEnemyBullet = modelBullet.isEnemyBullet;
            bullet.Position = modelBullet.Position;
            bullet.Direction = modelBullet.Direction;
            bullet.Id = modelBullet.GetId;
            if (sender is TankView)
            {
                (sender as TankView).SpawnBullet?.Invoke(bullet);
            }
            InitBulletsViewEvents(bullet);
        }

        private void GameObjectChangedDirection(DataTransfer e)
        {
            var model = globalModel.GetEnemyById(e.Id);
            model.DirectionChanged(e.Direction);
        }
        private void EnemyTankChangedPosition(GameObject sender, EventArgs e)
        {
            var model = globalModel.GetEnemyById(sender.Id);
            Point pt;
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    pt = MoveUp(model);
                    if (WallDetector(model, pt))
                    {
                        if (EnemyHitEnemyDetector(model, pt))
                        {
                            PositionChanged(model, pt);
                        }
                    }
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    pt = MoveDown(model);
                    if (WallDetector(model, pt))
                    {
                        if (EnemyHitEnemyDetector(model, pt))
                        {
                            PositionChanged(model, pt);
                        }
                    }
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    pt = MoveLeft(model);
                    if (WallDetector(model, pt))
                    {
                        if (EnemyHitEnemyDetector(model, pt))
                        {
                            PositionChanged(model, pt);
                        }
                    }
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    pt = MoveRight(model);
                    if (WallDetector(model, pt))
                    {
                        if (EnemyHitEnemyDetector(model, pt))
                        {
                            PositionChanged(model, pt);
                        }
                    }
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        private Boolean EnemyHitEnemyDetector(CoreModel model, Point pt)
        {
            var enemys = GetEnemyModels();
            for (var i = 0; i < enemys.Count; i++)
            {
                if (CollisionTests.CheckGameObjectsWallCollision(pt, model.Square, enemys[i].Position, enemys[i].Square))
                {
                    if(enemys[i].GetId != model.GetId)
                    {
                        model.Direction = ServiceLib.SwitchOppositeDirection(model);
                        enemys[i].Direction = ServiceLib.SwitchOppositeDirection(enemys[i]); ///!!!! model1 !?!?!?
                        return false;
                    }
                }
            }
            return true;
        }
        private Point SetBulletPosition(int direction, Point pt)
        {
            switch (direction)
            {
                case (int)EnumDirections.Direction.UP:
                    pt.X += 16;
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    pt.X += 16;
                    pt.Y += 45;
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    pt.Y += 16;
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    pt.X += 45;
                    pt.Y += 16;
                    break;
                default:
                    throw (new ArgumentException("there is no such direction!"));
            }
            return pt;
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

        public BulletModel GetBulletById(int id)
        {
            return globalModel.GetBulletById(id);
        }

        public void AddEnemy(Point position)
        {
            globalModel.AddEnemy(position);
        }

        public BulletModel AddBullet(Point position, int direction, Boolean isEnemyBullet)
        {
            return globalModel.AddBullet(position, direction, isEnemyBullet);
        }

        public void AddApple(Point position)
        {
            globalModel.AddApple(position);
        }

        public void AddBrick(Point position)
        {
            globalModel.AddBrick(position);
        }

        public void DeleteBullet(int id)
        {
            globalModel.DeleteBullet(id);
        }

        public void DeleteEnemy(int id)
        {
            globalModel.DeleteEnemy(id);
        }

        public void DeleteBrick(int id)
        {
            globalModel.DeleteBrick(id);
        }

        public void DeleteApple(int id)
        {
            globalModel.DeleteApple(id);
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

        public Point MoveBulletUp(CoreModel model)
        {
            var pt = model.Position;
            pt.Y -= model.Speed;
            if (pt.Y < 0)
            {
                model.ToDelete = true;
            }
            return pt;
        }

        public Point MoveBulletDown(CoreModel model)
        {
            var pt = model.Position;
            pt.Y += model.Speed;
            if (pt.Y > 600)
            {
                model.ToDelete = true;
            }
            return pt;
        }

        public Point MoveBulletLeft(CoreModel model)
        {
            var pt = model.Position;
            pt.X -= model.Speed;
            if (pt.X < 0)
            {
                model.ToDelete = true;
            }
            return pt;
        }

        public Point MoveBulletRight(CoreModel model)
        {
            var pt = model.Position;
            pt.X += model.Speed;
            if (pt.X > 600)
            {
                model.ToDelete = true;
            }
            return pt;
        }
    }
}
