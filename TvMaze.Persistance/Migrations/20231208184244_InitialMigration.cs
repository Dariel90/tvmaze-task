using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    SysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country_Timezone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.SysId);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    SysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    AverageRuntime = table.Column<int>(type: "int", nullable: false),
                    Premiered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ended = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficialSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    NetworkSysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebChannel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DvdCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<long>(type: "bigint", nullable: false),
                    Externals_Imdb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Externals_TheTvDb = table.Column<int>(type: "int", nullable: false),
                    Externals_TvRage = table.Column<int>(type: "int", nullable: false),
                    Image_Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_Original = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating_Average = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Schedule_DayOfTheWeek = table.Column<int>(type: "int", nullable: false),
                    Schedule_Time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.SysId);
                    table.ForeignKey(
                        name: "FK_Shows_Networks_NetworkSysId",
                        column: x => x.NetworkSysId,
                        principalTable: "Networks",
                        principalColumn: "SysId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ShowSysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => new { x.ShowSysId, x.Id });
                    table.ForeignKey(
                        name: "FK_Genre_Shows_ShowSysId",
                        column: x => x.ShowSysId,
                        principalTable: "Shows",
                        principalColumn: "SysId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    SysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousEpisode_Href = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Self_Href = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.SysId);
                    table.ForeignKey(
                        name: "FK_Links_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "SysId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_ShowId",
                table: "Links",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_NetworkSysId",
                table: "Shows",
                column: "NetworkSysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Networks");
        }
    }
}
