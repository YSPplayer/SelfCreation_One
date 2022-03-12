using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace 自游飞机
{   enum Flag
    {
        player,
        enemy,
    }
    /// <summary>
    /// 管理飞机子弹的类
    /// </summary>
    class Bullet:Movthing
    {
        public Flag Flag { get; set; }
        public Bullet(Bitmap gameMap, int x, int y, int speed, Flag flag)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.Flag = flag;
            this.ImageObject = gameMap;
            this.Width = gameMap.Width;
            this.Height = gameMap.Height;
        }
        public override void GameUpdate()
        {
            ImageObject.MakeTransparent(Color.Black);
            checkDestory();
            Move();
            base.GameUpdate();
        }
        /// <summary>
        /// 子弹移动的方法
        /// </summary>
        private void Move()
        {
            switch (Flag)
            {
                case Flag.player:
                     Y = Y - Speed;
                break;
                case Flag.enemy:
                    Y = Y + Speed;
                    break;
            }
         
        }
        /// <summary>
        /// 检查子弹是否需要被销毁以及攻击到敌人的方法
        /// </summary>
        private void checkDestory()
        {
            if (X < 0 || X > 550 || Y < 0 || Y > 550)
            {
                this.isDestroy = true;return;
            }
            switch(Flag)
            {
                case Flag.player:
                    if (GameManage.IsCollideEnemy(GetRectangle(X + 9, Y + 9, 4, 4)) != null)
                    {
                        GameManage.IsCollideEnemy(GetRectangle(X + 9, Y + 9, 4, 4)).HP --;
                        Explosive explosive = new Explosive(X + 9, Y + 9);
                        GameManage.explosives.Add(explosive);
                        this.isDestroy = true;
                    }
                    break;
                case Flag.enemy:
                    if (GameManage.IsCollidePlayer(GetRectangle(X + 9, Y + 9, 4, 4)) != null)
                    {
                        GameManage.IsCollidePlayer(GetRectangle(X + 9, Y + 9, 4, 4)).HP --;
                        Explosive explosive = new Explosive(X + 9, Y + 9);
                        GameManage.explosives.Add(explosive);
                        this.isDestroy = true;
                    }
                    break;
            }


        }
    }
}
