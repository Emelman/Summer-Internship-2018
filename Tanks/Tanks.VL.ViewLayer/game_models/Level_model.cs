using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.game_objects;

namespace Tanks.VL.ViewLayer.game_models
{
    class Level_model
    {
        private List<Enemy_model> enemies;
        private List<Bullet> allBullets;
        private List<Brick> bricks;
        private Kolobok_model hero;
        private Random rnd = new Random();

        public Level_model(int tankNum, int apples,int speed)
        {
            enemies = new List<Enemy_model>();
            for (var i = 0; i < tankNum; i++)
            {
                Enemy_model enemy = new Enemy_model();
                enemy.Direction = rnd.Next() * 4;
                enemy.Speed = speed;
                enemy.Position = new Point(0,0);
                var maxId = 0;
                var ids = enemies.Select(u => u.Id);
                if (ids.Count() != 0)
                {
                    maxId = ids.Max();
                }
                enemy.Id = maxId + 1;
                enemies.Add(enemy);
            }
            for(var i=0; i < apples; i++)
            {

            }

            Kolobok_model model = new Kolobok_model();
            model.Direction = (int)EnumDirections.Direction.UP;
            model.Speed = speed;
            model.Position = new Point(300, 450);
            Hero = model;
        }

        public Kolobok_model Hero { get => hero; private set => hero = value; }

        public List<Enemy> GetEnemyViewModels(EnemyController ctrl)
        {
            var viewEnemys = new List<Enemy>();
            for(var i=0; i < enemies.Count; i++)
            {
                Enemy foe = new Enemy(ctrl);
                foe.Id = enemies[i].Id;
                foe.Direction = enemies[i].Direction;
                foe.Position = enemies[i].Position;
                viewEnemys.Add(foe);
            }
            return viewEnemys;
        }

        public void UpdateEnemysLogik()
        {

        }


    }
}
