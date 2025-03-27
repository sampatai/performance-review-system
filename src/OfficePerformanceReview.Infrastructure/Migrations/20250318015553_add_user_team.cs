using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficePerformanceReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_user_team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Team_Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Team_Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Team_Name",
                table: "Users");
        }
    }
}
