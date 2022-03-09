using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 自游飞机
{
    /// <summary>
    ///按钮物体类，继承自主父类窗口物体，主要管理和游戏场景外有关的按钮图片。
    /// </summary>
    class KeyObject:WindowObject
    {
        //被鼠标靠近的物体区域
        private static Rectangle ButtonArea;
        //鼠标当前的位置
        private static Point mouseLocation;
        public KeyObject(Bitmap keyMap,int x,int y)
        {
            this.X = x;
            this.Y = y;
            this.ImageObject = keyMap;
            this.Width = keyMap.Width;
            this.Height = keyMap.Height;
        }


        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 检查鼠标是否进入按钮物体内的方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool CheckTouch(int x,int y,int width, int height)
        {
            ButtonArea = new Rectangle(x, y, width, height);
            return ButtonArea.Contains(mouseLocation.X, mouseLocation.Y);
        }

        /// <summary>
        /// 获取当前鼠标的窗口坐标位置
        /// </summary>
        /// <param name="e"></param>
        public static void MouseLocation(MouseEventArgs e)
        {
            mouseLocation.X = e.X;
            mouseLocation.Y = e.Y;
        }
    }
}
