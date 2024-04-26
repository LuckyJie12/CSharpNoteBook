namespace EFFirst.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class admins
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admins()
        {
            logs = new HashSet<logs>();
        }

        [Key]
        public int admin_id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        public bool? is_superadmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logs> logs { get; set; }
    }
}
