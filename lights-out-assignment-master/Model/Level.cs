using System.Collections.Generic;

namespace lights_out_assignment_master.Model
{
    internal class Level
    {
        public string Name { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public List<int> On { get; set; }
    }
}
