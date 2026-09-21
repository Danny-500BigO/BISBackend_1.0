using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeresetpasswordentitydatetimetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "expired_at",
                table: "ResetPassword",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "expired_at",
                table: "ResetPassword",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
