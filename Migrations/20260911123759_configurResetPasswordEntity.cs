using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryAPI.Migrations
{
    /// <inheritdoc />
    public partial class configurResetPasswordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_ResetPassword_resetPasswordid",
                table: "ResetPassword");

            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_ResetPassword_resetPasswordid",
                table: "ResetPassword");

            migrationBuilder.DropColumn(
                name: "resetPasswordid",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "ResetPassword",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "resetPasswordid",
                table: "ResetPassword",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_resetPasswordid",
                table: "ResetPassword",
                column: "resetPasswordid");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_ResetPassword_resetPasswordid",
                table: "ResetPassword",
                column: "resetPasswordid",
                principalTable: "ResetPassword",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_users_user_id",
                table: "ResetPassword",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
