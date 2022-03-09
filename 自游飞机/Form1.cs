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
        public Form1()
        {
            InitializeComponent();
            //画布必须在这声明，在下面声明的话会把默认的电脑桌面当做画布
            WindowG = this.CreateGraphics();
            thread = new Thread(new ThreadStart(GameInThread));
            thread.Start();
        
        }

        //游戏运行开启的额外线程
        private void GameInThread()
        {
              int sleepTime = 1000 / 60;
              while (true)
              {

                GameFramkWork.Start();
                Thread.Sleep(sleepTime);
              }

        }

        //当窗口关闭时线程对应关闭
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            KeyObject.MouseLocation(e);
        }
    }
}
