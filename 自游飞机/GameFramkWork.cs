using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自游飞机
{
    /// <summary>
    /// 主要管理和游戏线程运行相关的游戏行为，起到连接的中介作用。
    /// </summary>
    class GameFramkWork
    {
        public static void Start()
        {
            KeyManage.MenuUpdate();
        }
        public static void Update()
        {
            KeyManage.MenuUpdate();
        }
    }
}
