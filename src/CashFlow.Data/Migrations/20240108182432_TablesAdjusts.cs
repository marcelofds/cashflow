using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class TablesAdjusts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billstopay");

            migrationBuilder.DropTable(
                name: "billstoreceive");

            migrationBuilder.CreateTable(
                name: "billtopay",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    expirationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    paymentdate = table.Column<DateOnly>(type: "date", nullable: true),
                    supplierid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_billtopay", x => x.id);
                    table.ForeignKey(
                        name: "fk_billtopay_supplier_supplierid",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "billtoreceive",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    expirationdate = table.Column<DateOnly>(type: "date", nullable: false),
                    paymentdate = table.Column<DateOnly>(type: "date", nullable: true),
                    customerid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_billtoreceive", x => x.id);
                    table.ForeignKey(
                        name: "fk_billtoreceive_customer_customerid",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_billtopay_supplierid",
                table: "billtopay",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_billtoreceive_customerid",
                table: "billtoreceive",
                column: "customerid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billtopay");

            migrationBuilder.DropTable(
                name: "billtoreceive");

            migrationBuilder.CreateTable(
                name: "billstopay",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplierid = table.Column<int>(type: "integer", nullable: false),
                    expirationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "billstoreceive",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    expirationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paymentdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_billstopay_supplierid",
                table: "billstopay",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_billstoreceive_customerid",
                table: "billstoreceive",
                column: "customerid");
        }
    }
}
