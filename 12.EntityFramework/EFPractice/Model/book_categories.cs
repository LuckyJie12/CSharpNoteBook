namespace EFFirst.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class book_categories
    {
        [Key]
        public int book_id { get; set; }
        public int category_id { get; set; }
    }
}
