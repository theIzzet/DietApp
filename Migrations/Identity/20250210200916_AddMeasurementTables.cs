using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddMeasurementTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonalInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Waist = table.Column<double>(type: "REAL", nullable: true),
                    Hips = table.Column<double>(type: "REAL", nullable: true),
                    Chest = table.Column<double>(type: "REAL", nullable: true),
                    UpperArm = table.Column<double>(type: "REAL", nullable: true),
                    Leg = table.Column<double>(type: "REAL", nullable: true),
                    Neck = table.Column<double>(type: "REAL", nullable: true),
                    PhotoPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementIssueReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonalInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeasurementType = table.Column<string>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovedByDietitian = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementIssueReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementIssueReports_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonalInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    PhotoPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightMeasurements_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_PersonalInfoId",
                table: "BodyMeasurements",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementIssueReports_PersonalInfoId",
                table: "MeasurementIssueReports",
                column: "PersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightMeasurements_PersonalInfoId",
                table: "WeightMeasurements",
                column: "PersonalInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");

            migrationBuilder.DropTable(
                name: "MeasurementIssueReports");

            migrationBuilder.DropTable(
                name: "WeightMeasurements");
        }
    }
}
