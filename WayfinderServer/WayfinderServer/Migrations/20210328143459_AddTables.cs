using Microsoft.EntityFrameworkCore.Migrations;

namespace WayfinderServer.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FloorPlan2Ds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorPlan2Ds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "floorPlan3Ds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floorPlan3Ds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VirtualObjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualObjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: true),
                    FloorPlan2DId = table.Column<int>(type: "int", nullable: true),
                    FloorPlan3DId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_FloorPlan2Ds_FloorPlan2DId",
                        column: x => x.FloorPlan2DId,
                        principalTable: "FloorPlan2Ds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floors_floorPlan3Ds_FloorPlan3DId",
                        column: x => x.FloorPlan3DId,
                        principalTable: "floorPlan3Ds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Floors_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<int>(type: "int", nullable: true),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloudAnchorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XCoordinate = table.Column<float>(type: "real", nullable: false),
                    YCoordinate = table.Column<float>(type: "real", nullable: false),
                    ZCoordinate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markers_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    FloorId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XCoordinate = table.Column<float>(type: "real", nullable: false),
                    YCoordinate = table.Column<float>(type: "real", nullable: false),
                    ZCoordinate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Targets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Targets_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VirtualObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    FileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XCoordinate = table.Column<float>(type: "real", nullable: false),
                    YCoordinate = table.Column<float>(type: "real", nullable: false),
                    ZCoordinate = table.Column<float>(type: "real", nullable: false),
                    XRotation = table.Column<float>(type: "real", nullable: false),
                    YRotation = table.Column<float>(type: "real", nullable: false),
                    ZRotation = table.Column<float>(type: "real", nullable: false),
                    XScale = table.Column<float>(type: "real", nullable: false),
                    YScale = table.Column<float>(type: "real", nullable: false),
                    ZScale = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualObjects_Targets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VirtualObjects_VirtualObjectTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "VirtualObjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PlaceId",
                table: "Categories",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorPlan2DId",
                table: "Floors",
                column: "FloorPlan2DId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_FloorPlan3DId",
                table: "Floors",
                column: "FloorPlan3DId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_PlaceId",
                table: "Floors",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_FloorId",
                table: "Markers",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_CategoryId",
                table: "Targets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_FloorId",
                table: "Targets",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualObjects_TargetId",
                table: "VirtualObjects",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualObjects_TypeId",
                table: "VirtualObjects",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropTable(
                name: "VirtualObjects");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "VirtualObjectTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "FloorPlan2Ds");

            migrationBuilder.DropTable(
                name: "floorPlan3Ds");
        }
    }
}
