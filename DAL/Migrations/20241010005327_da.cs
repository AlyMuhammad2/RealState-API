using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class da : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEpLqoR24piqcIvMGrHe2sGeygFLDnpiMmtaN7w7BfhP1mF4XT6X9XqhuWVH+KufPQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIAUEqTDQoPNZ3EBZrGG+nSoZqo0Ujzzbau5IHK3yZZYpcFkPG9WTWCkWblo831I4Q==");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id");
        }
    }
}
