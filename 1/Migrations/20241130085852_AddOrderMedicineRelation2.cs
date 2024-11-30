using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderMedicineRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Orders_OrderId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_OrderId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "OrderMedicines",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMedicines", x => new { x.MedicinesId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderMedicines_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMedicines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderMedicines_OrderId",
                table: "OrderMedicines",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderMedicines");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_OrderId",
                table: "Medicines",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Orders_OrderId",
                table: "Medicines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
