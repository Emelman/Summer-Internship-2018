using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.game_objects;
using Tanks.VL.ViewLayer.Interfaces;

namespace Tanks.VL.ViewLayer
{
    public class GameData:IData
    {
        private List<EnemyModel> enemies;
        private List<BulletModel> bullets;
        private List<BrickModel> bricks;
        private List<AppleModel> apples;
        private KolobokModel hero;
        private Random rnd = new Random();

        public GameData(int tankNum, int _app,int speed)
        {
            enemies = new List<EnemyModel>();
            apples = new List<AppleModel>();
            bricks = new List<BrickModel>();
            bullets = new List<BulletModel>();
            for (var i = 0; i < tankNum; i++)
            {
                EnemyModel enemy = new EnemyModel();
                enemy.Direction = ServiceLib.GetRandomNumber(0, 4);
                enemy.Speed = speed;
                enemy.Position = new Point(0 + i * 70, 0);
                enemy.Square = new Size(45, 45);
                var maxId = 0;
                var ids = enemies.Select(u => u.GetId);
                if (ids.Count() != 0)
                {
                    maxId = ids.Max();
                }
                enemy.GetId = maxId + 1;
                enemies.Add(enemy);
            }
            for(var i=0; i < _app; i++)
            {
                AppleModel apple = new AppleModel();
                apples.Add(apple);
            }

            int count = 0;
            int raw = 1;
            for(var i=0; i < 30; i++)
            {
                BrickModel wall = new BrickModel();
                wall.Position = new Point(80 + 50 * count, 60 * raw);
                wall.Square = new Size(50, 50);
                var maxId = 0;
                var ids = bricks.Select(u => u.GetId);
                if (ids.Count() != 0)
                {
                    maxId = ids.Max();
                }
                wall.GetId = maxId + 1;
                bricks.Add(wall);
                if (count == 8)
                {
                    count = 0;
                    raw += 3;
                }
                else count++;
            }

            KolobokModel model = new KolobokModel();
            model.Direction = (int)EnumDirections.Direction.UP;
            model.Speed = speed;
            model.Position = new Point(300, 450);
            model.Square = new Size(45, 45);
            hero = model;
        }

        public List<EnemyModel> GetEnemyModels()
        {
            return enemies;
        }
        public List<BrickModel> GetBricksModels()
        {
            return bricks;
        }
        public List<AppleModel> GetApplesModels()
        {
            return apples;
        }
        public EnemyModel GetEnemyById(int id)
        {
            var foe = enemies.Find(item => item.GetId == id);
            return foe;
        }

        public KolobokModel GetHeroModel()
        {
            return hero;
        }

        public void UpdateEnemysLogik()
        {

        }

        public List<BulletModel> GetBulletsModels()
        {
            return bullets;
        }

        public BrickModel GetBrickById(int id)
        {
            var brick = bricks.Find(item => item.GetId == id);
            return brick;
        }
    }
}
