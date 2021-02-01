﻿// <auto-generated />
using System;
using CodeFactoryAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeFactoryAPI.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CodeFactoryAPI.Models.Question", b =>
                {
                    b.Property<Guid>("Question_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AskedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("User_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Question_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Reply", b =>
                {
                    b.Property<Guid>("Reply_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Question_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Reply_ID");

                    b.HasIndex("Question_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Tag", b =>
                {
                    b.Property<int>("Tag_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Tag_ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.User", b =>
                {
                    b.Property<Guid>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("User_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.UsersTags", b =>
                {
                    b.Property<Guid>("Question_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tag_ID")
                        .HasColumnType("int");

                    b.Property<Guid>("Question_ID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tag_ID1")
                        .HasColumnType("int");

                    b.HasKey("Question_ID", "Tag_ID");

                    b.HasIndex("Question_ID1");

                    b.HasIndex("Tag_ID1");

                    b.ToTable("UsersTags");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Question", b =>
                {
                    b.HasOne("CodeFactoryAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Reply", b =>
                {
                    b.HasOne("CodeFactoryAPI.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("Question_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeFactoryAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.UsersTags", b =>
                {
                    b.HasOne("CodeFactoryAPI.Models.Question", "Question")
                        .WithMany("UsersTags")
                        .HasForeignKey("Question_ID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeFactoryAPI.Models.Tag", "Tag")
                        .WithMany("UsersTags")
                        .HasForeignKey("Tag_ID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Question", b =>
                {
                    b.Navigation("UsersTags");
                });

            modelBuilder.Entity("CodeFactoryAPI.Models.Tag", b =>
                {
                    b.Navigation("UsersTags");
                });
#pragma warning restore 612, 618
        }
    }
}
