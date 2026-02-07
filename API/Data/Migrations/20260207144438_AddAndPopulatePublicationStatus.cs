using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAndPopulatePublicationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicationStatus",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
                
            migrationBuilder.Sql(@"
                UPDATE Series
                SET PublicationStatus = (
                    SELECT M.PublicationStatus
                    FROM SeriesMetadata M
                    WHERE M.SeriesId = Series.Id
                )
                WHERE EXISTS (
                    SELECT 1 
                    FROM SeriesMetadata M 
                    WHERE M.SeriesId = Series.Id
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationStatus",
                table: "Series");
        }
    }
}
