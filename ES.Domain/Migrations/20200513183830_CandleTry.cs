using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class CandleTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exchanges",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currencies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Exchanges_Name",
                table: "Exchanges",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Currencies_Symbol",
                table: "Currencies",
                column: "Symbol");

            migrationBuilder.CreateTable(
                name: "Candles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeOpen = table.Column<long>(nullable: false),
                    TimeClose = table.Column<long>(nullable: false),
                    Interval = table.Column<long>(nullable: false),
                    Open = table.Column<decimal>(nullable: false),
                    High = table.Column<decimal>(nullable: false),
                    Low = table.Column<decimal>(nullable: false),
                    Close = table.Column<decimal>(nullable: false),
                    VolumeFrom = table.Column<decimal>(nullable: false),
                    VolumeTo = table.Column<decimal>(nullable: false),
                    PairId = table.Column<Guid>(nullable: false),
                    ExchangePairId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candles", x => x.Id);
                    table.UniqueConstraint("AK_Candles_TimeOpen_TimeClose_PairId", x => new { x.TimeOpen, x.TimeClose, x.PairId });
                    table.ForeignKey(
                        name: "FK_Candles_Pairs_ExchangePairId",
                        column: x => x.ExchangePairId,
                        principalTable: "Pairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candles_Pairs_PairId",
                        column: x => x.PairId,
                        principalTable: "Pairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candles_ExchangePairId",
                table: "Candles",
                column: "ExchangePairId");

            migrationBuilder.CreateIndex(
                name: "IX_Candles_PairId",
                table: "Candles",
                column: "PairId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Exchanges_Name",
                table: "Exchanges");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Currencies_Symbol",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exchanges",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
