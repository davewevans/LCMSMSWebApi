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
    [Migration("20200710204904_Added1ToManyRelationships")]
    partial class Added1ToManyRelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KCPE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KCSE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("School")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademicID");

                    b.ToTable("Academics");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.DbUpdate", b =>
                {
                    b.Property<int>("DbUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeStamp")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
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

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrphanID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GuardianID")
                        .HasColumnType("int");

                    b.Property<string>("LCMStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileNumber")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("PictureUri")
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SponsorID");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Narration", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Orphan", null)
                        .WithMany("Narrations")
                        .HasForeignKey("OrphanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LCMSMSWebApi.Models.Orphan", b =>
                {
                    b.HasOne("LCMSMSWebApi.Models.Guardian", null)
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
#pragma warning restore 612, 618
        }
    }
}
