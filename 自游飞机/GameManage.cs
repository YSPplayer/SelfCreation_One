using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 游戏管理运行的核心部位，向上游线程框架传递自己的结果，自下游调用各物体的行为。
    /// </summary>
    class GameManage
    {
        public static bool isGameBegins;
        //创建随机生成星星的随机种子
        private static Random ranStar = new Random();

        //创建生成星星的集合
        private static List<Star> stars = new List<Star>();
        //创建玩家飞机的集合
        private static List<MyPlane> myPlanes = new List<MyPlane>();
        //创建玩家飞机的子弹集合
        public static List<Bullet> playerBullets = new List<Bullet>();

        /// <summary>
        /// 生成玩家的游戏子弹
        /// </summary>
        public static void playerBulletUpdate()
        {
            foreach (Bullet playerBullet in playerBullets)
            {
                playerBullet.GameUpdate();
            }
        }
        /// <summary>
        /// 建立游戏背景的星星对象
        /// </summary>
        public static void GameBackgroundUpdate()
        {
            foreach (Star star in stars)
            {
                star.GameUpdate();
            }
        }
        /// <summary>
        /// 生成玩家的飞机
        /// </summary>
        public static void MyPlaneUpdate()
        {
            foreach (MyPlane myPlane in myPlanes)
            {
                myPlane.GameUpdate();
            }
        }
        /// <summary>
        /// 创建自己的飞机对象
        /// </summary>
        public static void CreatMyPlane()
        {
            MyPlane plane = new MyPlane(Resources.MyPlane, 200, 450);
            myPlanes.Add(plane);
        }
        /// <summary>
        /// 向集合中增加游戏背景的星星对象
        /// </summary>
        public static void DrawStars()
        {
            for (int count = 0; count < 12; count++)
            {
                //当前窗口大小为450,450，不能超出这个范围
                int ranX=ranStar.Next(0, 451);
                int ranY=ranStar.Next(0, 451);
                int index = ranStar.Next(1, 4);
                stars.Add(CreatRanStars(ranX, ranY, index));
            }
        }
        /// <summary>
        /// 绘制游戏背景的随机星星对象
        /// </summary>
        private static Star CreatRanStars(int x,int y,int index)
        {
            switch (index)
            {
                case 1:
                    Star star1 = new Star(Resources.Star1, x, y,4);
                    return star1;
                case 2:
                    Star star2 = new Star(Resources.Star2, x, y, 4);
                    return star2;
                case 3:
                    Star star3 = new Star(Resources.Star3, x, y, 4);
                    return star3;
                default:
                    Star star4 = new Star(Resources.Star1, x, y, 4);
                    return star4;
            }
        }
    }
}
