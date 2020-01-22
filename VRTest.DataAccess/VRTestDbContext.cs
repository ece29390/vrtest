using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using VRTest.Entities;

namespace VRTest.DataAccess
{
    public class VRTestDbContext:DbContext
    {
        public VRTestDbContext(DbContextOptions options) : base(options) { }
        public DbSet<NPVPreviousRequest> NPVPreviousRequests { get; set; }
        public DbSet<NPVPreviousResult> NPVPreviousResult { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NPVPreviousRequest>((builder) =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.IncrementRate).IsRequired();
                builder.Property(p => p.InitialCost).IsRequired();
                builder.Property(p => p.LowerBoundDiscountRate).IsRequired();
                builder.Property(p => p.UpperBoundDiscountRate).IsRequired();
                builder.Property(p => p.CashFlowsDescription).IsRequired();
                builder.HasIndex(p => new { p.IncrementRate, p.InitialCost, p.LowerBoundDiscountRate, p.UpperBoundDiscountRate });
                builder.HasMany(p => p.NPVPreviousResults)
                .WithOne(r => r.NPVPreviousRequest)
                .HasForeignKey(r => r.NPVPreviousRequestId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });

            modelBuilder.Entity<NPVPreviousResult>((builder) =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.NPV).IsRequired();
               
            });
              



        }


    }
}
