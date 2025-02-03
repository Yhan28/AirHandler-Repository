using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirHandlers.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirHandlers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdentifierCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsOperating = table.Column<bool>(type: "INTEGER", nullable: false),
                    FilterChangeDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReferenceTemperature = table.Column<double>(type: "REAL", nullable: false),
                    ReferenceHumidity = table.Column<double>(type: "REAL", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirHandlers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ReferenceTemperature = table.Column<double>(type: "REAL", nullable: false),
                    ReferenceHumidity = table.Column<double>(type: "REAL", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Volume = table.Column<double>(type: "REAL", nullable: false),
                    AssociatedHandlerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rooms_AirHandlers_AssociatedHandlerId",
                        column: x => x.AssociatedHandlerId,
                        principalTable: "AirHandlers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirHandlerRecipe",
                columns: table => new
                {
                    AirHandlerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecipeID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirHandlerRecipe", x => new { x.AirHandlerID, x.RecipeID });
                    table.ForeignKey(
                        name: "FK_AirHandlerRecipe_AirHandlers_AirHandlerID",
                        column: x => x.AirHandlerID,
                        principalTable: "AirHandlers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirHandlerRecipe_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirHandlerRecipe_RecipeID",
                table: "AirHandlerRecipe",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AssociatedHandlerId",
                table: "Rooms",
                column: "AssociatedHandlerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirHandlerRecipe");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "AirHandlers");
        }
    }
}
