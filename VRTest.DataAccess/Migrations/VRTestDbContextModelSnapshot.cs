﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VRTest.DataAccess;

namespace VRTest.DataAccess.Migrations
{
    [DbContext(typeof(VRTestDbContext))]
    partial class VRTestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VRTest.Entities.NPVPreviousRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CashFlowsDescription")
                        .IsRequired();

                    b.Property<double>("IncrementRate");

                    b.Property<double>("InitialCost");

                    b.Property<double>("LowerBoundDiscountRate");

                    b.Property<double>("UpperBoundDiscountRate");

                    b.HasKey("Id");

                    b.HasIndex("IncrementRate", "InitialCost", "LowerBoundDiscountRate", "UpperBoundDiscountRate");

                    b.ToTable("NPVPreviousRequests");
                });

            modelBuilder.Entity("VRTest.Entities.NPVPreviousResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("DiscountRate");

                    b.Property<double>("NPV");

                    b.Property<int>("NPVPreviousRequestId");

                    b.HasKey("Id");

                    b.HasIndex("NPVPreviousRequestId");

                    b.ToTable("NPVPreviousResult");
                });

            modelBuilder.Entity("VRTest.Entities.NPVPreviousResult", b =>
                {
                    b.HasOne("VRTest.Entities.NPVPreviousRequest", "NPVPreviousRequest")
                        .WithMany("NPVPreviousResults")
                        .HasForeignKey("NPVPreviousRequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
