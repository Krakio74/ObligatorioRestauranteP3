using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioRestauranteP3.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Cliente");
        }
    }
}
