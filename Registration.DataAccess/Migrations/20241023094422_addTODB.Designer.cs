﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Registration.DataAccess.Data;

#nullable disable

namespace Registration.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241023094422_addTODB")]
    partial class addTODB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Registration.Models.Candidates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Candidates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Hubballi",
                            Class = "1st Puc",
                            CourseId = 1,
                            ImageUrl = "",
                            Name = "Den",
                            Status = ""
                        },
                        new
                        {
                            Id = 2,
                            Address = "Hubballi",
                            Class = "1st Puc",
                            CourseId = 2,
                            ImageUrl = "",
                            Name = "Ben",
                            Status = ""
                        },
                        new
                        {
                            Id = 3,
                            Address = "Hubballi",
                            Class = "1st Puc",
                            CourseId = 3,
                            ImageUrl = "",
                            Name = "Glen",
                            Status = ""
                        },
                        new
                        {
                            Id = 4,
                            Address = "Hubballi",
                            Class = "1st Puc",
                            CourseId = 1,
                            ImageUrl = "",
                            Name = "Dexter",
                            Status = ""
                        });
                });

            modelBuilder.Entity("Registration.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Science"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Commerce"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Arts"
                        });
                });

            modelBuilder.Entity("Registration.Models.Candidates", b =>
                {
                    b.HasOne("Registration.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });
#pragma warning restore 612, 618
        }
    }
}
