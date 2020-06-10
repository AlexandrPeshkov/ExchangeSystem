using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class FK_Candle_Pair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Candles_Pairs_ExchangePairId",
            //    table: "Candles");

            migrationBuilder.DropForeignKey(
                name: "FK_Candles_Pairs_PairId",
                table: "Candles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Candles_TimeOpen_TimeClose_PairId",
                table: "Candles");

            migrationBuilder.DropIndex(
                name: "IX_Candles_PairId",
                table: "Candles");

            //migrationBuilder.DropColumn(
            //    name: "PairId",
            //    table: "Candles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangePairId",
                table: "Candles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExchangePairId1",
                table: "Candles",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Candles_TimeOpen_TimeClose_ExchangePairId",
                table: "Candles",
                columns: new[] { "TimeOpen", "TimeClose", "ExchangePairId" });

            migrationBuilder.CreateIndex(
                name: "IX_Candles_ExchangePairId1",
                table: "Candles",
                column: "ExchangePairId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Candles_Pairs_ExchangePairId",
                table: "Candles",
                column: "ExchangePairId",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candles_Pairs_ExchangePairId1",
                table: "Candles",
                column: "ExchangePairId1",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candles_Pairs_ExchangePairId",
                table: "Candles");

            migrationBuilder.DropForeignKey(
                name: "FK_Candles_Pairs_ExchangePairId1",
                table: "Candles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Candles_TimeOpen_TimeClose_ExchangePairId",
                table: "Candles");

            migrationBuilder.DropIndex(
                name: "IX_Candles_ExchangePairId1",
                table: "Candles");

            migrationBuilder.DropColumn(
                name: "ExchangePairId1",
                table: "Candles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExchangePairId",
                table: "Candles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "PairId",
                table: "Candles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Candles_TimeOpen_TimeClose_PairId",
                table: "Candles",
                columns: new[] { "TimeOpen", "TimeClose", "PairId" });

            migrationBuilder.CreateIndex(
                name: "IX_Candles_PairId",
                table: "Candles",
                column: "PairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candles_Pairs_ExchangePairId",
                table: "Candles",
                column: "ExchangePairId",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candles_Pairs_PairId",
                table: "Candles",
                column: "PairId",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
