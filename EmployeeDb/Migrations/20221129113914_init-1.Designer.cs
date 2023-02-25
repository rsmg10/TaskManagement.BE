﻿// <auto-generated />
using System;
using MITT.EmployeeDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MITT.EmployeeDb.Migrations
{
    [DbContext(typeof(ManagementDb))]
    [Migration("20221129113914_init-1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("Sequence-Generator", "SequenceGenerator");

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedBeTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DevTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TaskState")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DevTaskId" }, "IX_AssignedBETasks_DevTaskId");

                    b.HasIndex(new[] { "DeveloperId" }, "IX_AssignedBETasks_DeveloperId");

                    b.ToTable("AssignedBETasks", (string)null);
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedManager", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProjectManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProjectId" }, "IX_AssignedManagers_ProjectId");

                    b.HasIndex(new[] { "ProjectManagerId" }, "IX_AssignedManagers_ProjectManagerId");

                    b.ToTable("AssignedManagers");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedQaTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DevTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TaskState")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DevTaskId" }, "IX_AssignedQATasks_DevTaskId");

                    b.HasIndex(new[] { "DeveloperId" }, "IX_AssignedQATasks_DeveloperId");

                    b.ToTable("AssignedQATasks", (string)null);
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.BeReview", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedBeTaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AssignedBETaskId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Findings")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AssignedBeTaskId" }, "IX_BEReviews_AssignedBETaskId");

                    b.ToTable("BEReviews", (string)null);
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.DevTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompletionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ImplementationType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeqNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskState")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AssignedManagerId" }, "IX_Tasks_AssignedManagerId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Developer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdentityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdentityId" }, "IX_Developers_IdentityId");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdentityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdentityId" }, "IX_Managers_IdentityId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.QaReview", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedQaTaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AssignedQATaskId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Findings")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AssignedQaTaskId" }, "IX_QAReviews_AssignedQATaskId");

                    b.ToTable("QAReviews", (string)null);
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedBeTask", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.DevTask", "DevTask")
                        .WithMany("AssignedBetasks")
                        .HasForeignKey("DevTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MITT.EmployeeDb.Models.Developer", "Developer")
                        .WithMany("AssignedBetasks")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DevTask");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedManager", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.Project", "Project")
                        .WithMany("AssignedManagers")
                        .HasForeignKey("ProjectId");

                    b.HasOne("MITT.EmployeeDb.Models.Manager", "ProjectManager")
                        .WithMany("AssignedManagers")
                        .HasForeignKey("ProjectManagerId");

                    b.Navigation("Project");

                    b.Navigation("ProjectManager");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedQaTask", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.DevTask", "DevTask")
                        .WithMany("AssignedQatasks")
                        .HasForeignKey("DevTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MITT.EmployeeDb.Models.Developer", "Developer")
                        .WithMany("AssignedQatasks")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DevTask");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.BeReview", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.AssignedBeTask", "AssignedBeTask")
                        .WithMany("BeReviews")
                        .HasForeignKey("AssignedBeTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedBeTask");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.DevTask", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.AssignedManager", "AssignedManager")
                        .WithMany("Tasks")
                        .HasForeignKey("AssignedManagerId");

                    b.Navigation("AssignedManager");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Developer", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.Identity", "Identity")
                        .WithMany("Developers")
                        .HasForeignKey("IdentityId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Manager", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.Identity", "Identity")
                        .WithMany("Managers")
                        .HasForeignKey("IdentityId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.QaReview", b =>
                {
                    b.HasOne("MITT.EmployeeDb.Models.AssignedQaTask", "AssignedQaTask")
                        .WithMany("QaReviews")
                        .HasForeignKey("AssignedQaTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedQaTask");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedBeTask", b =>
                {
                    b.Navigation("BeReviews");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedManager", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.AssignedQaTask", b =>
                {
                    b.Navigation("QaReviews");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.DevTask", b =>
                {
                    b.Navigation("AssignedBetasks");

                    b.Navigation("AssignedQatasks");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Developer", b =>
                {
                    b.Navigation("AssignedBetasks");

                    b.Navigation("AssignedQatasks");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Identity", b =>
                {
                    b.Navigation("Developers");

                    b.Navigation("Managers");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Manager", b =>
                {
                    b.Navigation("AssignedManagers");
                });

            modelBuilder.Entity("MITT.EmployeeDb.Models.Project", b =>
                {
                    b.Navigation("AssignedManagers");
                });
#pragma warning restore 612, 618
        }
    }
}
