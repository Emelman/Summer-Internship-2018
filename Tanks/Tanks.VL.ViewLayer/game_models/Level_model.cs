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
        private List<BrickView> bricks;
        private Kolobok_model hero;
        private Random rnd = new Random();

        public Level_model(int tankNum, int apples,int speed)
        {
            Enemies = new List<Enemy_model>();
            for (var i = 0; i < tankNum; i++)
            {
                Enemy_model enemy = new Enemy_model();
                enemy.Direction = ServiceLib.GetRandomNumber(0, 4);
                enemy.Speed = speed;
                enemy.Position = new Point(0 + i * 50, 0 + i*50);
                enemy.Square = new Size(45, 45);
                var maxId = 0;
                var ids = Enemies.Select(u => u.Id);
                if (ids.Count() != 0)
                {
                    maxId = ids.Max();
                }
                enemy.Id = maxId + 1;
                Enemies.Add(enemy);
            }
            for(var i=0; i < apples; i++)
            {

            }

            for(var i=0; i < 20; i++)
            {
                Brick_model wall = new Brick_model();
                
            }

            Kolobok_model model = new Kolobok_model();
            model.Direction = (int)EnumDirections.Direction.UP;
            model.Speed = speed;
            model.Position = new Point(300, 450);
            Hero = model;
        }

        public Kolobok_model Hero { get => hero; private set => hero = value; }
        internal List<Enemy_model> Enemies { get => enemies; set => enemies = value; }

        public List<TankView> GetEnemyViewModels()
        {
            var viewEnemys = new List<TankView>();
            for(var i=0; i < Enemies.Count; i++)
            {
                TankView foe = new TankView();
                foe.Id = Enemies[i].Id;
                foe.Direction = Enemies[i].Direction;
                foe.Position = Enemies[i].Position;
                foe.Square = Enemies[i].Square;
                viewEnemys.Add(foe);
            }
            return viewEnemys;
        }

        public List<Enemy_model> GetEnemy_Models()
        {
            return enemies;
        }

        public Enemy_model GetEnemy(int id)
        {
            var foe = enemies.Find(item => item.Id == id);
            return foe;
        }

        public void UpdateEnemysLogik()
        {

        }

        

    }
}
