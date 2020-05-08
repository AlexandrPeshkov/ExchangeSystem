using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class CurrencyPairExchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Change",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "ExchangeId",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "MakerFee",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Markets",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "TakerFee",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "_ID",
                table: "Exchanges");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    Algorithm = table.Column<string>(nullable: true),
                    TotalCoinsMined = table.Column<decimal>(nullable: true),
                    BlockNumber = table.Column<long>(nullable: true),
                    NetHashesPerSecond = table.Column<decimal>(nullable: true),
                    BlockReward = table.Column<decimal>(nullable: true),
                    SmartContractAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CurrencyFromId = table.Column<Guid>(nullable: false),
                    CurrencyToId = table.Column<Guid>(nullable: false),
                    ExchangeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pairs_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pairs_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pairs_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_CurrencyFromId",
                table: "Pairs",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_CurrencyToId",
                table: "Pairs",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_ExchangeId",
                table: "Pairs",
                column: "ExchangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Exchanges");

            migrationBuilder.AddColumn<decimal>(
                name: "Change",
                table: "Exchanges",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExchangeId",
                table: "Exchanges",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MakerFee",
                table: "Exchanges",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Markets",
                table: "Exchanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TakerFee",
                table: "Exchanges",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "Exchanges",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "_ID",
                table: "Exchanges",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
