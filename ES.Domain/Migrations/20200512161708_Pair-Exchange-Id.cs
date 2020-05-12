using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class PairExchangeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Exchanges_ExchangeId",
                table: "Pairs");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangeId",
                table: "Pairs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CentralizationType",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GradePoints",
                table: "Exchanges",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "OrderBook",
                table: "Exchanges",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Trades",
                table: "Exchanges",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Exchanges_ExchangeId",
                table: "Pairs",
                column: "ExchangeId",
                principalTable: "Exchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Exchanges_ExchangeId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "CentralizationType",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "GradePoints",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "OrderBook",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Trades",
                table: "Exchanges");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangeId",
                table: "Pairs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Exchanges_ExchangeId",
                table: "Pairs",
                column: "ExchangeId",
                principalTable: "Exchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
