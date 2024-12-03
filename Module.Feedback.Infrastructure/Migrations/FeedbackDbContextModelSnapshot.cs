﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module.Feedback.Infrastructure.DbContexts;

#nullable disable

namespace Module.Feedback.Infrastructure.Migrations
{
    [DbContext(typeof(FeedbackDbContext))]
    partial class FeedbackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Module.Feedback.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FeedbackId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("CommentText", "Module.Feedback.Domain.Comment.CommentText#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("FeedbackId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("HashedUserId", "Module.Feedback.Domain.Feedback.HashedUserId#HashedUserId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Problem", "Module.Feedback.Domain.Feedback.Problem#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Solution", "Module.Feedback.Domain.Feedback.Solution#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Module.Feedback.Domain.Feedback.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Module.Feedback.Domain.Room.Description#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Module.Feedback.Domain.Room.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FeedbackId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("VoteScale")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("HashedUserId", "Module.Feedback.Domain.Vote.HashedUserId#HashedUserId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Comment", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Comment", null)
                        .WithMany("SubComments")
                        .HasForeignKey("CommentId");

                    b.HasOne("Module.Feedback.Domain.Feedback", null)
                        .WithMany("Comments")
                        .HasForeignKey("FeedbackId");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Feedback", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Vote", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Feedback", null)
                        .WithMany("Votes")
                        .HasForeignKey("FeedbackId");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Comment", b =>
                {
                    b.Navigation("SubComments");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Feedback", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
