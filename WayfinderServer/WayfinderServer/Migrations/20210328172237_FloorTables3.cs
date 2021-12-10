using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class FloorTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_floorPlan3Ds_Floors_FloorId",
                table: "floorPlan3Ds");

            migrationBuilder.DropForeignKey(
                name: "FK_Floors_FloorPlan2Ds_FloorPlan2DId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_FloorPlan2DId",
                table: "Floors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_floorPlan3Ds",
                table: "floorPlan3Ds");

            migrationBuilder.DropColumn(
                name: "FloorPlan2DId",
                table: "Floors");

            migrationBuilder.RenameTable(
                name: "floorPlan3Ds",
                newName: "FloorPlan3Ds");

            migrationBuilder.RenameIndex(
                name: "IX_floorPlan3Ds_FloorId",
                table: "FloorPlan3Ds",
                newName: "IX_FloorPlan3Ds_FloorId");

            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "FloorPlan2Ds",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FloorPlan3Ds",
                table: "FloorPlan3Ds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FloorPlan2Ds_FloorId",
                table: "FloorPlan2Ds",
                column: "FloorId",
                unique: true,
                filter: "[FloorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FloorPlan2Ds_Floors_FloorId",
                table: "FloorPlan2Ds",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FloorPlan3Ds_Floors_FloorId",
                table: "FloorPlan3Ds",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FloorPlan2Ds_Floors_FloorId",
                table: "FloorPlan2Ds");

            migrationBuilder.DropForeignKey(
                name: "FK_FloorPlan3Ds_Floors_FloorId",
                table: "FloorPlan3Ds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FloorPlan3Ds",
                table: "FloorPlan3Ds");

            migrationBuilder.DropIndex(
                name: "IX_FloorPlan2Ds_FloorId",
                table: "FloorPlan2Ds");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "FloorPlan2Ds");

            migrationBuilder.RenameTable(
                name: "FloorPlan3Ds",
                newName: "floorPlan3Ds");

            migrationBuilder.RenameIndex(
                name: "IX_FloorPlan3Ds_FloorId",
                table: "floorPlan3Ds",
                newName: "IX_floorPlan3Ds_FloorId");

            migrationBuilder.AddColumn<int>(
                name: "FloorPlan2DId",
                table: "Floors",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_floorPlan3Ds",
                table: "floorPlan3Ds",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorPlan2DId",
                table: "Floors",
                column: "FloorPlan2DId");

            migrationBuilder.AddForeignKey(
                name: "FK_floorPlan3Ds_Floors_FloorId",
                table: "floorPlan3Ds",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_FloorPlan2Ds_FloorPlan2DId",
                table: "Floors",
                column: "FloorPlan2DId",
                principalTable: "FloorPlan2Ds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
