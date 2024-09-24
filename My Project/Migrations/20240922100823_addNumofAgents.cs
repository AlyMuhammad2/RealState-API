using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Project.Migrations
{
    /// <inheritdoc />
    public partial class addNumofAgents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfAvailableAgents",
                table: "Agencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumofAgents",
                table: "Agencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJbupAxyOj2baHVf4IJ1VCsFDiv3XyHrgE8+JkuHooO4AddVCE6pFGBvsPOGgBH3BQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfAvailableAgents",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "NumofAgents",
                table: "Agencies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ6PrwPSK2MysHiAtDRtvlhEUqyRo1fLEfamvKwGhiE77IN2XjkF3psnS1wf+mcpIQ==");
        }
    }
}
