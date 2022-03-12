using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 放置爆炸特效的类
    /// </summary>
    class Explosive:Movthing
    {
        private double speed= -0.5;
        private static int index = 0;
        private static Bitmap[] bitmaps = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5,
        };
        public Explosive(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        private static Bitmap GetDrawPicture(int index)
        {
            return bitmaps[index];
        }
        /// <summary>
        /// 提供爆炸特效的绘制方法
        /// </summary>
        public override void GameUpdate()
        {
            speed+=0.5;
            if ((speed * 10) % 2 == 0 && speed > 0)
            {
                index++;
            }
            if (index > 4)
            {
                index = 4;
                this.isDestroy = true;
            }
            if (speed >= 40)
            {
                speed = -0.5;
            }
            GetDrawPicture(index).MakeTransparent(Color.Black);
            this.ImageObject= GetDrawPicture(index);
            base.GameUpdate();
        }
         
    
    }
}
