using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirTravelService.DataAccess.PostgresSql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    passenger_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents", x => x.document_id);
                });

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    passenger_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_passengers", x => x.passenger_id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    service_provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    departure_point = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    destination_point = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    departure_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    arrival_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    registration_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    passenger_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tickets", x => x.ticket_id);
                });

            migrationBuilder.CreateTable(
                name: "document_fields",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_document_fields", x => new { x.document_id, x.name });
                    table.ForeignKey(
                        name: "fk_document_fields_documents_document_id",
                        column: x => x.document_id,
                        principalTable: "documents",
                        principalColumn: "document_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_fields");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "documents");
        }
    }
}
