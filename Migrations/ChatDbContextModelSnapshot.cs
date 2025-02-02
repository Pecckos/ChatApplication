﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PecckosChatProgram.Data;

#nullable disable

namespace PecckosChatProgram.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    partial class ChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PecckosChatProgram.Models.ChatMessage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Message = "Welcome to Pecckos chat",
                            TimeStamp = new DateTime(2024, 1, 31, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PecckosChatProgram.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            PasswordHash = "$2a$11$q2cYg.eqK.4hzoEsGbXt1.rKOVjNiiJZTDWD0oNs4hsZ0QoghL7oO",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("PecckosChatProgram.Models.ChatMessage", b =>
                {
                    b.HasOne("PecckosChatProgram.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PecckosChatProgram.Models.User", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
