using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProStep.DataSourse.Migrations
{
    /// <inheritdoc />
    public partial class IsApprove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "Portfolios",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "Portfolios");
        }
    }
}
