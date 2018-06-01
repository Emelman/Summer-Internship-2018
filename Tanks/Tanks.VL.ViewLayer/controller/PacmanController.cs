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

namespace Tanks.VL.ViewLayer.controller
{
    public class PacmanController
    {
        private Kolobok_model model;
        private KolobokView view;

        public PacmanController(Kolobok_model _model, KolobokView _view)
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

        private void ModelChangeDirection(int sender)
        {
            model.SetDirection(sender);
        }

        private void ModelCHangePosition(KolobokView sender, EventArgs e)
        {
            switch (model.Direction)
            {
                case (int)EnumDirections.Direction.UP:
                    model.MoveUp();
                    break;
                case (int)EnumDirections.Direction.DOWN:
                    model.MoveDown();
                    break;
                case (int)EnumDirections.Direction.LEFT:
                    model.MoveLeft();
                    break;
                case (int)EnumDirections.Direction.RIGHT:
                    model.MoveRight();
                    break;
                default:
                    throw (new ArgumentException("No such direction!"));
            }
        }
        private void HandleViewPosition(object sender, EventArgs e)
        {
            view.Position = model.Position;
        }

        private void HandleViewDirection(object sender, EventArgs e)
        {
            view.Direction = model.Direction;
        }

        public void Update()
        {
            view.UpdateLogick();
        }
    }
}
