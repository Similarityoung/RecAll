using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecAll.Core.List.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "list");

            migrationBuilder.CreateSequence(
                name: "listseq",
                schema: "list",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "listtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UserIdentityGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lists_listtypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "listtypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_listtypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "listtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lists_TypeId",
                table: "lists",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_TypeId",
                table: "Sets",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lists");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "listtypes");

            migrationBuilder.DropSequence(
                name: "listseq",
                schema: "list");
        }
    }
}
