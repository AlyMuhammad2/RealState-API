using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_Subscriptions_SubscriptionId",
                table: "Agencies");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "Agencies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKxlik8qP/KfhK1rzPHMur7u2ziNrHpdug0kIE9isSsg+MRe+Ms2Ct8Rj1ZQHShCmQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_Subscriptions_SubscriptionId",
                table: "Agencies",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agencies_Subscriptions_SubscriptionId",
                table: "Agencies");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "Agencies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEODz7fHdhEETCMdLWfxMi7btkqMe0vdy+U9m1q+mdSj5S5mqQ1HWLw12KFuHhpg2mQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Agencies_Subscriptions_SubscriptionId",
                table: "Agencies",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
