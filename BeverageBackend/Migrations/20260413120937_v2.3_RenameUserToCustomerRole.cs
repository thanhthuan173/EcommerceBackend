using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeverageBackend.Migrations
{
    /// <inheritdoc />
    public partial class v23_RenameUserToCustomerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: 2,
            column: "Name",
            value: "CUSTOMER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
