using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NewPractice.Models
{
    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<ItemMaster> ItemMasters { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderMaster> OrderMasters { get; set; }
        public virtual DbSet<OrderParty> OrderParties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<ItemMaster>()
                .Property(e => e.ItemCategory)
                .IsUnicode(false);

            modelBuilder.Entity<OrderParty>()
                .Property(e => e.PartyName)
                .IsUnicode(false);
        }
    }
}
