using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class Subscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Subscriptions_SubscriptionId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_SubscriptionId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Currencies");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_Email",
                table: "Accounts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CurrencyId",
                table: "Subscriptions",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Currencies_CurrencyId",
                table: "Subscriptions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Currencies_CurrencyId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CurrencyId",
                table: "Subscriptions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Currencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_SubscriptionId",
                table: "Currencies",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Subscriptions_SubscriptionId",
                table: "Currencies",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
