using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations
{
    /// <inheritdoc />
    public partial class PatientMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SurName = table.Column<string>(type: "TEXT", nullable: false),
                    GraduationSertificatePath = table.Column<string>(type: "TEXT", nullable: true),
                    TranskriptPath = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    AlergyName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allergies_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DiseaseName = table.Column<string>(type: "TEXT", nullable: true),
                    DiseaseYear = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EatingHabits",
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
                    table.PrimaryKey("PK_EatingHabits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EatingHabits_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DiseaseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyDiseases_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
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
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lifestyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    StressLevel = table.Column<string>(type: "TEXT", nullable: true),
                    SmokingHabits = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfSmokingPackage = table.Column<string>(type: "TEXT", nullable: true),
                    SmokingUtilezeYear = table.Column<string>(type: "TEXT", nullable: true),
                    AlcoholConsumption = table.Column<string>(type: "TEXT", nullable: true),
                    CaffeineIntake = table.Column<string>(type: "TEXT", nullable: true),
                    MotivationLevel = table.Column<string>(type: "TEXT", nullable: true),
                    SocialSupport = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifestyles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lifestyles_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    MedicationName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
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
                    table.PrimaryKey("PK_PersonalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalInfos_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
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
                        name: "FK_PhysicalActivityStatus_DietUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DietUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_UserId",
                table: "Allergies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_UserId",
                table: "Diseases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EatingHabits_UserId",
                table: "EatingHabits",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyDiseases_UserId",
                table: "FamilyDiseases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lifestyles_UserId",
                table: "Lifestyles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_UserId",
                table: "Medications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfos_UserId",
                table: "PersonalInfos",
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
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "EatingHabits");

            migrationBuilder.DropTable(
                name: "FamilyDiseases");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Lifestyles");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "PhysicalActivityStatus");

            migrationBuilder.DropTable(
                name: "DietUser");
        }
    }
}
