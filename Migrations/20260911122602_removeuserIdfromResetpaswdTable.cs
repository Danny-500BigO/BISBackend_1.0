using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeuserIdfromResetpaswdTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_users_user_id1",
                table: "ResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_ResetPassword_user_id1",
                table: "ResetPassword");

            migrationBuilder.DropColumn(
                name: "user_id1",
                table: "ResetPassword");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "ResetPassword",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_user_id",
                table: "ResetPassword",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_ResetPassword_user_id",
                table: "ResetPassword");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "ResetPassword",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id1",
                table: "ResetPassword",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_user_id1",
                table: "ResetPassword",
                column: "user_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_users_user_id1",
                table: "ResetPassword",
                column: "user_id1",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
