using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class Acc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pairs_ExchangeId",
                table: "Pairs");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Currencies",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Pairs_ExchangeId_CurrencyToId_CurrencyFromId",
                table: "Pairs",
                columns: new[] { "ExchangeId", "CurrencyToId", "CurrencyFromId" });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Predicate = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_SubscriptionId",
                table: "Currencies",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AccountId",
                table: "Subscriptions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Subscriptions_SubscriptionId",
                table: "Currencies",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Subscriptions_SubscriptionId",
                table: "Currencies");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Pairs_ExchangeId_CurrencyToId_CurrencyFromId",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_SubscriptionId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Currencies");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_ExchangeId",
                table: "Pairs",
                column: "ExchangeId");
        }
    }
}
