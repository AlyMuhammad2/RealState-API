using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class dp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Agencies_AgencyId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIAUEqTDQoPNZ3EBZrGG+nSoZqo0Ujzzbau5IHK3yZZYpcFkPG9WTWCkWblo831I4Q==");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Agencies_AgencyId",
                table: "Products",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Agencies_AgencyId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGy7vVbVN6h4AUJ7IXr4XKUgeprbLjLurbQ7o+32g9HTOhWb4Oy9lF0zzcXEPWj8tQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Agencies_AgencyId",
                table: "Products",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id");
        }
    }
}
