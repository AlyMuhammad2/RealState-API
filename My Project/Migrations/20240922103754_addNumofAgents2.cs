using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Project.Migrations
{
    /// <inheritdoc />
    public partial class addNumofAgents2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfAgents",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMvMIq3jYVmg70F6k1WDSETlKcCTTCBPu3vy84ze0JBrsRAn4i+Xl4PFA2KBiA1FLw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfAgents",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHNReD+brO/jRZvGIp4+19ZZAEumljV3QcVv3AGMJl1j7ovfeCyjvYM/3D3ORoeTiQ==");
        }
    }
}
