namespace EFFirst.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public books()
        {
            borrows = new HashSet<borrows>();
            categories = new HashSet<categories>();
        }

        [Key]
        public int book_id { get; set; }

        [Required]
        [StringLength(200)]
        public string title { get; set; }

        [Required]
        [StringLength(100)]
        public string author { get; set; }

        [StringLength(100)]
        public string publisher { get; set; }

        public int? publish_year { get; set; }

        public int? total_count { get; set; }

        public int? available_count { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<borrows> borrows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<categories> categories { get; set; }
    }
}
