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
            Move();
            base.GameUpdate();
        }
        /// <summary>
        /// 子弹移动的方法
        /// </summary>
        private void Move()
        {
            Y = Y - Speed;
        }
    }
}
