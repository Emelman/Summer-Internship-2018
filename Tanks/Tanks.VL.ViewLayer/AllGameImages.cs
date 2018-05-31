using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.VL.ViewLayer
{
    public class AllGameImages
    {
        public static Image heroTank = Image.FromFile("../../pics/hero.png");
        public static Image enemyTank = Image.FromFile("../../pics/enemy.png");
        public static Image brickWall = Image.FromFile("../../pics/brick_wall.png");
        public static Image bulletPic = Image.FromFile("../../pics/bullet.png");
        public static Image all_sprites = Image.FromFile("../../pics/all_stuff.png");
    }
}
