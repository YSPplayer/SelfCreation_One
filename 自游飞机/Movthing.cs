using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 自游飞机
{
    class Movthing:WindowObject
    {
        public int Speed { get; set; }
        public int HP { get; set; }
        public bool isDestroy;

        /// <summary>
        /// 获得当前物体的矩形大小
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }
        /// <summary>
        /// 获得当前物体的矩形大小
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle(int x,int y,int width,int height)
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }
        /// <summary>
        /// 当玩家或者敌人的HP归零时破坏
        /// </summary>
        public void Destory()
        {
            if (this.HP <= 0)
            {
                this.isDestroy = true;
            }
        }
    }
}
