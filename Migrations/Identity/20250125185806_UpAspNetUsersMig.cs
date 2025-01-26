using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class UpAspNetUsersMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TranskriptPath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "GraduationSertificatePath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "EatingHabit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DailyMealCount = table.Column<int>(type: "INTEGER", nullable: true),
                    MealTimes = table.Column<string>(type: "TEXT", nullable: true),
                    ConsumedFoods = table.Column<string>(type: "TEXT", nullable: true),
                    SnackingHabits = table.Column<string>(type: "TEXT", nullable: true),
                    WaterConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    FoodIntolerances = table.Column<string>(type: "TEXT", nullable: true),
                    CookingMethod = table.Column<string>(type: "TEXT", nullable: true),
                    EatingDuration = table.Column<string>(type: "TEXT", nullable: true),
                    EatingOutHabits = table.Column<string>(type: "TEXT", nullable: true),
                    DessertConsumption = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EatingHabit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EatingHabit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    WeightGoal = table.Column<string>(type: "TEXT", nullable: true),
                    HealthIssuesManagement = table.Column<string>(type: "TEXT", nullable: true),
                    SportsPerformanceGoals = table.Column<string>(type: "TEXT", nullable: true),
                    OtherGoals = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lifestyle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    StressLevel = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfSmokingPackage = table.Column<string>(type: "TEXT", nullable: true),
                    SmokingUtilezeYear = table.Column<string>(type: "TEXT", nullable: true),
                    AlcoholConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    CaffeineIntake = table.Column<string>(type: "TEXT", nullable: true),
                    MotivationLevel = table.Column<string>(type: "TEXT", nullable: true),
                    SocialSupport = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifestyle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifestyle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PastMedical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AllergyName = table.Column<string>(type: "TEXT", nullable: true),
                    DiseaseName = table.Column<string>(type: "TEXT", nullable: true),
                    FamilyDiseaseName = table.Column<string>(type: "TEXT", nullable: true),
                    MedicationName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastMedical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PastMedical_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SurName = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: true),
                    ContactInformation = table.Column<string>(type: "TEXT", nullable: true),
                    Occupation = table.Column<string>(type: "TEXT", nullable: true),
                    MaritalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfChildren = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalActivityStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RegularPhysicalActivity = table.Column<string>(type: "TEXT", nullable: true),
                    DailyInactivity = table.Column<string>(type: "TEXT", nullable: true),
                    SleepPattern = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivityStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalActivityStatus_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EatingHabit_UserId",
                table: "EatingHabit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goal_UserId",
                table: "Goal",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lifestyle_UserId",
                table: "Lifestyle",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PastMedical_UserId",
                table: "PastMedical",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfo_UserId",
                table: "PersonalInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityStatus_UserId",
                table: "PhysicalActivityStatus",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EatingHabit");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "Lifestyle");

            migrationBuilder.DropTable(
                name: "PastMedical");

            migrationBuilder.DropTable(
                name: "PersonalInfo");

            migrationBuilder.DropTable(
                name: "PhysicalActivityStatus");

            migrationBuilder.AlterColumn<string>(
                name: "TranskriptPath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GraduationSertificatePath",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
