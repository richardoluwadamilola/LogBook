﻿// <auto-generated />
using System;
using DigiLog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigiLog.Migrations
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20240131144836_TestMigration")]
    partial class TestMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DigiLog.Models.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DigiLog.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("EmployeeNumber");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DigiLog.Models.Photo", b =>
                {
                    b.Property<long>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<byte[]>("PhotoData")
                        .HasColumnType("longblob");

                    b.Property<long>("VisitorId")
                        .HasColumnType("bigint");

                    b.HasKey("PhotoId");

                    b.HasIndex("VisitorId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DigiLog.Models.Tag", b =>
                {
                    b.Property<string>("TagNumber")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TagNumber");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DigiLog.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigiLog.Models.Visitor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ContactAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReasonForVisit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReasonForVisitDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TagNumber")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeNumber");

                    b.HasIndex("TagNumber");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("DigiLog.Models.Employee", b =>
                {
                    b.HasOne("DigiLog.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DigiLog.Models.Photo", b =>
                {
                    b.HasOne("DigiLog.Models.Visitor", "Visitor")
                        .WithOne("Photo")
                        .HasForeignKey("DigiLog.Models.Photo", "VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("DigiLog.Models.Visitor", b =>
                {
                    b.HasOne("DigiLog.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigiLog.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagNumber");

                    b.Navigation("Employee");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DigiLog.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DigiLog.Models.Visitor", b =>
                {
                    b.Navigation("Photo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
