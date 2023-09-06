using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOwnBank.Migrations
{
    /// <inheritdoc />
    public partial class AddConfirmTokenFeildinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmToken",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "Users");
        }
    }
}
