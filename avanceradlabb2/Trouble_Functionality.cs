using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avanceradlabb2
{
    public class Trouble_Functionality : Trouble
    {
        public int AdjustSpeed { get; set; }

        public Trouble_Functionality(int adj, string name, int prob, string desc) : base(name, prob, desc)
        {
            AdjustSpeed = adj;
        }
    }
}
