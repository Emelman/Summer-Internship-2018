using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;

namespace Tanks.VL.ViewLayer.Interfaces
{
    public interface IData
    {
        List<Enemy_model> GetEnemy_Models();
        Enemy_model GetEnemyById(int id);

        Kolobok_model GetHeroModel();
    }
}
