﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School.Infrastructure.Data;

#nullable disable

namespace School.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241005101418_AddInstructorAndInstructorSubjectsTables")]
    partial class AddInstructorAndInstructorSubjectsTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("School.Data.Entities.Department", b =>
                {
                    b.Property<int>("DID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DID"));

                    b.Property<string>("DNameAr")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DNameEn")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("DID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("School.Data.Entities.DepartmetSubject", b =>
                {
                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.Property<int>("DID")
                        .HasColumnType("int");

                    b.HasKey("SubID", "DID");

                    b.HasIndex("DID");

                    b.ToTable("DepartmetSubjects");
                });

            modelBuilder.Entity("School.Data.Entities.Instructor", b =>
                {
                    b.Property<int>("InstId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DID")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmanagerId")
                        .HasColumnType("int");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("InstId");

                    b.HasIndex("DID");

                    b.HasIndex("DepartmanagerId")
                        .IsUnique()
                        .HasFilter("[DepartmanagerId] IS NOT NULL");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("School.Data.Entities.InstructorSubject", b =>
                {
                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.Property<int>("InstId")
                        .HasColumnType("int");

                    b.HasKey("SubID", "InstId");

                    b.HasIndex("InstId");

                    b.ToTable("InstructorSubjects");
                });

            modelBuilder.Entity("School.Data.Entities.Student", b =>
                {
                    b.Property<int>("StudID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("DID")
                        .HasColumnType("int");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("StudID");

                    b.HasIndex("DID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("School.Data.Entities.StudentSubject", b =>
                {
                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.Property<int>("StudID")
                        .HasColumnType("int");

                    b.HasKey("SubID", "StudID");

                    b.HasIndex("StudID");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("School.Data.Entities.Subject", b =>
                {
                    b.Property<int>("SubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubID"));

                    b.Property<DateTime>("Period")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectNameAr")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SubjectNameEn")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("SubID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("School.Data.Entities.DepartmetSubject", b =>
                {
                    b.HasOne("School.Data.Entities.Department", "Department")
                        .WithMany("DepartmentSubjects")
                        .HasForeignKey("DID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Data.Entities.Subject", "Subject")
                        .WithMany("DepartmetsSubjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("School.Data.Entities.Instructor", b =>
                {
                    b.HasOne("School.Data.Entities.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DID");

                    b.HasOne("School.Data.Entities.Department", "DepartmentManage")
                        .WithOne("InstructorManage")
                        .HasForeignKey("School.Data.Entities.Instructor", "DepartmanagerId");

                    b.HasOne("School.Data.Entities.Instructor", "Supervisor")
                        .WithMany("Instructors")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Department");

                    b.Navigation("DepartmentManage");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("School.Data.Entities.InstructorSubject", b =>
                {
                    b.HasOne("School.Data.Entities.Instructor", "Instructor")
                        .WithMany("InstructorSubjects")
                        .HasForeignKey("InstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Data.Entities.Subject", "Subject")
                        .WithMany("InstructorSubjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("School.Data.Entities.Student", b =>
                {
                    b.HasOne("School.Data.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("School.Data.Entities.StudentSubject", b =>
                {
                    b.HasOne("School.Data.Entities.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School.Data.Entities.Subject", "Subject")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("School.Data.Entities.Department", b =>
                {
                    b.Navigation("DepartmentSubjects");

                    b.Navigation("InstructorManage")
                        .IsRequired();

                    b.Navigation("Instructors");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("School.Data.Entities.Instructor", b =>
                {
                    b.Navigation("InstructorSubjects");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("School.Data.Entities.Student", b =>
                {
                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("School.Data.Entities.Subject", b =>
                {
                    b.Navigation("DepartmetsSubjects");

                    b.Navigation("InstructorSubjects");

                    b.Navigation("StudentsSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}