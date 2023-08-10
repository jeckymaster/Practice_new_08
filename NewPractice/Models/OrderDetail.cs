namespace NewPractice.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        public int OrderDetailsNo { get; set; }

        public int? OrderNo { get; set; }

        public int? ItemCode { get; set; }

        public int? ItemQty { get; set; }

        public decimal? ItemRate { get; set; }

        public decimal? TotalAmt { get; set; }
    }
}
