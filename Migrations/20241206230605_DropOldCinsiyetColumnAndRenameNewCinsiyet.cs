using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebOdev.Migrations
{
    /// <inheritdoc />
    public partial class DropOldCinsiyetColumnAndRenameNewCinsiyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old Cinsiyet column
            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "Calisanlar");

            // Rename the NewCinsiyet column to Cinsiyet
            migrationBuilder.RenameColumn(
                name: "NewCinsiyet",
                table: "Calisanlar",
                newName: "Cinsiyet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
           name: "Cinsiyet",
           table: "Calisanlar",
           nullable: true);

            migrationBuilder.RenameColumn(
                name: "Cinsiyet",
                table: "Calisanlar",
                newName: "NewCinsiyet");
        }
    }
}
