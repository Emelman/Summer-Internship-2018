using System;
using System.Collections.Generic;
using System.Drawing;
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
        AppleModel GetAppleById(int id);
        KolobokModel GetHeroModel();
        BulletModel GetBulletById(int id);

        void AddEnemy(Point position);
        BulletModel AddBullet(Point position, int direction, Boolean isEnemyBullet);
        void AddApple(Point position);
        void AddBrick(Point position, Boolean isWater);
        void DeleteBullet(int id);
        void DeleteEnemy(int id);
        void DeleteBrick(int id);
        void DeleteApple(int id);
        void UpdateGameScore();
        int GetScore();
    }
}
