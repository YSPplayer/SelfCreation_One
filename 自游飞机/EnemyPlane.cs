using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自游飞机.Properties;

namespace 自游飞机
{   enum Enemytype
    {
        Enemy1,
        Enemy2,
        Enemy3,
        Boss,
    }
    class EnemyPlane : Movthing
    {
        public int ShootSpeed { get; set;}
        public Enemytype Type { get; set;}
        //BOSS的随机移动速度
        private Random ranBool = new Random();
        //BOSS的随机移动速度帧数控制;
        private int BossSeppd = 0;
        //子弹发射速度
        private int bulletBossSeppd = 0;
        private int bulletSeppd = 0;

        //BOSS技能的实物
        public bool killStart;
        //控制BOSS技能帧数的变量
        private int skillSpeed=0;
        //控制BOSS技能次数的变量;
        private int skillTimes = 0;
        //检查BOSS是否释放大招
        public static bool isSkill;
        public EnemyPlane(Bitmap gameMap, int x, int y,int speed,int shootSpeed,int hp,bool value,Enemytype type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
            this.HP = hp;
            this.Value = value;
            this.Speed = speed;
            this.ShootSpeed = shootSpeed;
            this.ImageObject = gameMap;
            this.Width = gameMap.Width;
            this.Height = gameMap.Height;
        }
        public override void GameUpdate()
        {
            ImageObject.MakeTransparent(Color.White);
            if (!GameManage.isPlayGif)
            {
                CheckDestory();
                Destory();
                Move();
                ShootBullet();
                BossSkill();
                BossShoot();
            }
            base.GameUpdate();
        }
        /// <summary>
        /// 敌人飞机的移动方法
        /// </summary>
        private void Move()
        {
            if (this.Type != Enemytype.Boss)
            {
                this.Y = this.Y + this.Speed;
            }
            else
            {
                   BossSeppd++;               
                    //防止超过边界
                    if (X <= -80)
                    {
                        this.Speed = 3;
                    }
                    else if (X >= 200)
                    {
                        this.Speed = -3;
                    }
                    else
                    {
                      if (BossSeppd >= 60)
                      {
                         int res = ranBool.Next(0, 2);
                         if (res == 0)
                         {
                             this.Speed = 3;
                         }
                         else
                         {
                             this.Speed = -3;
                         }
                         BossSeppd = 0;
                      }
                    }
                this.X = this.X + this.Speed;
            }
        }
        /// <summary>
        /// 检查敌人飞机是否需要被销毁的方法
        /// </summary>
        private void CheckDestory()
        {
            if (this.Type != Enemytype.Boss)
            {
                if (X < 0 || X > 550 || Y < 0 || Y > 550)
                {
                    this.isDestroy = true; return;
                }
            }
        }
        /// <summary>
        /// BOSS技能的实物效果
        /// </summary>
        private void BossSkill()
        {
            if (killStart)
            {
                skillSpeed++;
                if (skillSpeed >= 40)
                {
                    int ranX;
                    int ranY;
                    for (int count = 0; count < 16; count++)
                    {
                        ranX = ranBool.Next(0, 451);
                        ranY = ranBool.Next(0, 451);
                        Explosive bossExplosive = new Explosive(ranX, ranY);
                        GameManage.bossExplosives.Add(bossExplosive);
                    }
                    skillSpeed = 0;
                    skillTimes++;
                }
                if (skillTimes >= 3)
                {
                    killStart = false;
                    skillTimes = 0;
                }
            }
        }
        /// <summary>
        /// BOSS发动子弹攻击的行为
        /// </summary>
        private void BossShoot()
        {
            if (this.Type == Enemytype.Boss)
            {
                bulletBossSeppd += ShootSpeed;
                if (bulletBossSeppd >= 120)
                {
                    int res=ranBool.Next(0, 11);
                    switch (res)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 5:
                        case 6:
                            int ranX = 0;
                            int ranY = 0;
                            for (int count = 0; count < 30; count++)
                            {
                                ranX = ranBool.Next(X + this.Width / 2-150, X + this.Width / 2 + 150);
                                ranY = ranBool.Next(0, 250);
                                Bullet bullet = new Bullet(Resources.BulletDown, ranX, ranY, 6, Flag.enemy);
                                GameManage.enemyBullets.Add(bullet);
                            }
                            break;
                        case 3:
                        case 4:
                        case 7:
                        case 8:
                            int index = 0;
                            int ranPlanX = 0;
                            int ranPlanY = 0;
                            for (int count = 0; count < 4; count++)
                            {
                                index = ranBool.Next(0, 3);
                                ranPlanX = ranBool.Next(0, 451);
                                ranPlanY = ranBool.Next(0, 250);
                                switch (index)
                                {
                                    case 0:
                                        EnemyPlane plane1 = new EnemyPlane(Resources.EnemyPlane1, ranPlanX, ranPlanY, 2, 1, 4, false, Enemytype.Enemy1);
                                        GameManage.enPlanes.Add(plane1);
                                        break;
                                    case 1:
                                        EnemyPlane plane2 = new EnemyPlane(Resources.EnemyPlane2, ranPlanX, ranPlanY, 4, 2, 3, false, Enemytype.Enemy2);
                                        GameManage.enPlanes.Add(plane2);
                                        break;
                                    case 2:
                                        EnemyPlane plane3 = new EnemyPlane(Resources.EnemyPlane3, ranPlanX, ranPlanY, 6, 2, 2, false, Enemytype.Enemy3);
                                        GameManage.enPlanes.Add(plane3);
                                        break;

                                }
                            }
                            break;
                        default:
                            isSkill = true;
                            break;
                    }
                    bulletBossSeppd = 0;
                }
            }
        }
        /// <summary>
        /// 敌人飞机发射子弹时调用，向集合中增加子弹对象
        /// </summary>
        private void ShootBullet()
        {
            if (this.Type != Enemytype.Boss)
            {
                bulletSeppd += ShootSpeed;
                if (bulletSeppd >= 60)
                {
                    switch (Type)
                    {
                        case Enemytype.Enemy1:
                            Bullet bullet1 = new Bullet(Resources.BulletDown, X + 25, Y + 37, 3, Flag.enemy);
                            GameManage.enemyBullets.Add(bullet1);
                            break;
                        case Enemytype.Enemy2:
                            Bullet bullet2_1 = new Bullet(Resources.BulletDown, X + 13, Y + 36, 5, Flag.enemy);
                            Bullet bullet2_2 = new Bullet(Resources.BulletDown, X + 33, Y + 36, 5, Flag.enemy);
                            GameManage.enemyBullets.Add(bullet2_1);
                            GameManage.enemyBullets.Add(bullet2_2);
                            break;
                        case Enemytype.Enemy3:
                            Bullet bullet3_1 = new Bullet(Resources.BulletDown, X + 25, Y + 34, 8, Flag.enemy);
                            Bullet bullet3_2 = new Bullet(Resources.BulletDown, X + 25, Y + 43, 8, Flag.enemy);
                            GameManage.enemyBullets.Add(bullet3_1);
                            GameManage.enemyBullets.Add(bullet3_2);
                            break;

                    }
                    bulletSeppd = 0;
                }
            }
        }
    }
}
