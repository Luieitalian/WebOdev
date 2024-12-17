using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebOdev.Migrations
{
    /// <inheritdoc />
    public partial class CalisanIslemleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Calisanlar_CalisanId",
                table: "CalisanIslemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Islemler_IslemId",
                table: "CalisanIslemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalisanIslemModel",
                table: "CalisanIslemModel");

            migrationBuilder.RenameTable(
                name: "CalisanIslemModel",
                newName: "CalisanIslemleri");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanIslemModel_IslemId",
                table: "CalisanIslemleri",
                newName: "IX_CalisanIslemleri_IslemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalisanIslemleri",
                table: "CalisanIslemleri",
                columns: new[] { "CalisanId", "IslemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemleri_Calisanlar_CalisanId",
                table: "CalisanIslemleri",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemleri_Islemler_IslemId",
                table: "CalisanIslemleri",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemleri_Calisanlar_CalisanId",
                table: "CalisanIslemleri");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemleri_Islemler_IslemId",
                table: "CalisanIslemleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalisanIslemleri",
                table: "CalisanIslemleri");

            migrationBuilder.RenameTable(
                name: "CalisanIslemleri",
                newName: "CalisanIslemModel");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanIslemleri_IslemId",
                table: "CalisanIslemModel",
                newName: "IX_CalisanIslemModel_IslemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalisanIslemModel",
                table: "CalisanIslemModel",
                columns: new[] { "CalisanId", "IslemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemModel_Calisanlar_CalisanId",
                table: "CalisanIslemModel",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemModel_Islemler_IslemId",
                table: "CalisanIslemModel",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
