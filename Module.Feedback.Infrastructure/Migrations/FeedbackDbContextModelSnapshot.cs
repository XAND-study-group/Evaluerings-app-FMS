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
                .HasDefaultSchema("FeedbackModule")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassIdRoom", b =>
                {
                    b.Property<Guid>("ClassIdsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClassIdsId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("ClassIdRoom", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
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

                    b.ComplexProperty<Dictionary<string, object>>("CommentText", "Module.Feedback.Domain.Entities.Comment.CommentText#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("FeedbackId");

                    b.ToTable("Comments", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("NotificationStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("HashedUserId", "Module.Feedback.Domain.Entities.Feedback.HashedUserId#HashedUserId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Problem", "Module.Feedback.Domain.Entities.Feedback.Problem#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Solution", "Module.Feedback.Domain.Entities.Feedback.Solution#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Module.Feedback.Domain.Entities.Feedback.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Feedbacks", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Module.Feedback.Domain.Entities.Room.Description#Text", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Module.Feedback.Domain.Entities.Room.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Rooms", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Vote", b =>
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

                    b.ComplexProperty<Dictionary<string, object>>("HashedUserId", "Module.Feedback.Domain.Entities.Vote.HashedUserId#HashedUserId", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.ToTable("Votes", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.WrapperObjects.ClassId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassIdValue")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("ClassId", "FeedbackModule");
                });

            modelBuilder.Entity("Module.Feedback.Domain.WrapperObjects.NotificationUserId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserIdValue")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("NotificationUserId", "FeedbackModule");
                });

            modelBuilder.Entity("NotificationUserIdRoom", b =>
                {
                    b.Property<Guid>("NotificationSubscribedUserIdsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NotificationSubscribedUserIdsId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("NotificationUserIdRoom", "FeedbackModule");
                });

            modelBuilder.Entity("ClassIdRoom", b =>
                {
                    b.HasOne("Module.Feedback.Domain.WrapperObjects.ClassId", null)
                        .WithMany()
                        .HasForeignKey("ClassIdsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Module.Feedback.Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Comment", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Entities.Comment", null)
                        .WithMany("SubComments")
                        .HasForeignKey("CommentId");

                    b.HasOne("Module.Feedback.Domain.Entities.Feedback", null)
                        .WithMany("Comments")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Entities.Room", "Room")
                        .WithMany("Feedbacks")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Vote", b =>
                {
                    b.HasOne("Module.Feedback.Domain.Entities.Feedback", null)
                        .WithMany("Votes")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotificationUserIdRoom", b =>
                {
                    b.HasOne("Module.Feedback.Domain.WrapperObjects.NotificationUserId", null)
                        .WithMany()
                        .HasForeignKey("NotificationSubscribedUserIdsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Module.Feedback.Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Comment", b =>
                {
                    b.Navigation("SubComments");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Feedback", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Module.Feedback.Domain.Entities.Room", b =>
                {
                    b.Navigation("Feedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
