﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module.User.Infrastructure.DbContext;

#nullable disable

namespace Module.User.Infrastructure.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Module.User.Domain.Entities.AccountClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaimName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountClaims");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.AccountLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountLogins");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("School")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SemesterUser", b =>
                {
                    b.Property<Guid>("SemesterResponsiblesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SemestersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SemesterResponsiblesId", "SemestersId");

                    b.HasIndex("SemestersId");

                    b.ToTable("SemesterUser");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.AccountClaim", b =>
                {
                    b.HasOne("Module.User.Domain.Entities.User", null)
                        .WithMany("AccountClaims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.AccountLogin", b =>
                {
                    b.HasOne("Module.User.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Module.User.Domain.Entities.Semester", b =>
                {
                    b.OwnsOne("SharedKernel.ValueObjects.EducationRange", "EducationRange", b1 =>
                        {
                            b1.Property<Guid>("SemesterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateOnly>("End")
                                .HasColumnType("date");

                            b1.Property<DateOnly>("Start")
                                .HasColumnType("date");

                            b1.HasKey("SemesterId");

                            b1.ToTable("Semesters");

                            b1.WithOwner()
                                .HasForeignKey("SemesterId");
                        });

                    b.OwnsOne("SharedKernel.ValueObjects.SemesterNumber", "SemesterNumber", b1 =>
                        {
                            b1.Property<Guid>("SemesterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("SemesterId");

                            b1.ToTable("Semesters");

                            b1.WithOwner()
                                .HasForeignKey("SemesterId");
                        });

                    b.Navigation("EducationRange")
                        .IsRequired();

                    b.Navigation("SemesterNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("SemesterUser", b =>
                {
                    b.HasOne("Module.User.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("SemesterResponsiblesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Module.User.Domain.Entities.Semester", null)
                        .WithMany()
                        .HasForeignKey("SemestersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Module.User.Domain.Entities.User", b =>
                {
                    b.Navigation("AccountClaims");
                });
#pragma warning restore 612, 618
        }
    }
}