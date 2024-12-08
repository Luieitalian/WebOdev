using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebOdev.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Calisanlar_CalisanID",
                table: "CalisanIslemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Islemler_IslemID",
                table: "CalisanIslemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler");

            migrationBuilder.DropIndex(
                name: "IX_Musteriler_KullaniciId",
                table: "Musteriler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "CalisanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MusteriId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IslemID",
                table: "CalisanIslemModel",
                newName: "IslemId");

            migrationBuilder.RenameColumn(
                name: "CalisanID",
                table: "CalisanIslemModel",
                newName: "CalisanId");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanIslemModel_IslemID",
                table: "CalisanIslemModel",
                newName: "IX_CalisanIslemModel_IslemId");

            migrationBuilder.AlterColumn<string>(
                name: "MusteriId",
                table: "Randevular",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "CalisanId",
                table: "Randevular",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "CalisanId",
                table: "CalisanIslemModel",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler",
                column: "KullaniciId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar",
                column: "KullaniciId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "KullaniciId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Calisanlar_CalisanId",
                table: "CalisanIslemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanIslemModel_Islemler_IslemId",
                table: "CalisanIslemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "IslemId",
                table: "CalisanIslemModel",
                newName: "IslemID");

            migrationBuilder.RenameColumn(
                name: "CalisanId",
                table: "CalisanIslemModel",
                newName: "CalisanID");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanIslemModel_IslemId",
                table: "CalisanIslemModel",
                newName: "IX_CalisanIslemModel_IslemID");

            migrationBuilder.AlterColumn<int>(
                name: "MusteriId",
                table: "Randevular",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CalisanId",
                table: "Randevular",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Musteriler",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Calisanlar",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "CalisanID",
                table: "CalisanIslemModel",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "CalisanId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusteriId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_KullaniciId",
                table: "Musteriler",
                column: "KullaniciId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_KullaniciId",
                table: "Calisanlar",
                column: "KullaniciId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemModel_Calisanlar_CalisanID",
                table: "CalisanIslemModel",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanIslemModel_Islemler_IslemID",
                table: "CalisanIslemModel",
                column: "IslemID",
                principalTable: "Islemler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
