using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "admin@gmail.com", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEGy7vVbVN6h4AUJ7IXr4XKUgeprbLjLurbQ7o+32g9HTOhWb4Oy9lF0zzcXEPWj8tQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "admin@admin.com", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEA+RvfS0fo/bGrSJiqi7PF9TSt7NJ7gDttYSd9blGUXcVhgFrNEUg08Fa7yBWI0qZA==" });
        }
    }
}
