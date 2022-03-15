using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using 自游飞机.Properties;

namespace 自游飞机
{
    /// <summary>
    /// 用来管理技能释放时的动态背景特效
    /// </summary>
    class SkillPicture:Movthing
    {
        private double speed = -0.5;
        private static int index = 0;
        private static Bitmap[] bitmaps = new Bitmap[]
        {
            Resources.Skill0,
            Resources.Skill1,
            Resources.Skill2,
            Resources.Skill3,
            Resources.Skill4,
            Resources.Skill5,
            Resources.Skill6,
            Resources.Skill7,
            Resources.Skill8,
            Resources.Skill9,
        };
        public SkillPicture(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// 返回特效的对应索引图片
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Bitmap GetDrawPicture(int index)
        {
            return bitmaps[index];
        }
        /// <summary>
        /// 提供爆炸特效的绘制方法
        /// </summary>
        public override void GameUpdate()
        {
            speed += 0.5;
            if ((speed * 10) % 2 == 0 && speed > 0)
            {
                index++;
            }
            if (index > 9)
            {
                index = 0;
                //重置资源
                GameManage.isPlayGif = false;
                GameManage.isStartGif = false;
                GameManage.isPlaySkill1 = false;
                GameManage.isPlaySkill2 = false;
                GameManage.isBossSkill = false;
                GameManage.skillPictures1[0].X = -100;
                GameManage.skillPictures2[0].X = -200;
                GameManage.skillBossPictures[0].X = -50;
            }
            if (speed >= 40)
            {
                speed = -0.5;
            }
            this.ImageObject = GetDrawPicture(index);
            base.GameUpdate();
        }

    }
}
