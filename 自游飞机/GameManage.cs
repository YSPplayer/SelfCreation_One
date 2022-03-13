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
        public static List<SkillPicture> skillPictures = new List<SkillPicture>();

        //创建生命值的集合
        public static List<NotMoving> lpfs = new List<NotMoving>();
        public static List<NotMoving> lps = new List<NotMoving>();

        //创建技能图片的集合
        public static List<NotMoving> skillPictures1 = new List<NotMoving>();
        public static List<NotMoving> skillPictures2= new List<NotMoving>();

        //控制飞机生成的速度
        private static int generationSpeed;
        
        //控制技能图片生成的速度
        private static int skillPictureSpeed=0;
        //控制技能图片生成的加速度
        private static int accelerationConounter = 0;

        //播放技能图片的时机
        public static bool isPlayGif;
        public static bool isPlaySkill1;
        public static bool isPlaySkill2;
        //播放动图的时机检查
        public static bool isStartGif;

        /// <summary>
        /// 管理所有游戏物体更新的方法集合
        /// </summary>
        public static void Update()
        {

               //最后绘制的图片的图层在最上面
                SkillPictureUpdate();
                GameBackgroundUpdate();
                MyPlaneUpdate();
                EnPlaneUpdate();
                PlayerBulletUpdate();
                EnemyBulletUpdate();
                ExplosivesUpdate();
                HealthUpdate();
                SkillPicture1Update();
                SkillPicture2Update();
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
        /// 生成以及销毁爆炸特效
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
        /// 生成以及销毁技能特效
        /// </summary>
        private static void SkillPictureUpdate()
        {
            if (isStartGif)
            {
                for (int index = 0; index < skillPictures.Count; index++)
                {
                    skillPictures[index].GameUpdate();
                }
            }
        }
        /// <summary>
        /// 生成技能图片特效
        /// </summary>
        private static void SkillPicture1Update()
        {
            
            if (MyPlane.isSkill1)
            {
                skillPictureSpeed++;
                isPlayGif = true;
                isPlaySkill1 = true;
                for (int index = 0; index < skillPictures1.Count; index++)
                {
                    skillPictures1[index].GameUpdate();
                    if (skillPictureSpeed > 1)
                    {
                        skillPictures1[index].X += 20- accelerationConounter;//15
                        skillPictureSpeed = 0;
                        if (accelerationConounter < 22)//17
                        {
                            accelerationConounter++;
                        }
                        else
                        {
                            accelerationConounter += 2;
                        }
                        //判断图片是否左移了，左移一段结束动画效果
                        if (20 - accelerationConounter < 0 && skillPictures1[index].X <= 42)
                        {
                            MyPlane.isSkill1 = false;
                            isStartGif = true;
                            accelerationConounter = 0;
                            return;
                        }
                    }
                }
            }
            else if (isPlayGif && isPlaySkill1)
            {
                for (int index = 0; index < skillPictures1.Count; index++)
                {
                    skillPictures1[index].GameUpdate();
                }
            }

        }
        /// <summary>
        /// 生成技能图片特效2
        /// </summary>
        private static void SkillPicture2Update()
        {

            if (MyPlane.isSkill2)
            {
                skillPictureSpeed++;
                isPlayGif = true;
                isPlaySkill2 = true;
                for (int index = 0; index < skillPictures2.Count; index++)
                {
                    skillPictures2[index].GameUpdate();
                    if (skillPictureSpeed > 1)
                    {
                        skillPictures2[index].X += 20 - accelerationConounter;
                        skillPictureSpeed = 0;
                        if (accelerationConounter < 22)
                        {
                            accelerationConounter++;
                        }
                        else
                        {
                            accelerationConounter += 2;
                        }
                        //判断图片是否左移了，左移一段结束动画效果
                        if (20 - accelerationConounter < 0 && skillPictures2[index].X <= 42)
                        {
                            MyPlane.isSkill2 = false;
                            isStartGif = true;
                            accelerationConounter = 0;
                            return;
                        }
                    }
                }
            }
            else if (isPlayGif && isPlaySkill2)
            {
                for (int index = 0; index < skillPictures2.Count; index++)
                {
                    skillPictures2[index].GameUpdate();
                }
            }

        }
        /// <summary>
        /// 生成游戏技能图片以及背景动图的对象
        /// </summary>
        public static void CreateSikllPicture()
        {
            SkillPicture skillBackGround = new SkillPicture(0, 0);
            skillPictures.Add(skillBackGround);

            NotMoving skillPicture1 = new NotMoving(Resources.PlayerSkillPicture, -100, 40);
            skillPictures1.Add(skillPicture1);

            NotMoving skillPicture2 = new NotMoving(Resources.PlayerSkillPicture2, -200, 40);
            skillPictures2.Add(skillPicture2);
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
            if (!isPlayGif)
            {
                generationSpeed++;
            }
            if (generationSpeed >= 60 && !isPlayGif)
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
        /// 创建以及销毁生命值读条的对象
        /// </summary>
        public static void HealthUpdate()
        {
            foreach (NotMoving lpf in lpfs)
            {
                lpf.GameUpdate();
            }
            foreach (MyPlane myPlane in myPlanes)
            {
                if (myPlane.HP > 0)
                {
                    for (int index = 0; index < lps.Count - (5 - myPlane.HP) * 6; index++)
                    {
                        lps[index].GameUpdate();
                    }
                }
            }
        }
        /// <summary>
        /// 创建生命值读条的对象
        /// </summary>
        public static void CreateHealth()
        {
            NotMoving lpf = new NotMoving(Resources.lpf, 42, 490);
            lpfs.Add(lpf);
            for (int count = 0; count < 30; count++)
            {
                NotMoving lp = new NotMoving(Resources.lp, 42+count * 6, 490);
                lps.Add(lp);
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
