using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RegisteredPlayer
    {
        public int PlayerID { get; set; }
        public int LoginID { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int BankBalance { get; set; }
        public int LifePoints { get; set; }
        public int StrengthPoints { get; set; }
        public int SpeedPoints { get; set; }
        public int Clan { get; set; }
    }
}
