namespace EFFirst.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class borrows
    {
        [Key]
        public int borrow_id { get; set; }

        public int reader_id { get; set; }

        public int book_id { get; set; }

        public DateTime borrow_date { get; set; }

        public DateTime? return_date { get; set; }

        public bool? is_returned { get; set; }

        public virtual books books { get; set; }

        public virtual readers readers { get; set; }
    }
}
