using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidyalaya.Migrations
{
    /// <inheritdoc />
    public partial class AddVidyalayaToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    AId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ATitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ADescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.AId);
                });

            migrationBuilder.CreateTable(
                name: "cclass",
                columns: table => new
                {
                    CId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CRoomNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cclass", x => x.CId);
                });

            migrationBuilder.CreateTable(
                name: "school",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school", x => x.SId);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    TId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.TId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "cclass");

            migrationBuilder.DropTable(
                name: "school");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
