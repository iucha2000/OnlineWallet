using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineWallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionWalletstablemodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionWallet_Transactions_TransactionsId",
                table: "TransactionWallet");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionWallet_Wallets_WalletsId",
                table: "TransactionWallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionWallet",
                table: "TransactionWallet");

            migrationBuilder.RenameTable(
                name: "TransactionWallet",
                newName: "WalletTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionWallet_WalletsId",
                table: "WalletTransactions",
                newName: "IX_WalletTransactions_WalletsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletTransactions",
                table: "WalletTransactions",
                columns: new[] { "TransactionsId", "WalletsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Transactions_TransactionsId",
                table: "WalletTransactions",
                column: "TransactionsId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Wallets_WalletsId",
                table: "WalletTransactions",
                column: "WalletsId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Transactions_TransactionsId",
                table: "WalletTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Wallets_WalletsId",
                table: "WalletTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletTransactions",
                table: "WalletTransactions");

            migrationBuilder.RenameTable(
                name: "WalletTransactions",
                newName: "TransactionWallet");

            migrationBuilder.RenameIndex(
                name: "IX_WalletTransactions_WalletsId",
                table: "TransactionWallet",
                newName: "IX_TransactionWallet_WalletsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionWallet",
                table: "TransactionWallet",
                columns: new[] { "TransactionsId", "WalletsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionWallet_Transactions_TransactionsId",
                table: "TransactionWallet",
                column: "TransactionsId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionWallet_Wallets_WalletsId",
                table: "TransactionWallet",
                column: "WalletsId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
