using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 游戏管理运行的核心部位，向上游线程框架传递自己的结果，自下游调用各物体的行为。
    /// </summary>
    class GameManage
    {
        public static bool isGameBegins;
        //创建随机生成星星或飞机的随机种子
        private static Random ranObject = new Random();

        //创建生成星星的集合
        private static List<Star> stars = new List<Star>();
        //创建玩家飞机的集合
        private static List<MyPlane> myPlanes = new List<MyPlane>();
        //创建玩家飞机的子弹集合
        public static List<Bullet> playerBullets = new List<Bullet>();

        //创建敌人飞机的子弹集合
        public static List<Bullet> enemyBullets = new List<Bullet>();

        //创建敌人飞机的集合
        public static List<EnemyPlane> enPlanes = new List<EnemyPlane>();

        //创建特效的集合
        public static List<Explosive> explosives = new List<Explosive>();

        //控制飞机生成的速度
        private static int generationSpeed;
        /// <summary>
        /// 管理所有游戏物体更新的方法集合
        /// </summary>
        public static void Update()
        {
            GameBackgroundUpdate();
            MyPlaneUpdate();
            EnPlaneUpdate();
            PlayerBulletUpdate();
            EnemyBulletUpdate();
            ExplosivesUpdate();
        }


        /// <summary>
        /// 生成以及破坏玩家的游戏子弹
        /// </summary>
        private static void PlayerBulletUpdate()
        {
            //销毁的操作在for循环中不会出现错误，在foreach中会出现线程冲突的错误
            for (int index = 0; index < playerBullets.Count; index++)
            {
                if (!playerBullets[index].isDestroy)
                {
                    playerBullets[index].GameUpdate();
                }
                else
                {
                    playerBullets.Remove(playerBullets[index]);
                }
            }
        }
        /// <summary>
        /// 生成以及破坏敌人的游戏子弹
        /// </summary>
        private static void EnemyBulletUpdate()
        {
            for (int index = 0; index < enemyBullets.Count; index++)
            {
                if (!enemyBullets[index].isDestroy)
                {
                    enemyBullets[index].GameUpdate();
                }
                else
                {
                    enemyBullets.Remove(enemyBullets[index]);
                }
            }
        }
        /// <summary>
        /// 生成爆炸特效
        /// </summary>
        private static void ExplosivesUpdate()
        {
            for (int index = 0; index < explosives.Count; index++)
            {
                if (!explosives[index].isDestroy)
                {
                    explosives[index].GameUpdate();
                }
                else
                {
                    explosives.Remove(explosives[index]);
                }
            }
        }
        /// <summary>
        /// 建立游戏背景的星星对象
        /// </summary>
        private static void GameBackgroundUpdate()
        {
            foreach (Star star in stars)
            {
                star.GameUpdate();
            }
        }
        /// <summary>
        /// 生成以及摧毁玩家的飞机
        /// </summary>
        private static void MyPlaneUpdate()
        {
            foreach (MyPlane myPlane in myPlanes)
            {
                if (!myPlane.isDestroy)
                {
                    myPlane.GameUpdate();
                }
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
        /// 生成以及销毁敌人的飞机
        /// </summary>
        private static void EnPlaneUpdate()
        {
            generationSpeed++;
            if (generationSpeed >= 60)
            {
                CreatEnPlane();
                generationSpeed = 0;
            }
            for (int index = 0; index < enPlanes.Count; index++)
            {
                if (!enPlanes[index].isDestroy)
                {
                    enPlanes[index].GameUpdate();
                }
                else
                {
                    enPlanes.Remove(enPlanes[index]);
                }
            }
        }
        /// <summary>
        /// 创建敌人飞机的对象
        /// </summary>
        private static void CreatEnPlane()
        {
            int ranX = ranObject.Next(0, 451);
            int index = ranObject.Next(0, 3);
            switch (index)
            {
                case 0:
                    EnemyPlane plane1 = new EnemyPlane(Resources.EnemyPlane1, ranX,0,2,1,4,Enemytype.Enemy1);
                    enPlanes.Add(plane1);
                    break;
                case 1:
                    EnemyPlane plane2 = new EnemyPlane(Resources.EnemyPlane2, ranX, 0, 4,2,3,Enemytype.Enemy2);
                    enPlanes.Add(plane2);
                    break;
                case 2:
                    EnemyPlane plane3 = new EnemyPlane(Resources.EnemyPlane3, ranX, 0, 6,2,2,Enemytype.Enemy3);
                    enPlanes.Add(plane3);
                    break;

            }
           
        }
        /// <summary>
        /// 判断两个物体是否发生碰撞
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public static Movthing IsCollideEnemy(Rectangle rt)
        {
            foreach (Movthing enPlane in enPlanes)
            {
                if (enPlane.GetRectangle(enPlane.X, enPlane.Y, enPlane.Width, enPlane.Height).IntersectsWith(rt))
                {
                    return enPlane;
                }
            }
            return null;
        }
        /// <summary>
        /// 判断两个物体是否发生碰撞，仅和玩家的飞机碰撞
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public static Movthing IsCollidePlayer(Rectangle rt)
        {
            foreach (Movthing myPlane in myPlanes)
            {
                if (myPlane.GetRectangle(myPlane.X, myPlane.Y, myPlane.Width, myPlane.Height).IntersectsWith(rt))
                {
                    return myPlane;
                }
            }
            return null;
        }
        /// <summary>
        /// 向集合中增加游戏背景的星星对象
        /// </summary>
        public static void DrawStars()
        {
            for (int count = 0; count < 12; count++)
            {
                //当前窗口大小为450,450，不能超出这个范围
                int ranX= ranObject.Next(0, 451);
                int ranY= ranObject.Next(0, 451);
                int index = ranObject.Next(1, 4);
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
