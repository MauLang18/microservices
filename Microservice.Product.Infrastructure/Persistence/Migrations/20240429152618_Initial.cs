using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Microservice.Product.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    StockMin = table.Column<int>(type: "integer", nullable: false),
                    StockMax = table.Column<int>(type: "integer", nullable: false),
                    UnitSalePrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: false),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
