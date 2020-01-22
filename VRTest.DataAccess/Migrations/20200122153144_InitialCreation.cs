using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VRTest.DataAccess.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NPVPreviousRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CashFlowsDescription = table.Column<string>(nullable: false),
                    InitialCost = table.Column<double>(nullable: false),
                    UpperBoundDiscountRate = table.Column<double>(nullable: false),
                    LowerBoundDiscountRate = table.Column<double>(nullable: false),
                    IncrementRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPVPreviousRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPVPreviousResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NPVPreviousRequestId = table.Column<int>(nullable: false),
                    DiscountRate = table.Column<double>(nullable: false),
                    NPV = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPVPreviousResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPVPreviousResult_NPVPreviousRequests_NPVPreviousRequestId",
                        column: x => x.NPVPreviousRequestId,
                        principalTable: "NPVPreviousRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NPVPreviousRequests_IncrementRate_InitialCost_LowerBoundDiscountRate_UpperBoundDiscountRate",
                table: "NPVPreviousRequests",
                columns: new[] { "IncrementRate", "InitialCost", "LowerBoundDiscountRate", "UpperBoundDiscountRate" });

            migrationBuilder.CreateIndex(
                name: "IX_NPVPreviousResult_NPVPreviousRequestId",
                table: "NPVPreviousResult",
                column: "NPVPreviousRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NPVPreviousResult");

            migrationBuilder.DropTable(
                name: "NPVPreviousRequests");
        }
    }
}
