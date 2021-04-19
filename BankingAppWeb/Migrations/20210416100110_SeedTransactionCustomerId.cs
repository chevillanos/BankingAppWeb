using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingAppWeb.Migrations
{
    public partial class SeedTransactionCustomerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("UPDATE Transactions SET CustomerId = 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
