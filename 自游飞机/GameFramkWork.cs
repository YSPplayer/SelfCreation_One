using System;
using System.Collections.Generic;
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

            //窗口退出按钮点否时返回原菜单
            if (KeyManage.iskeyExitBoxClick)
            {
                Form1.WindowG.DrawImage(Resources.MenuBackgroundGame,0,0);
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
    }
}
