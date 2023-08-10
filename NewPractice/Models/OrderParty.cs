namespace NewPractice.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderParty
    {
        [Key]
        public int PartyCode { get; set; }

        [StringLength(50)]
        public string PartyName { get; set; }
    }
}
