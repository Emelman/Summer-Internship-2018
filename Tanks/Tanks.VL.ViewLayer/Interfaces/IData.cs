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
        List<EnemyModel> GetEnemyModels();
        List<BrickModel> GetBricksModels();
        List<AppleModel> GetApplesModels();
        List<BulletModel> GetBulletsModels();
        EnemyModel GetEnemyById(int id);
        BrickModel GetBrickById(int id);
        KolobokModel GetHeroModel();

    }
}
