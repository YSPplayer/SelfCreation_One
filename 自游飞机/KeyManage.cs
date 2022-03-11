using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 自游飞机.Properties;
using System.Windows.Forms;

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

        //鼠标是否在菜单按钮内的布尔检查
        private static bool isCheckTouch;
        //按退出键之后是否弹出退出框的布尔检查
        public static bool isOpenExitBox;
        //鼠标在各个按钮内的布尔检查
        public static bool iskeyStartClick;
        public static bool iskeyCheatClick;
        public static bool iskeyHelpClick;
        public static bool iskeyExitClick;

        public static bool iskeyExitBoxClick;
        public static bool isKeyOpenGameBoxClick;
        //帧数的速度
        public static int Speed;
        //生成按钮所有的对象
        //--------------------------------------------------------------------------------------
        public static KeyObject keyStartPre = new KeyObject(Resources.StartButtonPre, 300, 160);
        public static KeyObject keyCheatPre = new KeyObject(Resources.CheatButtonPre, 300, 240);
        public static KeyObject keyHelpPre = new KeyObject(Resources.HelpButtonPre, 300, 320);
        public static KeyObject keyExitPre = new KeyObject(Resources.ExitButtonPre, 300, 400);

        private static KeyObject keyStarting = new KeyObject(Resources.StartButtoning, 300, 160);
        private static KeyObject keyCheating = new KeyObject(Resources.CheatButtoning, 300, 240);
        private static KeyObject keyHelping = new KeyObject(Resources.HelpButtoning, 300, 320);
        private static KeyObject keyExiting = new KeyObject(Resources.ExitButtoning, 300, 400);

        private static KeyObject keyStartEd = new KeyObject(Resources.StartButtonEd, 300, 160);
        private static KeyObject keyCheatEd = new KeyObject(Resources.CheatButtonEd, 300, 240);
        private static KeyObject keyHelpEd = new KeyObject(Resources.HelpButtonEd, 300, 320);
        private static KeyObject keyExitEd = new KeyObject(Resources.ExitButtonEd, 300, 400);
        //--------------------------------------------------------------------------------------
        //生成退出游戏图像框
        public static KeyObject KeyExitBox = new KeyObject(Resources.ExitBox, 77, 200);
        private static KeyObject KeyExitBoxYes = new KeyObject(Resources.ExitBoxYes, 77, 200);
        private static KeyObject KeyExitBoxNo = new KeyObject(Resources.ExitBoxNo, 77, 200);
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// 生成菜单按钮
        /// </summary>
        public static void MenuUpdate()
        {
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
        /// 生成退出窗口按钮
        /// </summary>
        public static void OpenExitBoxUpdate()
        {
                if (Speed < 3)
                {
                    Speed++;
                    foreach (KeyObject NewMenu in NewMenus)
                    {
                        NewMenu.Update();
                    }
                  
                }
                else
                {
                    Menus[0].Update();
                    Menus[3].Update();
                    CreateNewExitBox();
                }
        }
        /// <summary>
        /// 生成游戏开始的界面
        /// </summary>
        public static void GameBoxUpdate()
        {
            if (Speed < 5)
            {
                //创造一种按钮已经被点击的动画效果
                Speed++;
                for (int index = 0; index < NewMenus.Count; index++)
                {
                    if (NewMenus[0] != keyStartEd)
                    {
                        NewMenus[0] = keyStartEd;
                    }
                    NewMenus[index].Update();
                }

            }
            else if (Speed < 10)
            {
                Speed++;
                foreach (KeyObject menu in Menus)
                {
                    menu.Update();
                }
            }
            else
            {
                Form1.WindowG.Clear(Form1.DefaultForeColor);
                CreateGameObject();
                isKeyOpenGameBoxClick = false;
                GameManage.isGameBegins = true;
            }
        }
        /// <summary>
        /// 在这里统一完成开始游戏时创建游戏对象的工作，游戏开始之前创建
        /// </summary>
        private static void CreateGameObject()
        { 
            GameManage.DrawStars();
            GameManage.CreatMyPlane();
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
        /// 提供重置按钮至初始集合数量的方法。
        /// </summary>
        private static void ResetList()
        {
            NewMenus[0] = keyStartPre;
            NewMenus[1] = keyCheatPre;
            NewMenus[2] = keyHelpPre;
            NewMenus[3] = keyExitPre;
        }
        /// <summary>
        ///创建鼠标触碰之后的退出窗口
        /// </summary>
        public static void CreateNewExitBox()
        {
            //这些数字都是事后通过打开窗口不断调试出的，并无第一次的经验
            switch (KeyObject.IsOnSide(KeyExitBox.X, KeyExitBox.Y+50, KeyExitBox.Width / 3, keyStartPre.Height / 3))
            {
                case 1:
                    KeyExitBoxYes.Update();
                    break;
                case 0:
                    KeyExitBoxNo.Update();
                    break;
                default:
                    KeyExitBox.Update();
                    break;
            }

        }
        /// <summary>
        ///创建鼠标触碰之后的菜单按钮
        /// </summary>
        public static void CreateNewMenu()
        {
            //这里不知道为啥这个宽高度要除以2，但实际检查的时候确实会超过当前的高度。
            if (KeyObject.CheckTouch(keyStartPre.X, keyStartPre.Y, keyStartPre.Width / 2, keyStartPre.Height / 2))
            {
                //把点击范围设置成false，只有在这个范围之内点击之后才设置成true
                //要把数组内每一个数都重置，防止它点击后不会重置
                isCheckTouch = true;
                ResetList();
                if (iskeyStartClick)
                {
                    NewMenus[0] = keyStartEd;
                }
                else
                {
                    NewMenus[0] = keyStarting;
                }
            }
            else if (KeyObject.CheckTouch(keyCheatPre.X, keyCheatPre.Y, keyCheatPre.Width / 2, keyCheatPre.Height / 2))
            {
                isCheckTouch = true;
                ResetList();
                if (iskeyCheatClick)
                {
                    NewMenus[1] = keyCheatEd;
                }
                else
                {
                    NewMenus[1] = keyCheating;
                }
            }
            else if (KeyObject.CheckTouch(keyHelpPre.X, keyHelpPre.Y, keyHelpPre.Width / 2, keyHelpPre.Height / 2))
            {
                isCheckTouch = true;
                ResetList();
                if (iskeyHelpClick)
                {
                    NewMenus[2] = keyHelpEd;
                }
                else
                {
                    NewMenus[2] = keyHelping;
                }
            }
            else if (KeyObject.CheckTouch(keyExitPre.X, keyExitPre.Y, keyExitPre.Width / 2, keyExitPre.Height / 2))
            {
                isCheckTouch = true;
                ResetList();
                if (iskeyExitClick)
                {
                    NewMenus[3] = keyExitEd;
                    isOpenExitBox = true;
                }
                else
                {
                    NewMenus[3] = keyExiting;
                }
            }
            else
            {
                //检查按钮重置
                isCheckTouch = false;
                //鼠标点击检查重置

                iskeyStartClick = false;
                iskeyCheatClick = false;
                iskeyHelpClick = false;
                iskeyExitClick = false;
                if (NewMenus.Contains(keyStarting) || NewMenus.Contains(keyCheating)
                     || NewMenus.Contains(keyHelping) || NewMenus.Contains(keyExiting))
                {
                    ResetList();
                }
            }
        }
    }
}
