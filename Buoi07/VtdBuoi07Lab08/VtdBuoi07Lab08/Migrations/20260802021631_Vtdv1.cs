using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtdBuoi07Lab08.Migrations
{
    /// <inheritdoc />
    public partial class Vtdv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VtdAccount",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VtdEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdAccount", x => x.VtdId);
                });

            migrationBuilder.CreateTable(
                name: "VtdBanner",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VtdStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    VtdPrioty = table.Column<int>(type: "int", nullable: false),
                    VtdCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VtdImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdBanner", x => x.VtdId);
                });

            migrationBuilder.CreateTable(
                name: "VtdBlog",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VtdStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    VtdViewCount = table.Column<int>(type: "int", nullable: false),
                    VtdCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VtdImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdDescription = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdBlog", x => x.VtdId);
                });

            migrationBuilder.CreateTable(
                name: "VtdCategory",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VtdStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    VtdCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VtdImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdCategory", x => x.VtdId);
                });

            migrationBuilder.CreateTable(
                name: "VtdCustomer",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdFullName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VtdEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdAddress = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    VtdAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VtdGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdFacebook = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdCustomer", x => x.VtdId);
                });

            migrationBuilder.CreateTable(
                name: "VtdProduct",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VtdImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdPrice = table.Column<float>(type: "real", nullable: false),
                    VtdSalePrice = table.Column<float>(type: "real", nullable: false),
                    VtdStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    VtdDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    VtdCategoryId = table.Column<int>(type: "int", nullable: false),
                    VtdCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdProduct", x => x.VtdId);
                    table.ForeignKey(
                        name: "FK_VtdProduct_VtdCategory_VtdCategoryId",
                        column: x => x.VtdCategoryId,
                        principalTable: "VtdCategory",
                        principalColumn: "VtdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VtdOrders",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdCustomerId = table.Column<int>(type: "int", nullable: false),
                    VtdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtdCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VtdStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdOrders", x => x.VtdId);
                    table.ForeignKey(
                        name: "FK_VtdOrders_VtdCustomer_VtdCustomerId",
                        column: x => x.VtdCustomerId,
                        principalTable: "VtdCustomer",
                        principalColumn: "VtdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VtdOrderDetail",
                columns: table => new
                {
                    VtdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VtdOrderId = table.Column<int>(type: "int", nullable: false),
                    VtdProductId = table.Column<int>(type: "int", nullable: false),
                    VtdQuantity = table.Column<int>(type: "int", nullable: false),
                    VtdPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtdOrderDetail", x => x.VtdId);
                    table.ForeignKey(
                        name: "FK_VtdOrderDetail_VtdOrders_VtdOrderId",
                        column: x => x.VtdOrderId,
                        principalTable: "VtdOrders",
                        principalColumn: "VtdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VtdOrderDetail_VtdProduct_VtdProductId",
                        column: x => x.VtdProductId,
                        principalTable: "VtdProduct",
                        principalColumn: "VtdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VtdCategory_VtdName",
                table: "VtdCategory",
                column: "VtdName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VtdOrderDetail_VtdOrderId_VtdProductId",
                table: "VtdOrderDetail",
                columns: new[] { "VtdOrderId", "VtdProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VtdOrderDetail_VtdProductId",
                table: "VtdOrderDetail",
                column: "VtdProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VtdOrders_VtdCustomerId",
                table: "VtdOrders",
                column: "VtdCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VtdProduct_VtdCategoryId",
                table: "VtdProduct",
                column: "VtdCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VtdAccount");

            migrationBuilder.DropTable(
                name: "VtdBanner");

            migrationBuilder.DropTable(
                name: "VtdBlog");

            migrationBuilder.DropTable(
                name: "VtdOrderDetail");

            migrationBuilder.DropTable(
                name: "VtdOrders");

            migrationBuilder.DropTable(
                name: "VtdProduct");

            migrationBuilder.DropTable(
                name: "VtdCustomer");

            migrationBuilder.DropTable(
                name: "VtdCategory");
        }
    }
}
