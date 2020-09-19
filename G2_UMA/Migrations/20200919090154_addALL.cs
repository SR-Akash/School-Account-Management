using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G2_UMA.Migrations
{
    public partial class addALL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Fee_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Fee_Id);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    M_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.M_Id);
                });

            migrationBuilder.CreateTable(
                name: "student_info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    std_id = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    gender = table.Column<string>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    paid = table.Column<int>(nullable: false),
                    due = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    username = table.Column<string>(nullable: false),
                    pass = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Std_Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Birth_Date = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Father_Name = table.Column<string>(nullable: false),
                    Mother_Name = table.Column<string>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    M_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Std_Id);
                    table.ForeignKey(
                        name: "FK_Students_Months_M_Id",
                        column: x => x.M_Id,
                        principalTable: "Months",
                        principalColumn: "M_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Std_Id = table.Column<string>(nullable: true),
                    Fee_Id = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    M_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payment_Fees",
                        column: x => x.Fee_Id,
                        principalTable: "Fees",
                        principalColumn: "Fee_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Months",
                        column: x => x.M_Id,
                        principalTable: "Months",
                        principalColumn: "M_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Students",
                        column: x => x.Std_Id,
                        principalTable: "Students",
                        principalColumn: "Std_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Fee_Id",
                table: "Payment",
                column: "Fee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_M_Id",
                table: "Payment",
                column: "M_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Std_Id",
                table: "Payment",
                column: "Std_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_M_Id",
                table: "Students",
                column: "M_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "student_info");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Months");
        }
    }
}
