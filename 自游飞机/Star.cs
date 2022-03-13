using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace 自游飞机
{
    class Star: Movthing
    {
        public Star(Bitmap gameMap, int x, int y,int speed)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.ImageObject = gameMap;
            this.Width = gameMap.Width;
            this.Height = gameMap.Height;
        }
        public override void GameUpdate()
        {
            ImageObject.MakeTransparent(Color.Black);
            if (!GameManage.isPlayGif)
            {
                Move();
            }
            base.GameUpdate();
        }
        /// <summary>
        /// 移动物体移动的方法，只向上运动
        /// </summary>
        private void Move()
        {
            if(this.Y > 450)
            {
                this.Y = 0;
            }
            this.Y += this.Speed;
        }
    }
}
