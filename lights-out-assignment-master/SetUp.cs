using lights_out_assignment_master.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace lights_out_assignment_master
{
    internal class SetUp
    {
        public static List<Level> GetConfig()
        {
            using (StreamReader r = new StreamReader("lights-out-levels.json"))
            {
                string json = r.ReadToEnd();
                List<Level> items = JsonConvert.DeserializeObject<List<Level>>(json);
                return items;
            }
            throw new ArgumentNullException();
        }
        public static bool[,] ReadButtonSettings(Level level)
        {
            bool[,] onButton = new bool[level.Rows, level.Columns];

            int counter = 0;
            for (int i = 0; i < level.Rows; i++)
            {
                for (int j = 0; j < level.Columns; j++)
                {
                    if (level.On.Contains(counter))
                        onButton[i, j] = true;

                    else
                        onButton[i, j] = false;

                    counter++;
                }
            }
            return onButton;
        }
    }
}
