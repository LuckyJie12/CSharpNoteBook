namespace EFFirst.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class logs
    {
        [Key]
        public int log_id { get; set; }

        public int admin_id { get; set; }

        [Required]
        [StringLength(200)]
        public string action { get; set; }

        public DateTime created_at { get; set; }

        public virtual admins admins { get; set; }
    }
}
