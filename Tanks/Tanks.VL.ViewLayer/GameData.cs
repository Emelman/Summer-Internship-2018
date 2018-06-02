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
        private List<Enemy_model> enemies;
        private List<BulletModel> bullets;
        private List<Brick_model> bricks;
        private List<Apple_model> apples;
        private Kolobok_model hero;
        private Random rnd = new Random();

        public GameData(int tankNum, int _app,int speed)
        {
            enemies = new List<Enemy_model>();
            apples = new List<Apple_model>();
            bricks = new List<Brick_model>();
            bullets = new List<BulletModel>();
            for (var i = 0; i < tankNum; i++)
            {
                Enemy_model enemy = new Enemy_model();
                enemy.Direction = ServiceLib.GetRandomNumber(0, 4);
                enemy.Speed = speed;
                enemy.Position = new Point(0 + i * 50, 0 + i*50);
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
                Apple_model apple = new Apple_model();
                apples.Add(apple);
            }

            for(var i=0; i < 20; i++)
            {
                Brick_model wall = new Brick_model();
                bricks.Add(wall);
            }

            Kolobok_model model = new Kolobok_model();
            model.Direction = (int)EnumDirections.Direction.UP;
            model.Speed = speed;
            model.Position = new Point(300, 450);
            hero = model;
        }
        //public List<TankView> GetEnemyViewModels()
        //{
        //    var viewEnemys = new List<TankView>();
        //    for(var i=0; i < enemies.Count; i++)
        //    {
        //        var enemy = enemies[i] ;
        //        TankView foe = new TankView();
        //        foe.Id = enemy.GetId;
        //        foe.Direction = enemy.Direction;
        //        foe.Position = enemy.Position;
        //        foe.Square = enemy.Square;
        //        viewEnemys.Add(foe);
        //    }
        //    return viewEnemys;
        //}

        public List<Enemy_model> GetEnemy_Models()
        {
            return enemies;
        }

        public void UpdateEnemysLogik()
        {

        }

        public Enemy_model GetEnemyById(int id)
        {
            var foe = enemies.Find(item => item.GetId == id);
            return foe;
        }

        public Kolobok_model GetHeroModel()
        {
            return hero;
        }
    }
}
