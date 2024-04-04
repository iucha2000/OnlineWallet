﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineWallet.Infrastructure.Persistence;

#nullable disable

namespace OnlineWallet.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineWallet.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReceiverWalletCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SenderWalletCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OnlineWallet.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("88e2537d-59fa-4ea4-88a0-4da67160f69f"),
                            Email = "imegr@gmail.com",
                            FirstName = "Iuri",
                            LastName = "Megreladze",
                            PasswordHash = new byte[] { 109, 121, 57, 115, 143, 50, 80, 191, 68, 51, 73, 37, 72, 237, 209, 154, 233, 59, 114, 0, 27, 196, 148, 250, 198, 104, 248, 179, 197, 255, 77, 16, 31, 22, 211, 124, 163, 128, 201, 200, 102, 74, 181, 152, 242, 197, 250, 254, 240, 94, 150, 213, 131, 131, 19, 185, 7, 251, 159, 237, 26, 254, 204, 198 },
                            PasswordSalt = new byte[] { 154, 190, 57, 53, 86, 179, 18, 36, 205, 9, 198, 251, 159, 239, 0, 23, 152, 118, 207, 0, 62, 157, 127, 207, 13, 189, 13, 20, 220, 4, 205, 188, 254, 235, 218, 234, 18, 100, 6, 26, 233, 212, 83, 189, 54, 113, 223, 80, 3, 101, 14, 180, 56, 43, 183, 102, 101, 148, 144, 16, 200, 156, 172, 19, 231, 93, 30, 37, 232, 143, 90, 228, 249, 237, 211, 45, 58, 219, 250, 0, 59, 211, 98, 229, 226, 167, 150, 149, 184, 87, 240, 250, 228, 143, 11, 23, 220, 255, 51, 75, 184, 162, 201, 69, 27, 181, 35, 7, 174, 207, 73, 159, 236, 234, 208, 19, 35, 112, 195, 173, 120, 78, 185, 109, 119, 19, 174, 95 },
                            Role = 1
                        });
                });

            modelBuilder.Entity("OnlineWallet.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalletCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("TransactionWallet", b =>
                {
                    b.Property<Guid>("TransactionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WalletsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TransactionsId", "WalletsId");

                    b.HasIndex("WalletsId");

                    b.ToTable("WalletTransactions", (string)null);
                });

            modelBuilder.Entity("OnlineWallet.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("OnlineWallet.Domain.Entities.User", null)
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactionWallet", b =>
                {
                    b.HasOne("OnlineWallet.Domain.Entities.Transaction", null)
                        .WithMany()
                        .HasForeignKey("TransactionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineWallet.Domain.Entities.Wallet", null)
                        .WithMany()
                        .HasForeignKey("WalletsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineWallet.Domain.Entities.User", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
