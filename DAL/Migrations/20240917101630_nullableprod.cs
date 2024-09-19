using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class nullableprod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Agencies_AgencyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Agents_AgentId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Agencies_AgencyId",
                table: "Product",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Agents_AgentId",
                table: "Product",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Agencies_AgencyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Agents_AgentId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgencyId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Agencies_AgencyId",
                table: "Product",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Agents_AgentId",
                table: "Product",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
