﻿// <auto-generated />
using System;
using Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220114161720_Init migration")]
    partial class Initmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Application.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Application.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Application.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectForeignKey")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectForeignKey")
                        .IsUnique()
                        .HasFilter("[ProjectForeignKey] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Application.Entities.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("Application.Entities.TechnologyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TechnologyCategories");
                });

            modelBuilder.Entity("EmployeeTechnology", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("TechnologiesId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesId", "TechnologiesId");

                    b.HasIndex("TechnologiesId");

                    b.ToTable("EmployeeTechnology");
                });

            modelBuilder.Entity("ProjectTechnology", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TechnologiesId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "TechnologiesId");

                    b.HasIndex("TechnologiesId");

                    b.ToTable("ProjectTechnology");
                });

            modelBuilder.Entity("Application.Entities.Employee", b =>
                {
                    b.HasOne("Application.Entities.Team", "Team")
                        .WithMany("Employees")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Application.Entities.Team", b =>
                {
                    b.HasOne("Application.Entities.Project", "Project")
                        .WithOne("Team")
                        .HasForeignKey("Application.Entities.Team", "ProjectForeignKey");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Application.Entities.Technology", b =>
                {
                    b.HasOne("Application.Entities.TechnologyCategory", "Category")
                        .WithMany("Technologies")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EmployeeTechnology", b =>
                {
                    b.HasOne("Application.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectTechnology", b =>
                {
                    b.HasOne("Application.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.Project", b =>
                {
                    b.Navigation("Team");
                });

            modelBuilder.Entity("Application.Entities.Team", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Application.Entities.TechnologyCategory", b =>
                {
                    b.Navigation("Technologies");
                });
#pragma warning restore 612, 618
        }
    }
}