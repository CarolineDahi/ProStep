using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProStep.DataSourse.Migrations
{
    /// <inheritdoc />
    public partial class road : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "RoadMapLevel");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_RoadMaps_CategoryId",
                table: "RoadMaps");

            migrationBuilder.DropColumn(
                name: "RoadMapId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationFor",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RoadMapCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoadMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoadMapCategoryParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadMapCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadMapCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoadMapCategory_RoadMapCategory_RoadMapCategoryParentId",
                        column: x => x.RoadMapCategoryParentId,
                        principalTable: "RoadMapCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoadMapCategory_RoadMaps_RoadMapId",
                        column: x => x.RoadMapId,
                        principalTable: "RoadMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoadMaps_CategoryId",
                table: "RoadMaps",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadMapCategory_CategoryId",
                table: "RoadMapCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadMapCategory_RoadMapCategoryParentId",
                table: "RoadMapCategory",
                column: "RoadMapCategoryParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadMapCategory_RoadMapId",
                table: "RoadMapCategory",
                column: "RoadMapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoadMapCategory");

            migrationBuilder.DropIndex(
                name: "IX_RoadMaps_CategoryId",
                table: "RoadMaps");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationFor",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoadMapId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoadMapLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoadMapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadMapLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadMapLevel_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadMapLevel_RoadMaps_RoadMapId",
                        column: x => x.RoadMapId,
                        principalTable: "RoadMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoadMapLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_RoadMapLevel_RoadMapLevelId",
                        column: x => x.RoadMapLevelId,
                        principalTable: "RoadMapLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoadMaps_CategoryId",
                table: "RoadMaps",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoadMapLevel_LevelId",
                table: "RoadMapLevel",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadMapLevel_RoadMapId",
                table: "RoadMapLevel",
                column: "RoadMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RoadMapLevelId",
                table: "Steps",
                column: "RoadMapLevelId");
        }
    }
}
