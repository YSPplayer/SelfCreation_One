using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
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
        /// 敌人飞机发射子弹时调用，向集合中增加子弹对象
        /// </summary>
        private void ShootBullet()
        {
            ShootSpeed+= ShootSpeed;
            if (ShootSpeed >= 60)
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
                ShootSpeed = 0;
            }
        }
    }
}
