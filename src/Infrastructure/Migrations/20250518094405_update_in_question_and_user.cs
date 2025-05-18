using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficePerformanceReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_in_question_and_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingMax",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingMin",
                table: "Questions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RatingMax",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RatingMin",
                table: "Questions");
        }
    }
}
