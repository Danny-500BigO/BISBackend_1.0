using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BakeryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddresetPasswordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResetPassword",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    token_hash = table.Column<string>(type: "text", nullable: false),
                    expired_at = table.Column<DateOnly>(type: "date", nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateOnly>(type: "date", nullable: false),
                    rate_limit = table.Column<int>(type: "integer", nullable: false),
                    last_reset_timeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPassword", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetPassword");
        }
    }
}
