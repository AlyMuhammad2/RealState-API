using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class upAgy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agencies_OwnerId",
                table: "Agencies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELZhOTrdFh7rvNPvFNZxpk8WLX7E7BRmZTNFi3xta+Li3LTpVULQI5zuaNF3yP7S5g==");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_OwnerId",
                table: "Agencies",
                column: "OwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agencies_OwnerId",
                table: "Agencies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAED+KsFDV5uubDq1HxfxcjUmGNaG430kTbTnXXiEVUE8EodNrNvLGk3ACAARIox22rA==");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_OwnerId",
                table: "Agencies",
                column: "OwnerId");
        }
    }
}
