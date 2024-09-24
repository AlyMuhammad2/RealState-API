using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesAndUsers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ6PrwPSK2MysHiAtDRtvlhEUqyRo1fLEfamvKwGhiE77IN2XjkF3psnS1wf+mcpIQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC45wE14fmXOu9BJdiQx/cu5LDrQrSuyxL1WQvMDwIHvLhdf8jorpEjToR6b8qOs0g==");
        }
    }
}
