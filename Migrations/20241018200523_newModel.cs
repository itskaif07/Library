using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class newModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Apis");

            migrationBuilder.DropColumn(
                name: "BookCover",
                table: "Apis");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Apis");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Apis");

            migrationBuilder.AddColumn<int>(
                name: "VolumeInfoId",
                table: "Apis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ImageLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolumeInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageLinksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeInfo_ImageLinks_ImageLinksId",
                        column: x => x.ImageLinksId,
                        principalTable: "ImageLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apis_VolumeInfoId",
                table: "Apis",
                column: "VolumeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeInfo_ImageLinksId",
                table: "VolumeInfo",
                column: "ImageLinksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apis_VolumeInfo_VolumeInfoId",
                table: "Apis",
                column: "VolumeInfoId",
                principalTable: "VolumeInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apis_VolumeInfo_VolumeInfoId",
                table: "Apis");

            migrationBuilder.DropTable(
                name: "VolumeInfo");

            migrationBuilder.DropTable(
                name: "ImageLinks");

            migrationBuilder.DropIndex(
                name: "IX_Apis_VolumeInfoId",
                table: "Apis");

            migrationBuilder.DropColumn(
                name: "VolumeInfoId",
                table: "Apis");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Apis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookCover",
                table: "Apis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Apis",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Apis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
