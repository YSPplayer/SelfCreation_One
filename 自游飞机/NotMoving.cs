using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自游飞机
{
    class NotMoving:WindowObject
    {
        public NotMoving(Bitmap gameMap, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.ImageObject = gameMap;
            this.Width = gameMap.Width;
            this.Height = gameMap.Height;
        }
        public override void GameUpdate()
        {
            ImageObject.MakeTransparent(Color.White);
            base.GameUpdate();
        }
    }
}
