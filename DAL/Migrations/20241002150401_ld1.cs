using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ld1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfAgents",
                table: "Subscriptions",
                newName: "NumOfsubs");

            migrationBuilder.AddColumn<int>(
                name: "NumOfAvailableAgents",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfAvailableproducts",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfimagesperproducts",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA+RvfS0fo/bGrSJiqi7PF9TSt7NJ7gDttYSd9blGUXcVhgFrNEUg08Fa7yBWI0qZA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfAvailableAgents",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NumOfAvailableproducts",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NumOfimagesperproducts",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "NumOfsubs",
                table: "Subscriptions",
                newName: "NumOfAgents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOYj87NEYaGu91U8U1PJEm+tqmL0Flyt+hpHBxMNGq53J/QPdkdGkmXejFU0iHeq7w==");
        }
    }
}
