using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryAPI.Migrations
{
    /// <inheritdoc />
    public partial class userandresetpasswordReationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "resetPasswordid",
                table: "ResetPassword",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user_id1",
                table: "ResetPassword",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_resetPasswordid",
                table: "ResetPassword",
                column: "resetPasswordid");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_user_id1",
                table: "ResetPassword",
                column: "user_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_ResetPassword_resetPasswordid",
                table: "ResetPassword",
                column: "resetPasswordid",
                principalTable: "ResetPassword",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResetPassword_users_user_id1",
                table: "ResetPassword",
                column: "user_id1",
                principalTable: "users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_ResetPassword_resetPasswordid",
                table: "ResetPassword");

            migrationBuilder.DropForeignKey(
                name: "FK_ResetPassword_users_user_id1",
                table: "ResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_ResetPassword_resetPasswordid",
                table: "ResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_ResetPassword_user_id1",
                table: "ResetPassword");

            migrationBuilder.DropColumn(
                name: "resetPasswordid",
                table: "ResetPassword");

            migrationBuilder.DropColumn(
                name: "user_id1",
                table: "ResetPassword");
        }
    }
}
