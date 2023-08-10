namespace NewPractice.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderMaster
    {
        [Key]
        public int OrderNo { get; set; }

        public DateTime? OrderDate { get; set; }

        public int PartyCode { get; set; }

        public DateTime? EntryDate { get; set; }

        [ForeignKey("OrderNo")]
        public virtual List<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string ObjectOrderDetails { get; set; }
        [NotMapped]
        public string ObjectOrderMaster { get; set; }
    }
}
