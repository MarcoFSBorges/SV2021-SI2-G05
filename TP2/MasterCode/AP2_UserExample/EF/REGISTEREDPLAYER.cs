//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class REGISTEREDPLAYER
    {
        public int player_id { get; set; }
        public Nullable<int> login_id { get; set; }
        public Nullable<int> score { get; set; }
        public Nullable<int> level { get; set; }
        public Nullable<int> bank_balance { get; set; }
        public int life_points { get; set; }
        public int strength_points { get; set; }
        public int speed_points { get; set; }
        public Nullable<int> clan { get; set; }
    
        public virtual CLAN CLAN1 { get; set; }
        public virtual LOGIN LOGIN { get; set; }
        public virtual PLAYER PLAYER { get; set; }
    }
}
