using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EKStore.Migrations
{
    /// <inheritdoc />
    public partial class istelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Warehouse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "Warehouse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Vehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "Vehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatus",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsStatus",
                table: "Driver");
        }
    }
}
