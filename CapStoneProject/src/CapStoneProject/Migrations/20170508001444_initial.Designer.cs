using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CapStoneProject.Repositories;

namespace CapStoneProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170508001444_initial")]
    partial class initial
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

                    b.Property<string>("UserId");

                    b.HasKey("BidID");

                    b.HasIndex("BidRequestID");

                    b.HasIndex("UserId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("CapStoneProject.Models.BidRequest", b =>
                {
                    b.Property<int>("BidRequestID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Concrete");

                    b.Property<bool>("FrameWork");

                    b.Property<bool>("NewBuild");

                    b.Property<string>("ProjectDescription");

                    b.Property<string>("ProjectLocation");

                    b.Property<bool>("Remodel");

                    b.Property<string>("UserId");

                    b.HasKey("BidRequestID");

                    b.HasIndex("UserId");

                    b.ToTable("BidRequests");
                });

            modelBuilder.Entity("CapStoneProject.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("UserIdentityId");

                    b.Property<string>("Zipcode");

                    b.HasKey("ClientID");

                    b.HasIndex("UserIdentityId");

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

                    b.Property<string>("FromId");

                    b.Property<string>("Subject");

                    b.HasKey("ReviewID");

                    b.HasIndex("FromId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CapStoneProject.Models.UserIdentity", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Password");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CapStoneProject.Models.Bid", b =>
                {
                    b.HasOne("CapStoneProject.Models.BidRequest", "BidReq")
                        .WithMany()
                        .HasForeignKey("BidRequestID");

                    b.HasOne("CapStoneProject.Models.UserIdentity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CapStoneProject.Models.BidRequest", b =>
                {
                    b.HasOne("CapStoneProject.Models.UserIdentity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CapStoneProject.Models.Client", b =>
                {
                    b.HasOne("CapStoneProject.Models.UserIdentity", "UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserIdentityId");
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
                    b.HasOne("CapStoneProject.Models.UserIdentity", "From")
                        .WithMany()
                        .HasForeignKey("FromId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CapStoneProject.Models.UserIdentity")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CapStoneProject.Models.UserIdentity")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CapStoneProject.Models.UserIdentity")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
