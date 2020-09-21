﻿// <auto-generated />
using System;
using LCMSMSWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LCMSMSWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
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

                    b.Property<string>("School")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("AcademicID");

                    b.HasIndex("OrphanID");

                    b.ToTable("Academics");
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

            modelBuilder.Entity("LCMSMSWebApi.Models.Guardian", b =>
                {
                    b.Property<int>("GuardianID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("GuardianID");

                    b.ToTable("Guardians");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Narration", b =>
                {
                    b.Property<int>("NarrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("NarrationID");

                    b.HasIndex("OrphanID");

                    b.ToTable("Narrations");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Orphan", b =>
                {
                    b.Property<int>("OrphanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ProfileNumber")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int?>("ProfilePictureID")
                        .HasColumnType("int");

                    b.HasKey("OrphanID");

                    b.HasIndex("GuardianID");

                    b.ToTable("Orphans");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.OrphanProfilePic", b =>
                {
                    b.Property<int>("OrphanProfilePicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("PicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrphanProfilePicID");

                    b.ToTable("OrphanProfilePics");
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

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("PictureFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PictureID");

                    b.HasIndex("OrphanID");

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

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MainPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("SponsorID");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Academic", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", null)
                        .WithMany("Academics")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Narration", b =>
                {
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

            modelBuilder.Entity("LCMSMSWebApi.Models.Picture", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", "Orphan")
                        .WithMany("Pictures")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
