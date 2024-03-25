﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutHouse.Server.Infrastructure;

#nullable disable

namespace OutHouse.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240325154256_AddBookings")]
    partial class AddBookings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OutHouse.Server.Domain.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("End")
                        .HasColumnType("date");

                    b.Property<Guid>("OuthouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Start")
                        .HasColumnType("date");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OuthouseId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5990aea7-1c7b-48b1-8d18-de00bf98a7b5"),
                            BookerEmail = "member@outhouse.com",
                            End = new DateOnly(2000, 1, 4),
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Start = new DateOnly(2000, 1, 3),
                            State = 0
                        },
                        new
                        {
                            Id = new Guid("9467a1f8-56be-447e-8e96-f23510b5d7ab"),
                            BookerEmail = "member@outhouse.com",
                            End = new DateOnly(2000, 1, 2),
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Start = new DateOnly(2000, 1, 1),
                            State = 1
                        },
                        new
                        {
                            Id = new Guid("4a241119-e25c-4d5a-9a2f-98af68abe9a3"),
                            BookerEmail = "member@outhouse.com",
                            End = new DateOnly(2000, 1, 2),
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Start = new DateOnly(2000, 1, 1),
                            State = 2
                        },
                        new
                        {
                            Id = new Guid("9628c05c-8b3c-41ca-9d53-9b111217133e"),
                            BookerEmail = "admin@outhouse.com",
                            End = new DateOnly(2000, 1, 2),
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Start = new DateOnly(2000, 1, 1),
                            State = 3
                        });
                });

            modelBuilder.Entity("OutHouse.Server.Domain.Members.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OuthouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OuthouseId");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = new Guid("54f1504a-5e01-4d89-9446-4639762e13cc"),
                            Email = "owner@outhouse.com",
                            Name = "Owner",
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Role = 0
                        },
                        new
                        {
                            Id = new Guid("681198e0-2671-455e-a759-e532916f50dc"),
                            Email = "admin@outhouse.com",
                            Name = "Admin",
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Role = 1
                        },
                        new
                        {
                            Id = new Guid("275e4646-2730-4656-9fe6-9ff80069cb1b"),
                            Email = "member@outhouse.com",
                            Name = "Member",
                            OuthouseId = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Role = 2
                        });
                });

            modelBuilder.Entity("OutHouse.Server.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ce28d4eb-f953-45b4-bc8c-41e60727d621",
                            Email = "owner@outhouse.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "OWNER@OUTHOUSE.COM",
                            NormalizedUserName = "OWNER@OUTHOUSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEA7koEsEHkRfALbmLBFJroYgFXSUhJPSNS+4JndjPKYslImX/2DW05xbTuIzYssXFw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "35a6c895-a2b9-4353-a9db-e0b7bf0816cf",
                            TwoFactorEnabled = false,
                            UserName = "owner@outhouse.com"
                        },
                        new
                        {
                            Id = new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fedba227-2766-448a-9604-fc8d893215b7",
                            Email = "admin@outhouse.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@OUTHOUSE.COM",
                            NormalizedUserName = "ADMIN@OUTHOUSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHZewWWiV6qAWWsI3q9JG3+Sq5f1taKsl+qdpCcVuEJ5BErzBs+PykpOcxRS77hETQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cedb285f-cc71-4ffe-a1fe-21847c131aae",
                            TwoFactorEnabled = false,
                            UserName = "admin@outhouse.com"
                        },
                        new
                        {
                            Id = new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4d1a157b-78a2-4899-92ab-26fd2bfe80d3",
                            Email = "member@outhouse.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MEMBER@OUTHOUSE.COM",
                            NormalizedUserName = "MEMBER@OUTHOUSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEIZvj6WMGfTuBrzqv7N9EumCuoEkRPrcihIts3gZrhoKjAEgX35Tr98sQSndaDPI+A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "978213ec-e24b-47ad-900c-c08a759bc25c",
                            TwoFactorEnabled = false,
                            UserName = "member@outhouse.com"
                        },
                        new
                        {
                            Id = new Guid("a6050009-75b2-48a0-a591-5759f3065af3"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "137a20b6-ae7d-4fea-ac7a-077e8f24d895",
                            Email = "guest@outhouse.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GUEST@OUTHOUSE.COM",
                            NormalizedUserName = "GUEST@OUTHOUSE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOdCYOZPJdGaOL+uWVM1PV/0w2wsCdr5oUARJj+JzxFZ3MOA3GOOKhpeewe32d/Jyg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9ecbc49e-438b-45a0-b804-594cd8822bf3",
                            TwoFactorEnabled = false,
                            UserName = "guest@outhouse.com"
                        });
                });

            modelBuilder.Entity("OutHouse.Server.Models.Outhouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Outhouses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("acdd236c-e699-434b-9024-48e614b1ae58"),
                            Name = "My Outhouse"
                        },
                        new
                        {
                            Id = new Guid("008f84df-f856-4a4d-b92b-314cdecd6fab"),
                            Name = "Orphan Outhouse"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("OutHouse.Server.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("OutHouse.Server.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutHouse.Server.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("OutHouse.Server.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OutHouse.Server.Domain.Bookings.Booking", b =>
                {
                    b.HasOne("OutHouse.Server.Models.Outhouse", "Outhouse")
                        .WithMany("Bookings")
                        .HasForeignKey("OuthouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outhouse");
                });

            modelBuilder.Entity("OutHouse.Server.Domain.Members.Member", b =>
                {
                    b.HasOne("OutHouse.Server.Models.Outhouse", "Outhouse")
                        .WithMany("Members")
                        .HasForeignKey("OuthouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outhouse");
                });

            modelBuilder.Entity("OutHouse.Server.Models.Outhouse", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
