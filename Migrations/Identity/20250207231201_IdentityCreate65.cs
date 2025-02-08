using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class IdentityCreate65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientInfo");

            migrationBuilder.AddColumn<int>(
                name: "DiyetisyenId",
                table: "PersonalInfos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DietLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonalInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietLists_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_DiyetisyenId",
                table: "PersonalInfos",
                column: "DiyetisyenId");

            migrationBuilder.CreateIndex(
                name: "IX_DietLists_PersonalInfoId",
                table: "DietLists",
                column: "PersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_DiyetisyenProfiles_DiyetisyenId",
                table: "PersonalInfos",
                column: "DiyetisyenId",
                principalTable: "DiyetisyenProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_DiyetisyenProfiles_DiyetisyenId",
                table: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "DietLists");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfos_DiyetisyenId",
                table: "PersonalInfos");

            migrationBuilder.DropColumn(
                name: "DiyetisyenId",
                table: "PersonalInfos");

            migrationBuilder.CreateTable(
                name: "PatientInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiyetisyenId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AlcoholConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    CaffeineIntake = table.Column<string>(type: "TEXT", nullable: true),
                    ConsumedFoods = table.Column<string>(type: "TEXT", nullable: true),
                    ContactInformation = table.Column<string>(type: "TEXT", nullable: true),
                    CookingMethod = table.Column<string>(type: "TEXT", nullable: true),
                    DailyInactivity = table.Column<string>(type: "TEXT", nullable: true),
                    DailyMealCount = table.Column<int>(type: "INTEGER", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DessertConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    EatingDuration = table.Column<string>(type: "TEXT", nullable: true),
                    EatingOutHabits = table.Column<string>(type: "TEXT", nullable: true),
                    FoodIntolerances = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    HealthIssuesManagement = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    MaritalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    MealTimes = table.Column<string>(type: "TEXT", nullable: true),
                    MotivationLevel = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfChildren = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfSmokingPackage = table.Column<string>(type: "TEXT", nullable: true),
                    Occupation = table.Column<string>(type: "TEXT", nullable: true),
                    OtherGoals = table.Column<string>(type: "TEXT", nullable: true),
                    RegularPhysicalActivity = table.Column<string>(type: "TEXT", nullable: true),
                    SleepPattern = table.Column<string>(type: "TEXT", nullable: true),
                    SmokingUtilezeYear = table.Column<string>(type: "TEXT", nullable: true),
                    SnackingHabits = table.Column<string>(type: "TEXT", nullable: true),
                    SocialSupport = table.Column<string>(type: "TEXT", nullable: true),
                    SportsPerformanceGoals = table.Column<string>(type: "TEXT", nullable: true),
                    StressLevel = table.Column<string>(type: "TEXT", nullable: true),
                    SurName = table.Column<string>(type: "TEXT", nullable: true),
                    WaterConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: true),
                    WeightGoal = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientInfo_DiyetisyenProfiles_DiyetisyenId",
                        column: x => x.DiyetisyenId,
                        principalTable: "DiyetisyenProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientInfo_DiyetisyenId",
                table: "PatientInfo",
                column: "DiyetisyenId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInfo_UserId",
                table: "PatientInfo",
                column: "UserId");
        }
    }
}
