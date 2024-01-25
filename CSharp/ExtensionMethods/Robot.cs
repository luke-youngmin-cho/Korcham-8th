using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    internal sealed class Robot
    {
        public int this[int index]
        {
            get
            {
                return 1;
            }
            set
            {

            }
        }

        public Robot(string name)
        {
            this.Name = name;
        }

        public string Name;

        public static void SayGoodMorning2(Robot robot)
        {
            Console.WriteLine($"[{robot.Name}] : Good morning sir.");
        }


        public void SayGoodMorning()
        {
            Console.WriteLine($"[{this.Name}] : Good morning sir.");
        }
    }
}
