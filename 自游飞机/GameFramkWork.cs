using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 主要管理和游戏线程运行相关的游戏行为，起到连接的中介作用。
    /// </summary>
    class GameFramkWork
    {
        public static void Start()
        {
            KeyManage.CreateMenu();
        }
        public static void Update()
        {
            //按钮点击之后进行不同方法的绘制，可能让结果更清晰


            //游戏开始前的检查
            //游戏未开始的菜单绘制
            if (!GameManage.isGameBegins)
            {
                if (KeyManage.isKeyOpenGameBoxClick)
                {
                    KeyManage.GameBoxUpdate();
                }
                //窗口退出按钮点否时返回原菜单
                else if (KeyManage.iskeyExitBoxClick)
                {
                    Form1.WindowG.DrawImage(Resources.MenuBackgroundGame, 0, 0);
                    KeyManage.iskeyExitBoxClick = false;
                }
                else if (!KeyManage.isOpenExitBox)
                {
                    KeyManage.MenuUpdate();
                }
                else
                {
                    KeyManage.OpenExitBoxUpdate();
                }
            }
            //游戏开始的绘制
            else
            {
                //在这里绘制游戏的画布，没有这一步就会无限的增加
                WindowObject.tempG.Clear(Color.Black);
                GameManage.Update();
                Form1.WindowG.DrawImage(WindowObject.tempBmp, 0, 0);
              
            }

        }
    }
}
