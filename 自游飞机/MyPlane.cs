using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using 自游飞机.Properties;

namespace 自游飞机
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    class MyPlane: Movthing
    {
        //检查按下是否已经抬起，移动
        public static bool isMove;

        //检查碰到边界不能移动的布尔值
        private static bool isMoveUp;
        private static bool isMoveDown;
        private static bool isMoveLeft;
        private static bool isMoveRight;

        //获取玩家的移动方向
        private static Direction dir;
        //检查玩家是否需要攻击
        private static bool isAttack;
        //检查玩家是否发动技能
        public static bool isSkill1;
        public static bool isSkill2;
        //检查玩家发动技能后产生的实际效果
        public bool killStart1;
        public bool killStart2;
        //1技能实物的生效时间
        public static int killStart1Speed=0;
        public static int killStart2Count = 0;
        public MyPlane(Bitmap gameMap, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Speed = 4;
            this.HP = 5;
            this.ImageObject = gameMap;
            this.Width = gameMap.Width;
            this.Height = gameMap.Height;
        }
        public override void GameUpdate()
        {
            ImageObject.MakeTransparent(Color.White);
            if (!GameManage.isPlayGif)
            {
                StartSkill();
                CollisionCheck();
                DestructiveCollisionCheck();
                Destory();
                Move();
                ShootBullet();
            }
            base.GameUpdate();
        }
        /// <summary>
        /// 检查是否和其他物体碰撞（）
        /// </summary>
        private void DestructiveCollisionCheck()
        {
            if (GameManage.IsCollideEnemy(GetRectangle(X,Y+5, ImageObject.Width, ImageObject.Height)) != null)
            {
                GameManage.IsCollideEnemy(GetRectangle(X, Y + 5, ImageObject.Width, ImageObject.Height)).isDestroy = true;
                if (!killStart1)
                {
                    HP--;
                }
                Explosive explosive = new Explosive(X, Y + 5);
                GameManage.explosives.Add(explosive);
            }
        }
        /// <summary>
        /// 检查是否和边界碰撞 
        /// </summary>
        private void CollisionCheck()
        {
            if (Y + ImageObject.Height + Speed <= 500)
            {
                isMoveDown = false;

            }
            if (Y - Speed >= 0)
            {
                isMoveUp = false;

            }
            if (X + ImageObject.Width + Speed <= 550)
            {
                isMoveRight = false;

            }
            if (X - Speed >= 0)
            {
                isMoveLeft = false;

            }
//-------------------------------------------------------------
            if (Y + ImageObject.Height + Speed > 500)
            {
                if (X + ImageObject.Width + Speed > 550)
                {
                    isMoveRight = true;
                }
                if (X - Speed < 0)
                {
                    isMoveLeft = true;
                }
                isMoveDown = true;
             
            }
            else if (Y - Speed < 0)
            {
                if (X + ImageObject.Width + Speed > 550)
                {
                    isMoveRight = true;
                }
                if (X - Speed < 0)
                {
                    isMoveLeft = true;
                }
                isMoveUp = true;
            }
            else if (X + ImageObject.Width + Speed > 550)
            {
                isMoveRight = true;
            }
            else if (X - Speed < 0)
            {
                isMoveLeft = true;

            }
        }
        /// <summary>
        /// 根据飞机的移动方向来改变飞机的位置
        /// </summary>
        private void Move()
        {
            if (isMove)
            {
                switch (dir)
                {
                    case Direction.Up:
                        if (!isMoveUp)
                        {
                            Y = Y - Speed;
                        }
                        break;
                    case Direction.Down:
                        if (!isMoveDown)
                        {
                            Y = Y + Speed;
                        }
                        break;
                    case Direction.Left:
                        if (!isMoveLeft)
                        {
                            X = X - Speed;
                        }
                        break;
                    case Direction.Right:
                        if (!isMoveRight)
                        {
                            X = X + Speed;
                        }
                        break;
                }
            }
            
        }
        /// <summary>
        /// 飞机发射子弹时调用，向集合中增加子弹对象
        /// </summary>
        private void ShootBullet()
        {
            if (isAttack)
            {
                if (!killStart2)
                {
                    Bullet bullet = new Bullet(Resources.BulletUp, X + ImageObject.Width / 2 - 8, Y - 4, 4, Flag.player);
                    GameManage.playerBullets.Add(bullet);
                    SoundManger.PlayShoot();
                }
                else
                {
                    Bullet shell = new Bullet(Resources.Shell, X + ImageObject.Width / 2 - 8, Y +20, 2, Flag.playerShell);
                    GameManage.playerBullets.Add(shell);
                    SoundManger.PlayShell();
                    killStart2Count++;
                    if (killStart2Count >= 5)
                    {
                        killStart2 = false;
                        killStart2Count = 0;
                    }
                }
                //在这里设置false，它就只会发射1个子弹了
                isAttack = false;
            }
        }
        /// <summary>
        /// 飞机发动技能时调用，更改集合中防护罩的位置，和飞机同级
        /// </summary>
        private void StartSkill()
        {
            //后面是规范技能1的防护罩的移动
            if (killStart1)
            {
                killStart1Speed++;
                GameManage.covers[0].X = X;
                GameManage.covers[0].Y = Y-10;
                if (killStart1Speed >= 250)
                {
                    killStart1 = false;
                    killStart1Speed = 0;
                }
            }
        }
        /// <summary>
        /// 获取当前键盘按下的按键
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static void MoveKey(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    dir = Direction.Up;
                    isMove = true;
                    break;
                case Keys.S:
                    dir = Direction.Down;
                    isMove = true;
                    break;
                case Keys.A:
                    dir = Direction.Left;
                    isMove = true;
                    break;
                case Keys.D:
                    dir = Direction.Right;
                    isMove = true;
                    break;
                case Keys.J:
                    isAttack =true;
                    break;
                case Keys.G:
                    if (GameManage.skillValue >= 0 && !GameManage.isBossSkill) 
                    {
                        SoundManger.PlaySkill_1();
                        isSkill1 = true;
                        GameManage.skillValue = 0;
                    }
                    break;
                case Keys.H:
                    if (GameManage.skillValue >= 0 && !GameManage.isBossSkill)
                    {
                        SoundManger.PlaySkill_2();
                        isSkill2 = true;
                        GameManage.skillValue = 0;
                    }
                    break;

            }
        }
        /// <summary>
        /// 获取当前抬起释放的按键
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static void KeyRelease(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.S:
                case Keys.A:
                case Keys.D:
                    isMove = false;
                break;
                case Keys.J:
                    isAttack = false;
                    break;
            }
        }
    }
}
