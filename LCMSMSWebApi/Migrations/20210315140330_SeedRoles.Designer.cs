﻿// <auto-generated />
using System;
using LCMSMSWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LCMSMSWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210315140330_SeedRoles")]
    partial class SeedRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LCMSMSWebApi.Models.Academic", b =>
                {
                    b.Property<int>("AcademicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("KCPE")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("KCSE")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("PostKCSENotes")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("School")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("AcademicID");

                    b.HasIndex("OrphanID");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.DbUpdate", b =>
                {
                    b.Property<int>("DbUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AcademicsUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GuardiansUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NarrationsUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrphansUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PicturesUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SponsorsUpdateTimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("DbUpdateId");

                    b.ToTable("DbUpdates");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Document", b =>
                {
                    b.Property<int>("DocumentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllSponsors")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<int?>("SponsorID")
                        .HasColumnType("int");

                    b.HasKey("DocumentID");

                    b.HasIndex("OrphanID");

                    b.HasIndex("SponsorID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Guardian", b =>
                {
                    b.Property<int>("GuardianID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AltPhone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltPhone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltPhone3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("MainPhone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GuardianID");

                    b.ToTable("Guardians");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Narration", b =>
                {
                    b.Property<int>("NarrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ApprovedByID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GuardianID")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int?>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubmittedByID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NarrationID");

                    b.HasIndex("GuardianID");

                    b.HasIndex("OrphanID");

                    b.ToTable("Narrations");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Orphan", b =>
                {
                    b.Property<int>("OrphanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int?>("GuardianID")
                        .HasColumnType("int");

                    b.Property<string>("LCMStatus")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ProfileNumber")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("ProfilePicFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelationshipToGuardian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("YearOfAdmission")
                        .HasColumnType("datetime2");

                    b.HasKey("OrphanID");

                    b.HasIndex("GuardianID");

                    b.ToTable("Orphans");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.OrphanPicture", b =>
                {
                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<int>("PictureID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrphanID", "PictureID");

                    b.HasIndex("PictureID");

                    b.ToTable("OrphanPictures");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.OrphanSponsor", b =>
                {
                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<int>("SponsorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrphanID", "SponsorID");

                    b.HasIndex("SponsorID");

                    b.ToTable("OrphanSponsors");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Picture", b =>
                {
                    b.Property<int>("PictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PictureFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TakenDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PictureID");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Sponsor", b =>
                {
                    b.Property<int>("SponsorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastDonationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MainPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("SponsorID");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Academic", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", null)
                        .WithMany("Academics")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Document", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", "Orphan")
                        .WithMany("Documents")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LCMSMSWebApi.Models.Sponsor", "Sponsor")
                        .WithMany("Documents")
                        .HasForeignKey("SponsorID");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Narration", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Guardian", null)
                        .WithMany("Narrations")
                        .HasForeignKey("GuardianID");

                    b.HasOne("LCMSMSWebApi.Models.Orphan", null)
                        .WithMany("Narrations")
                        .HasForeignKey("OrphanID");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Orphan", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Guardian", "Guardian")
                        .WithMany("Orphans")
                        .HasForeignKey("GuardianID");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.OrphanPicture", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", "Orphan")
                        .WithMany("OrphanPictures")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LCMSMSWebApi.Models.Picture", "Picture")
                        .WithMany("OrphanPictures")
                        .HasForeignKey("PictureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.OrphanSponsor", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", "Orphan")
                        .WithMany("OrphanSponsors")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LCMSMSWebApi.Models.Sponsor", "Sponsor")
                        .WithMany("OrphanSponsors")
                        .HasForeignKey("SponsorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LCMSMSWebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
