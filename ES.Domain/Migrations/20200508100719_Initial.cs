using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExchangeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    Volume = table.Column<decimal>(nullable: false),
                    Change = table.Column<decimal>(nullable: false),
                    TakerFee = table.Column<decimal>(nullable: false),
                    MakerFee = table.Column<decimal>(nullable: false),
                    Markets = table.Column<int>(nullable: false),
                    _ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
