using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOwnBank.Migrations
{
    /// <inheritdoc />
    public partial class AddConfirmFeildinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirm",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirm",
                table: "Users");
        }
    }
}
