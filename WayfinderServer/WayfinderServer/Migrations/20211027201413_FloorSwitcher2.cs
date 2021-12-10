using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class FloorSwitcher2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcher_Places_PlaceId",
                table: "FloorSwitcher");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcherPoint_Floors_FloorId",
                table: "FloorSwitcherPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcherPoint_FloorSwitcher_FloorSwitcherId",
                table: "FloorSwitcherPoint");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcherPoint_Places_PlaceId",
                table: "FloorSwitcherPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloorSwitcherPoint",
                table: "FloorSwitcherPoint");

            migrationBuilder.DropIndex(
                name: "IX_FloorSwitcherPoint_PlaceId",
                table: "FloorSwitcherPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloorSwitcher",
                table: "FloorSwitcher");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "FloorSwitcherPoint");

            migrationBuilder.RenameTable(
                name: "FloorSwitcherPoint",
                newName: "FloorSwitcherPoints");

            migrationBuilder.RenameTable(
                name: "FloorSwitcher",
                newName: "FloorSwitchers");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitcherPoint_FloorSwitcherId",
                table: "FloorSwitcherPoints",
                newName: "IX_FloorSwitcherPoints_FloorSwitcherId");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitcherPoint_FloorId",
                table: "FloorSwitcherPoints",
                newName: "IX_FloorSwitcherPoints_FloorId");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitcher_PlaceId",
                table: "FloorSwitchers",
                newName: "IX_FloorSwitchers_PlaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloorSwitcherPoints",
                table: "FloorSwitcherPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloorSwitchers",
                table: "FloorSwitchers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcherPoints_Floors_FloorId",
                table: "FloorSwitcherPoints",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcherPoints_FloorSwitchers_FloorSwitcherId",
                table: "FloorSwitcherPoints",
                column: "FloorSwitcherId",
                principalTable: "FloorSwitchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitchers_Places_PlaceId",
                table: "FloorSwitchers",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcherPoints_Floors_FloorId",
                table: "FloorSwitcherPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitcherPoints_FloorSwitchers_FloorSwitcherId",
                table: "FloorSwitcherPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorSwitchers_Places_PlaceId",
                table: "FloorSwitchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloorSwitchers",
                table: "FloorSwitchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloorSwitcherPoints",
                table: "FloorSwitcherPoints");

            migrationBuilder.RenameTable(
                name: "FloorSwitchers",
                newName: "FloorSwitcher");

            migrationBuilder.RenameTable(
                name: "FloorSwitcherPoints",
                newName: "FloorSwitcherPoint");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitchers_PlaceId",
                table: "FloorSwitcher",
                newName: "IX_FloorSwitcher_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitcherPoints_FloorSwitcherId",
                table: "FloorSwitcherPoint",
                newName: "IX_FloorSwitcherPoint_FloorSwitcherId");

            migrationBuilder.RenameIndex(
                name: "IX_FloorSwitcherPoints_FloorId",
                table: "FloorSwitcherPoint",
                newName: "IX_FloorSwitcherPoint_FloorId");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "FloorSwitcherPoint",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloorSwitcher",
                table: "FloorSwitcher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloorSwitcherPoint",
                table: "FloorSwitcherPoint",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FloorSwitcherPoint_PlaceId",
                table: "FloorSwitcherPoint",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcher_Places_PlaceId",
                table: "FloorSwitcher",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcherPoint_Floors_FloorId",
                table: "FloorSwitcherPoint",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcherPoint_FloorSwitcher_FloorSwitcherId",
                table: "FloorSwitcherPoint",
                column: "FloorSwitcherId",
                principalTable: "FloorSwitcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorSwitcherPoint_Places_PlaceId",
                table: "FloorSwitcherPoint",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
