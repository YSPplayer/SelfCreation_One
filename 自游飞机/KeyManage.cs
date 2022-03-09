using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 游戏外菜单行为的主管理类，与游戏管理类平级，向上游线程提供自己的结果。
    /// </summary>
    class KeyManage
    {
        //鼠标不在按钮内的生成
        private static List <KeyObject> Menus = new List <KeyObject>();

        //鼠标在按钮内的生成
        private static List<KeyObject> NewMenus = new List<KeyObject>();
        //因为线程每次都调用创建方法，所有让它只调用一次。
        private static bool isAddMenu;
        //鼠标是否在菜单按钮内的布尔检查
        private static bool isCheckTouch;

        //为了避免可以按下多个按钮，进行检查
        //private static bool isCheckStart;
        //private static bool isCheckCheat;
        //private static bool isCheckHelp;
        //private static bool isCheckExit;

        //生成按钮所有的对象
        //--------------------------------------------------------------------------------------
        private static KeyObject keyStartPre = new KeyObject(Resources.StartButtonPre, 300, 160);
        private static KeyObject keyCheatPre = new KeyObject(Resources.CheatButtonPre, 300, 240);
        private static KeyObject keyHelpPre = new KeyObject(Resources.HelpButtonPre, 300, 320);
        private static KeyObject keyExitPre = new KeyObject(Resources.ExitButtonPre, 300, 400);

        private static KeyObject keyStartEd = new KeyObject(Resources.StartButtonEd, 300, 160);
        private static KeyObject keyCheatEd = new KeyObject(Resources.CheatButtonEd, 300, 240);
        private static KeyObject keyHelpEd = new KeyObject(Resources.HelpButtonEd, 300, 320);
        private static KeyObject keyExitEd = new KeyObject(Resources.ExitButtonEd, 300, 400);
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// 生成菜单按钮
        /// </summary>
        public static void MenuUpdate()
        {
            if (!isAddMenu)
            {
                CreateMenu();
                isAddMenu = true;
            }
            CreateNewMenu();
            if (!isCheckTouch)
            {
                foreach (KeyObject Menu in Menus)
                {
                    Menu.Update();
                }
            }
            else
            {
                foreach (KeyObject NewMenu in NewMenus)
                {
                    NewMenu.Update();
                }
            }
        } 
        /// <summary>
        /// 向集合内添加菜单按钮
        /// </summary>
        public static void CreateMenu()
        {
            Menus.Add(keyStartPre);
            Menus.Add(keyCheatPre);
            Menus.Add(keyHelpPre);
            Menus.Add(keyExitPre);

            NewMenus.Add(keyStartPre);
            NewMenus.Add(keyCheatPre);
            NewMenus.Add(keyHelpPre);
            NewMenus.Add(keyExitPre);
        }

        /// <summary>
        ///创建鼠标触碰之后的菜单按钮
        /// </summary>
        public static void CreateNewMenu()
        {
            //这里不知道为啥这个宽高度要除以2，但实际检查的时候确实会超过当前的高度。
            if (KeyObject.CheckTouch(keyStartPre.X, keyStartPre.Y, keyStartPre.Width / 2, keyStartPre.Height/ 2))
            {
                    isCheckTouch = true;
                    NewMenus[0] = keyStartEd;
            }
            else if (KeyObject.CheckTouch(keyCheatPre.X, keyCheatPre.Y, keyCheatPre.Width / 2, keyCheatPre.Height / 2))
            {
                    isCheckTouch = true;
                    NewMenus[1] = keyCheatEd;
            }
            else if (KeyObject.CheckTouch(keyHelpPre.X, keyHelpPre.Y, keyHelpPre.Width / 2, keyHelpPre.Height / 2))
            {
                    isCheckTouch = true;
                    NewMenus[2] = keyHelpEd;
            }
            else if (KeyObject.CheckTouch(keyExitPre.X, keyExitPre.Y, keyExitPre.Width / 2, keyExitPre.Height / 2))
            {
                    isCheckTouch = true;
                    NewMenus[3] = keyExitEd;
            }
            else
            {
                //检查按钮重置
                isCheckTouch = false;
                if (NewMenus.Contains(keyStartEd) || NewMenus.Contains(keyCheatEd)
                     || NewMenus.Contains(keyHelpEd) || NewMenus.Contains(keyExitEd))
                {
                        NewMenus.Clear();
                        NewMenus.Add(keyStartPre);
                        NewMenus.Add(keyCheatPre);
                        NewMenus.Add(keyHelpPre);
                        NewMenus.Add(keyExitPre);
                }
            }
        }
    }
}
