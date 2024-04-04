using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineWallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderWalletCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverWalletCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    TransactionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => new { x.TransactionsId, x.WalletsId });
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletsId",
                        column: x => x.WalletsId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { new Guid("88e2537d-59fa-4ea4-88a0-4da67160f69f"), "imegr@gmail.com", "Iuri", "Megreladze", new byte[] { 109, 121, 57, 115, 143, 50, 80, 191, 68, 51, 73, 37, 72, 237, 209, 154, 233, 59, 114, 0, 27, 196, 148, 250, 198, 104, 248, 179, 197, 255, 77, 16, 31, 22, 211, 124, 163, 128, 201, 200, 102, 74, 181, 152, 242, 197, 250, 254, 240, 94, 150, 213, 131, 131, 19, 185, 7, 251, 159, 237, 26, 254, 204, 198 }, new byte[] { 154, 190, 57, 53, 86, 179, 18, 36, 205, 9, 198, 251, 159, 239, 0, 23, 152, 118, 207, 0, 62, 157, 127, 207, 13, 189, 13, 20, 220, 4, 205, 188, 254, 235, 218, 234, 18, 100, 6, 26, 233, 212, 83, 189, 54, 113, 223, 80, 3, 101, 14, 180, 56, 43, 183, 102, 101, 148, 144, 16, 200, 156, 172, 19, 231, 93, 30, 37, 232, 143, 90, 228, 249, 237, 211, 45, 58, 219, 250, 0, 59, 211, 98, 229, 226, 167, 150, 149, 184, 87, 240, 250, 228, 143, 11, 23, 220, 255, 51, 75, 184, 162, 201, 69, 27, 181, 35, 7, 174, 207, 73, 159, 236, 234, 208, 19, 35, 112, 195, 173, 120, 78, 185, 109, 119, 19, 174, 95 }, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletsId",
                table: "WalletTransactions",
                column: "WalletsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
