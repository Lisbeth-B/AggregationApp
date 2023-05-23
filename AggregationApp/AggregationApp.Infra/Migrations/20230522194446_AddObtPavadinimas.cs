using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregationApp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddObtPavadinimas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObtPavadinimas",
                table: "ElectricityConsumptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObtPavadinimas",
                table: "ElectricityConsumptions");
        }
    }
}
