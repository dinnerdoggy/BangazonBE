using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangazonBE.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToProductOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A Moon Greatsword, bestowed by a Carian queen upon her spouse to honor long-standing tradition.One of the legendary armaments.Ranni's sigil is a full moon, cold and leaden, and this sword is but a beam of its light.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Blade built into Malenia's prosthetic arm.Through consecration it is resistant to rot.Malenia's war prosthesis symbolized her victories.Some claim to have seen wings when the weapon was raised aloft;wings of fierce determination that have never known defeat.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Curved greatsword wielded by Magma Wyrms. The shape resembles a dragon's jaw and is covered in hard scales.It's said these land-bound dragons were once human heroes who partook in dragon communion, a grave transgression for which they were cursed to crawl the earth upon their bellies, shadows of their former selves.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductOrders");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A Moon Greatsword, bestowed by a Carian queen upon her\r\nspouse to honor long-standing tradition.\r\nOne of the legendary armaments.\r\n\r\nRanni's sigil is a full moon, cold and leaden, and this sword is but a beam of its light.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Blade built into Malenia's prosthetic arm.\r\nThrough consecration it is resistant to rot.\r\n\r\nMalenia's war prosthesis symbolized her victories.\r\nSome claim to have seen wings when the weapon was raised aloft;\r\nwings of fierce determination that have never known defeat.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Curved greatsword wielded by Magma Wyrms. The shape resembles a dragon's jaw and is covered in hard scales.\r\n\r\nIt's said these land-bound dragons were once human heroes who partook in dragon communion, a grave transgression for which they were cursed to crawl the earth upon their bellies, shadows of their former selves.");
        }
    }
}
