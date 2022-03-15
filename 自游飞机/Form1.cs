using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 自游飞机.Properties;
using System.Threading;

namespace 自游飞机
{
    public partial class Form1 : Form
    {
        public static Graphics WindowG;
        public Thread thread;
        //检查当前的按键按下的行为是否持续，持续就返回false
        private static bool isKeyDuration;
        public Form1()
        {

            InitializeComponent();
            //WindowObject.tempG ;
            //画布必须在这声明，在下面声明的话会把默认的电脑桌面当做画布
            WindowG = this.CreateGraphics();
            thread = new Thread(new ThreadStart(GameInThread));
            thread.Start();
        
        }

        //游戏运行开启的额外线程
        private void GameInThread()
        {
            int sleepTime = 1000 / 60;
            //Bitmap tempBmp = new Bitmap(550, 550);
            //Graphics tempG = Graphics.FromImage(tempBmp);
            //bitmap.MakeTransparent(Color.White);
            //bitmap.MakeTransparent(Color.Black);
            //Bitmap bitmap = Resources.BossLpf;
            //Bitmap bitmap2 = Resources.MyPlane;
            GameFramkWork.Start();
              while (true)
              {
                 //tempG.Clear(Color.Black);
                 //tempG.Clear(Color.Black);
                 //tempG.DrawImage(bitmap, 0, 0);
                // WindowG.DrawImage(bitmap, 0, 0);
                //WindowG.DrawImage(bitmap2, 100, 0);
                //WindowG.DrawImage(bitmap2, 0, 0);
                GameFramkWork.Update();
                Thread.Sleep(sleepTime);
              }

        }
        //当窗口关闭时线程对应关闭
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

        /// <summary>
        /// 当鼠标移动到指定位置时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            KeyObject.MouseLocation(e);
        }
        /// <summary>
        /// 当鼠标点击时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (KeyObject.CheckTouch(KeyManage.keyStartPre.X, KeyManage.keyStartPre.Y, KeyManage.keyStartPre.Width / 2, KeyManage.keyStartPre.Height / 2, e.X, e.Y))
            {
                KeyManage.iskeyStartClick = true;
                KeyManage.isKeyOpenGameBoxClick = true;
                KeyManage.Speed = 0;
            }
            else if (KeyObject.CheckTouch(KeyManage.keyCheatPre.X, KeyManage.keyCheatPre.Y, KeyManage.keyCheatPre.Width / 2, KeyManage.keyCheatPre.Height / 2, e.X, e.Y))
            {
                KeyManage.iskeyCheatClick = true;
            }
            else if (KeyObject.CheckTouch(KeyManage.keyHelpPre.X, KeyManage.keyHelpPre.Y, KeyManage.keyHelpPre.Width / 2, KeyManage.keyHelpPre.Height / 2, e.X, e.Y))
            {
                KeyManage.iskeyHelpClick = true;
            }
            else if (KeyObject.CheckTouch(KeyManage.keyExitPre.X, KeyManage.keyExitPre.Y, KeyManage.keyExitPre.Width / 2, KeyManage.keyExitPre.Height / 2, e.X, e.Y))
            {
                KeyManage.iskeyExitClick = true;
            }
            else if (KeyObject.IsOnSide(KeyManage.KeyExitBox.X, KeyManage.KeyExitBox.Y + 50, KeyManage.KeyExitBox.Width / 3, KeyManage.KeyExitBox.Height / 3, e.X, e.Y) == 1)
            {
                //点击则是关闭游戏窗口
                KeyManage.iskeyExitBoxClick = true;
                KeyManage.isOpenExitBox = false;
                KeyManage.Speed = 0;
                this.Close();
                
            }
            else if (KeyObject.IsOnSide(KeyManage.KeyExitBox.X, KeyManage.KeyExitBox.Y + 50, KeyManage.KeyExitBox.Width / 3, KeyManage.KeyExitBox.Height / 3, e.X, e.Y) == 0)
            {
                //点击否则返回原菜单，同时关闭其他几个开关
                KeyManage.iskeyExitBoxClick = true;
                KeyManage.isOpenExitBox = false;
                KeyManage.Speed = 0;
            }
            else
            {
                KeyManage.iskeyStartClick = false;
                KeyManage.iskeyCheatClick = false;
                KeyManage.iskeyHelpClick = false;
                KeyManage.iskeyExitClick = false;
            }
        }
        /// <summary>
        /// 当按下键盘的按键时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isKeyDuration)
            {
                MyPlane.MoveKey(e);
            }
            if (e.KeyCode == Keys.J)
            {
                isKeyDuration = true;
            }
        }

        /// <summary>
        /// 当按下的键被抬起时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            MyPlane.KeyRelease(e);
            isKeyDuration = false;
        }
    }
}
