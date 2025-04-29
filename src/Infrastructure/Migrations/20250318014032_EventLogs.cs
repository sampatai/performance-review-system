using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficePerformanceReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "eventseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EventType_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType_Id = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropSequence(
                name: "eventseq");
        }
    }
}
