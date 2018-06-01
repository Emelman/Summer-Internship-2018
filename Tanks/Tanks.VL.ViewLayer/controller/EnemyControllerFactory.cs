using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.controller
{
    class EnemyControllerFactory : IEnemyControllerFactory
    {
        public EnemyControllerFactory(Enemy_model model, TankView view)
        {
            Controller = new EnemyController(model, view);
        }

        public EnemyControllerFactory():this(new Enemy_model(), new TankView())
        {
        }


        public IEnemyController Controller { get; private set; }


    }
}
