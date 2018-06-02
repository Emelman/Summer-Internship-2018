using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer.controller
{
    class EnemyController: IEnemyController, IMoving
    {
        private Enemy_model model;
        private TankView view;
        public EnemyController(Enemy_model _model, TankView _view)
        {
            model = _model;
            view = _view;

            view.OnMoving += ModelCHangePosition;
            view.PickDirection += ModelChangeDirection;
            model.OnDirectionChanged += HandleViewDirection;
            model.OnPositionChanged += HandleViewPosition;
            HandleViewDirection(model, EventArgs.Empty);
            HandleViewPosition(model, EventArgs.Empty);
        }

        private void HandleViewDirection(object sender, EventArgs e)
        {
            view.Direction = model.Direction;
        }

        private void HandleViewPosition(object sender, EventArgs e)
        {
            view.Position = model.Position;
        }

        private void ModelChangeDirection(int sender)
        {
            model.SetDirection(sender);
        }
        private void ModelCHangePosition(TankView sender, EventArgs e)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    model.ChangePosition(MoveUp());
                    //tempPt = MoveUp();
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    model.ChangePosition(MoveDown());
                    //tempPt = MoveDown();
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    model.ChangePosition(MoveLeft());
                    //tempPt = MoveLeft();
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    model.ChangePosition(MoveRight());
                    //tempPt = MoveRight();
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        public Point MoveUp()
        {
            var pt = model.Position;
            pt.Y -= model.Speed;
            if (pt.Y < 0)
            {
                pt.Y = 0;
                view.ChooseDirection(EnumDirections.Direction.RIGHT);
            }
            return pt;
        }

        public Point MoveDown()
        {
            var pt = model.Position;
            pt.Y += model.Speed;
            if (pt.Y > 550)
            {
                pt.Y = 550;
                view.ChooseDirection(EnumDirections.Direction.LEFT);
            }
            return pt;
        }

        public Point MoveLeft()
        {
            var pt = model.Position;
            pt.X -= model.Speed;
            if (pt.X < 0)
            {
                pt.X = 0;
                view.ChooseDirection(EnumDirections.Direction.UP);
            }
            return pt;
        }

        public Point MoveRight()
        {
            var pt = model.Position;
            pt.X += model.Speed;
            if (pt.X > 550)
            {
                pt.X = 550;
                view.ChooseDirection(EnumDirections.Direction.DOWN);
            }
            return pt;
        }

        public Boolean CheckCollision(Enemy_model _model)
        {
            var rectHere = new Rectangle(model.Position, model.Square);
            var rectOutside = new Rectangle(_model.Position, _model.Square);
            if (rectHere.IntersectsWith(rectOutside))
            {
                SwitchDirection();
                return true;
            }
            return false;
        }

        public void SwitchDirection()
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    view.ChooseDirection(EnumDirections.Direction.DOWN);
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    view.ChooseDirection(EnumDirections.Direction.UP);
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    view.ChooseDirection(EnumDirections.Direction.RIGHT);
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    view.ChooseDirection(EnumDirections.Direction.LEFT);
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }

        public Enemy_model EnemyModelPosition()
        {
            return model;
        }
        internal void Update()
        {
            
        }

    }
}
