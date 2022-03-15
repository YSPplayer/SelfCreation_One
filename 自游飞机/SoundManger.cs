using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using 自游飞机.Properties;

namespace 自游飞机
{
    class SoundManger
    {
        private static SoundPlayer Shoot = new SoundPlayer();
        private static SoundPlayer Shell = new SoundPlayer();
        private static SoundPlayer Skill_1 = new SoundPlayer();
        private static SoundPlayer Skill_2 = new SoundPlayer();

        //初始化所有的声音
        public static void InitSound()
        {
            Shoot.Stream = Resources.Shoot;
            Shell.Stream = Resources.Shell_1;
            Skill_1.Stream = Resources.Skill1_1;
            Skill_2.Stream = Resources.Skill2_1;
        }
        public static void PlayShoot()
        {
            Shoot.Play();
        }
        public static void PlayShell()
        {
            Shell.Play();
        }
        public static void PlaySkill_1()
        {
            Skill_1.Play();
        }
        public static void PlaySkill_2()
        {
            Skill_2.Play();
        }
    }
}
