using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.view_models
{
    public class CommonViewModel
    {
        public int GetId { get; set; }
        public Point Position { get; set; }
        public int Direction { get; set; }
        public string ObjectName { get; set; }

        public static CommonViewModel ToModel(CoreModel model)
        {
            var commonM = new CommonViewModel();
            commonM.GetId = model.GetId;
            commonM.Position = model.Position;
            commonM.Direction = model.Direction;
            commonM.ObjectName = model.ObjectName;
            return commonM;
        }
    }
}
