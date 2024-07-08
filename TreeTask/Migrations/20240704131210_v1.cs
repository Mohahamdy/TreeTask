using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreeTask.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetsTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLast = table.Column<bool>(type: "bit", nullable: false),
                    Money = table.Column<double>(type: "float", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetsTrees_AssetsTrees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AssetsTrees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetsTrees_ParentId",
                table: "AssetsTrees",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsTrees");
        }
    }
}
