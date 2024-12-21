using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebOdev.Migrations
{
    /// <inheritdoc />
    public partial class IslemModelImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Islemler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Islemler",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Islemler");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Islemler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
