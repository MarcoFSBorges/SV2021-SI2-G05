using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Item
    {  
        public int ItemID { get; set; }
        public int PlayerID { get; set; }
        public bool Active { get; set; }
        public double BonusLife { get; set; }
        public double BonusStrength { get; set; }
        public double BonusSpeed { get; set; }
    }
}
