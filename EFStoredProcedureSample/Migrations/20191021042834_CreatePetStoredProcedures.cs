using Microsoft.EntityFrameworkCore.Migrations;

namespace EFStoredProcedureSample.Migrations
{
    public partial class CreatePetStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetPetsByType @Type varchar(50)
                AS   
                    SET NOCOUNT ON;
                    SELECT [PetId], [Name], [Type]  
                    FROM Pets
                    WHERE [Type] = @Type;
                GO  
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE GetPetsByType;  
                GO  
            ");
        }
    }
}
