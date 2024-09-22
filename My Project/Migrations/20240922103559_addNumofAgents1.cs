using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Project.Migrations
{
    /// <inheritdoc />
    public partial class addNumofAgents1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumofAgents",
                table: "Agencies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHNReD+brO/jRZvGIp4+19ZZAEumljV3QcVv3AGMJl1j7ovfeCyjvYM/3D3ORoeTiQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
