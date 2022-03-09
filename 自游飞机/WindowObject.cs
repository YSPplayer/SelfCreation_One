using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自游飞机
{
    /// <summary>
    /// 主父类，窗口物体
    /// </summary>
    class WindowObject
    {
        public int X { get; set;}
        public int Y { get; set;}
        public int Width { get; set; }
        public int Height { get; set; }
        public Bitmap ImageObject { get; set; }
        private Graphics ImageG = Form1.WindowG;

        public void DrawSelf()
        {
            ImageG.DrawImage(ImageObject, X, Y);
        }

        public virtual void Update()
        {
            DrawSelf();
        }
    }
}
