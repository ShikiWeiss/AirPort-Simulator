using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Dal.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Landings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaneId = table.Column<int>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    RunwayStationId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovementHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaneId = table.Column<int>(nullable: false),
                    ToStationId = table.Column<int>(nullable: false),
                    ExitedTime = table.Column<DateTime>(nullable: false),
                    EnteredTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Company = table.Column<string>(nullable: true),
                    CurrentStationId = table.Column<int>(nullable: false),
                    IsLanded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Takeoffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaneId = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    RunwayStationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takeoffs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Landings");

            migrationBuilder.DropTable(
                name: "MovementHistory");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Takeoffs");
        }
    }
}
