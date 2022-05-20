﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotificationBot.DataAccess;

#nullable disable

namespace NotificationBot.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220520150739_MadeSleepTimeColumnsNullable")]
    partial class MadeSleepTimeColumnsNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CryptoAssetUser", b =>
                {
                    b.Property<long>("CryptoAssetsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("CryptoAssetsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersCryptoAssets", (string)null);
                });

            modelBuilder.Entity("NotificationBot.DataAccess.Entities.CryptoAsset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoinGeckoAbbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CryptoAssets");
                });

            modelBuilder.Entity("NotificationBot.DataAccess.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long?>("TelegramUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NotificationBot.DataAccess.Entities.UserSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<TimeSpan?>("SleepTimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("SleepTimeStart")
                        .HasColumnType("time");

                    b.Property<bool>("UsePeriodicNotifications")
                        .HasColumnType("bit");

                    b.Property<bool>("UseSleepHours")
                        .HasColumnType("bit");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("CryptoAssetUser", b =>
                {
                    b.HasOne("NotificationBot.DataAccess.Entities.CryptoAsset", null)
                        .WithMany()
                        .HasForeignKey("CryptoAssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotificationBot.DataAccess.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationBot.DataAccess.Entities.UserSettings", b =>
                {
                    b.HasOne("NotificationBot.DataAccess.Entities.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("NotificationBot.DataAccess.Entities.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotificationBot.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Settings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}