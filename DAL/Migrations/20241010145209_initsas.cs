using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class initsas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForRent",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfBathrom",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfBedroom",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfCars",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKwF3ql0WhwaWoKRkVtM0bYCW+8Go8hjBcYJBaXdyCRz6sMn9YJUpbgup6ehEdsKCw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsForRent",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NumOfBathrom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NumOfBedroom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NumOfCars",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEpLqoR24piqcIvMGrHe2sGeygFLDnpiMmtaN7w7BfhP1mF4XT6X9XqhuWVH+KufPQ==");
        }
    }
}
