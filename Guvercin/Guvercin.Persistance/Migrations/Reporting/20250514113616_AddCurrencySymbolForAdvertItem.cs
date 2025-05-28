using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guvercin.Persistance.Migrations.Reporting
{
    /// <inheritdoc />
    public partial class AddCurrencySymbolForAdvertItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currencies",
                table: "AdvertItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currencies",
                table: "AdvertItems");
        }
    }
}
