//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WLA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActivityGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActivityGroup()
        {
            this.ActivityGroupLists = new HashSet<ActivityGroupList>();
            this.ResumeGroups = new HashSet<ResumeGroup>();
            this.WLATrxes = new HashSet<WLATrx>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sarpras { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityGroupList> ActivityGroupLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResumeGroup> ResumeGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WLATrx> WLATrxes { get; set; }
    }
}
