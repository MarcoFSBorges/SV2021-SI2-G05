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
    
    public partial class TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE()
        {
            this.MATCH = new HashSet<MATCH>();
        }
    
        public int type_id { get; set; }
        public string description { get; set; }
        public Nullable<int> min_life_points { get; set; }
        public Nullable<int> min_strength_points { get; set; }
        public Nullable<int> min_speed_points { get; set; }
        public Nullable<int> score { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATCH> MATCH { get; set; }
    }
}