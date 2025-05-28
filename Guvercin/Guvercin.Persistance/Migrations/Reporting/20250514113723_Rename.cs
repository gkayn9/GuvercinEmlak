using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guvercin.Persistance.Migrations.Reporting
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currencies",
                table: "AdvertItems",
                newName: "CurrencySymbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencySymbol",
                table: "AdvertItems",
                newName: "Currencies");
        }
    }
}
