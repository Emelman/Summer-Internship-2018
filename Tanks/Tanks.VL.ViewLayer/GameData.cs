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
        private int speed;

        public GameData(int tankNum, int _app,int _speed)
        {
            speed = _speed;
            enemies = new List<EnemyModel>();
            apples = new List<AppleModel>();
            bricks = new List<BrickModel>();
            bullets = new List<BulletModel>();
            for (var i = 0; i < tankNum; i++)
            {
                AddEnemy(new Point(0 + i * 70, 0));
            }
            for(var i=0; i < _app; i++)
            {
                AddApple(new Point(0 + i * 60, i * 50));
            }

            int count = 0;
            int raw = 1;
            for(var i=0; i < 30; i++)
            {
                AddBrick(new Point(80 + 50 * count, 60 * raw));
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
            model.Position = new Point(300, 550);
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

        public KolobokModel GetHeroModel()
        {
            return hero;
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

        public BulletModel GetBulletById(int id)
        {
            var bullet = bullets.Find(item => item.GetId == id);
            return bullet;
        }
        public EnemyModel GetEnemyById(int id)
        {
            var foe = enemies.Find(item => item.GetId == id);
            return foe;
        }
        public void AddEnemy(Point position)
        {
            EnemyModel enemy = new EnemyModel();
            enemy.Direction = ServiceLib.GetRandomNumber(0, 4);
            enemy.Speed = speed;
            enemy.Position = position;
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

        public BulletModel AddBullet(Point position, int direction, Boolean isEnemyBullet)
        {
            BulletModel bullet = new BulletModel();
            bullet.Direction = direction;
            bullet.Position = position;
            bullet.Square = new Size(15, 15);
            bullet.Speed = speed;
            bullet.isEnemyBullet = isEnemyBullet;
            var maxId = 0;
            var ids = bullets.Select(u => u.GetId);
            if (ids.Count() != 0)
            {
                maxId = ids.Max();
            }
            bullet.GetId = maxId + 1;
            bullets.Add(bullet);
            return bullet;
        }

        public void AddApple(Point position)
        {
            AppleModel apple = new AppleModel();
            apple.Position = position;
            apple.Square = new Size(40, 40);
            var maxId = 0;
            var ids = apples.Select(u => u.GetId);
            if (ids.Count() != 0)
            {
                maxId = ids.Max();
            }
            apple.GetId = maxId + 1;
            apples.Add(apple);
        }

        public void AddBrick(Point position)
        {
            BrickModel wall = new BrickModel();
            wall.Position = position;
            wall.Square = new Size(50, 50);
            var maxId = 0;
            var ids = bricks.Select(u => u.GetId);
            if (ids.Count() != 0)
            {
                maxId = ids.Max();
            }
            wall.GetId = maxId + 1;
            bricks.Add(wall);
        }

        public void DeleteBullet(int id)
        {
            var bullet = bullets.Find(item => item.GetId == id);
            bullets.Remove(bullet);
        }

        public void DeleteEnemy(int id)
        {
            var foe = enemies.Find(item => item.GetId == id);
            enemies.Remove(foe);
        }

        public void DeleteBrick(int id)
        {
            var brick = bricks.Find(item => item.GetId == id);
            bricks.Remove(brick);
        }

        public void DeleteApple(int id)
        {
            var apple = apples.Find(item => item.GetId == id);
            apples.Remove(apple);
        }
    }
}
