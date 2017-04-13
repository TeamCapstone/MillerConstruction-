using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CapStoneProject.Repositories;

namespace CapStoneProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170413205222_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CapStoneProject.Models.Bid", b =>
                {
                    b.Property<int>("BidID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BidRequestID");

                    b.Property<decimal>("LaborCost");

                    b.Property<string>("MaterialsDescription");

                    b.Property<string>("ProjectedTimeFrame");

                    b.Property<DateTime>("ProposedStartDate");

                    b.Property<decimal>("SupplyCost");

                    b.Property<decimal>("TotalEstimate");

                    b.Property<int?>("UserID");

                    b.HasKey("BidID");

                    b.HasIndex("BidRequestID");

                    b.HasIndex("UserID");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("CapStoneProject.Models.BidRequest", b =>
                {
                    b.Property<int>("BidRequestID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Concrete");

                    b.Property<bool?>("FrameWork");

                    b.Property<string>("IdealTimeFrame");

                    b.Property<bool?>("NewBuild");

                    b.Property<string>("ProjectDescription");

                    b.Property<string>("ProjectLocation");

                    b.Property<bool?>("Remodel");

                    b.Property<DateTime>("RequestedStartDate");

                    b.Property<int?>("UserID")
                        .IsRequired();

                    b.HasKey("BidRequestID");

                    b.HasIndex("UserID");

                    b.ToTable("BidRequests");
                });

            modelBuilder.Entity("CapStoneProject.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BidID");

                    b.Property<string>("City");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Street");

                    b.Property<string>("Zipcode");

                    b.HasKey("ClientID");

                    b.HasIndex("BidID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CapStoneProject.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ReviewID");

                    b.Property<string>("Subject");

                    b.HasKey("CommentID");

                    b.HasIndex("ReviewID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CapStoneProject.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientID");

                    b.Property<int?>("ProjectID");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("InvoiceID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("CapStoneProject.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AdditionalCosts");

                    b.Property<int?>("BidID")
                        .IsRequired();

                    b.Property<int?>("ClientID")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<decimal>("OriginalEstimate");

                    b.Property<string>("ProjectName")
                        .IsRequired();

                    b.Property<string>("ProjectStatus");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StatusDate");

                    b.Property<decimal>("TotalCost");

                    b.HasKey("ProjectID");

                    b.HasIndex("BidID");

                    b.HasIndex("ClientID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CapStoneProject.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<string>("Body");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("FromUserID");

                    b.Property<string>("Subject");

                    b.HasKey("ReviewID");

                    b.HasIndex("FromUserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CapStoneProject.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientID");

                    b.Property<int?>("ProjectID");

                    b.HasKey("UserID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.HasIndex("ProjectID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CapStoneProject.Models.Bid", b =>
                {
                    b.HasOne("CapStoneProject.Models.BidRequest", "BidReq")
                        .WithMany()
                        .HasForeignKey("BidRequestID");

                    b.HasOne("CapStoneProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("CapStoneProject.Models.BidRequest", b =>
                {
                    b.HasOne("CapStoneProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CapStoneProject.Models.Client", b =>
                {
                    b.HasOne("CapStoneProject.Models.Bid", "Bid")
                        .WithMany()
                        .HasForeignKey("BidID");
                });

            modelBuilder.Entity("CapStoneProject.Models.Comment", b =>
                {
                    b.HasOne("CapStoneProject.Models.Review")
                        .WithMany("Comments")
                        .HasForeignKey("ReviewID");
                });

            modelBuilder.Entity("CapStoneProject.Models.Invoice", b =>
                {
                    b.HasOne("CapStoneProject.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID");

                    b.HasOne("CapStoneProject.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("CapStoneProject.Models.Project", b =>
                {
                    b.HasOne("CapStoneProject.Models.Bid", "Bid")
                        .WithMany()
                        .HasForeignKey("BidID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CapStoneProject.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CapStoneProject.Models.Review", b =>
                {
                    b.HasOne("CapStoneProject.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("FromUserID");
                });

            modelBuilder.Entity("CapStoneProject.Models.User", b =>
                {
                    b.HasOne("CapStoneProject.Models.Client", "Client")
                        .WithOne("User")
                        .HasForeignKey("CapStoneProject.Models.User", "ClientID");

                    b.HasOne("CapStoneProject.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");
                });
        }
    }
}
