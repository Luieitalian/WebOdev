using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebOdev.Migrations
{
    /// <inheritdoc />
    public partial class MigrateCinsiyetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            UPDATE ""Calisanlar""
            SET ""NewCinsiyet"" = 
                CASE 
                    WHEN ""Cinsiyet"" = 'Male' THEN 1
                    WHEN ""Cinsiyet"" = 'Female' THEN 2
                    WHEN ""Cinsiyet"" = 'Other' THEN 3
                    WHEN ""Cinsiyet"" = 'PreferNotToSay' THEN 4
                    ELSE NULL
                END
            WHERE ""Cinsiyet"" IS NOT NULL;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            UPDATE ""Calisanlar""
            SET ""NewCinsiyet"" = NULL
            WHERE ""NewCinsiyet"" IS NOT NULL;
            ");
        }
    }
}
