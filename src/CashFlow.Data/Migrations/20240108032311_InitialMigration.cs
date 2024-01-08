using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "billstoreceive",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    expirationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    customerid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_billstoreceive", x => x.id);
                    table.ForeignKey(
                        name: "fk_billstoreceive_customer_customerid",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "billstopay",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    expirationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    supplierid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_billstopay", x => x.id);
                    table.ForeignKey(
                        name: "fk_billstopay_supplier_supplierid",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_billstopay_supplierid",
                table: "billstopay",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_billstoreceive_customerid",
                table: "billstoreceive",
                column: "customerid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billstopay");

            migrationBuilder.DropTable(
                name: "billstoreceive");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
