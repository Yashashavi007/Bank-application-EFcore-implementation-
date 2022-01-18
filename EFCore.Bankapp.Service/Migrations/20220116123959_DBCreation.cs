using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Bankapp.Service.Migrations
{
    public partial class DBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IFSCCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => new { x.ID, x.IFSCCode });
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(767)", nullable: false),
                    ConversionRate = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AccountNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Pin = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<float>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BankID = table.Column<string>(type: "varchar(100)", nullable: true),
                    BankIFSCCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.UniqueConstraint("AK_Customers_AccountNumber", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Customers_Banks_BankID_BankIFSCCode",
                        columns: x => new { x.BankID, x.BankIFSCCode },
                        principalTable: "Banks",
                        principalColumns: new[] { "ID", "IFSCCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    Pin = table.Column<int>(type: "int", nullable: false),
                    BankID = table.Column<string>(type: "varchar(100)", nullable: true),
                    BankIFSCCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Banks_BankID_BankIFSCCode",
                        columns: x => new { x.BankID, x.BankIFSCCode },
                        principalTable: "Banks",
                        principalColumns: new[] { "ID", "IFSCCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SenderAccountNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ReceiverAccountNumber = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    CustomerID = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankID_BankIFSCCode",
                table: "Customers",
                columns: new[] { "BankID", "BankIFSCCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankID_BankIFSCCode",
                table: "Employees",
                columns: new[] { "BankID", "BankIFSCCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerID",
                table: "Transactions",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
