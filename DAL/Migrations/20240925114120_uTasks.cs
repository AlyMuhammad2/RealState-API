using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class uTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECp0ew4Z+zxQCOIAfUoa/i/8efKjObEvCubyQpxLOOVXS5dix59iyp91FAGd5ixjog==");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AgencyId",
                table: "Tasks",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Agencies_AgencyId",
                table: "Tasks",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Agencies_AgencyId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AgencyId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKxlik8qP/KfhK1rzPHMur7u2ziNrHpdug0kIE9isSsg+MRe+Ms2Ct8Rj1ZQHShCmQ==");
        }
    }
}
