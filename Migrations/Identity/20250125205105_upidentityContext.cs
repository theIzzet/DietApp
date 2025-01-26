using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class upidentityContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EatingHabit_AspNetUsers_UserId",
                table: "EatingHabit");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_AspNetUsers_UserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifestyle_AspNetUsers_UserId",
                table: "Lifestyle");

            migrationBuilder.DropForeignKey(
                name: "FK_PastMedical_AspNetUsers_UserId",
                table: "PastMedical");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalInfo",
                table: "PersonalInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PastMedical",
                table: "PastMedical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lifestyle",
                table: "Lifestyle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EatingHabit",
                table: "EatingHabit");

            migrationBuilder.RenameTable(
                name: "PersonalInfo",
                newName: "PersonalInfos");

            migrationBuilder.RenameTable(
                name: "PastMedical",
                newName: "PastMedicals");

            migrationBuilder.RenameTable(
                name: "Lifestyle",
                newName: "Lifestyles");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameTable(
                name: "EatingHabit",
                newName: "EatingHabits");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfo_UserId",
                table: "PersonalInfos",
                newName: "IX_PersonalInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PastMedical_UserId",
                table: "PastMedicals",
                newName: "IX_PastMedicals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lifestyle_UserId",
                table: "Lifestyles",
                newName: "IX_Lifestyles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_UserId",
                table: "Goals",
                newName: "IX_Goals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EatingHabit_UserId",
                table: "EatingHabits",
                newName: "IX_EatingHabits_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PastMedicals",
                table: "PastMedicals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lifestyles",
                table: "Lifestyles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EatingHabits",
                table: "EatingHabits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EatingHabits_AspNetUsers_UserId",
                table: "EatingHabits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_AspNetUsers_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifestyles_AspNetUsers_UserId",
                table: "Lifestyles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PastMedicals_AspNetUsers_UserId",
                table: "PastMedicals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfos_AspNetUsers_UserId",
                table: "PersonalInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EatingHabits_AspNetUsers_UserId",
                table: "EatingHabits");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_AspNetUsers_UserId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Lifestyles_AspNetUsers_UserId",
                table: "Lifestyles");

            migrationBuilder.DropForeignKey(
                name: "FK_PastMedicals_AspNetUsers_UserId",
                table: "PastMedicals");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfos_AspNetUsers_UserId",
                table: "PersonalInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalInfos",
                table: "PersonalInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PastMedicals",
                table: "PastMedicals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lifestyles",
                table: "Lifestyles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EatingHabits",
                table: "EatingHabits");

            migrationBuilder.RenameTable(
                name: "PersonalInfos",
                newName: "PersonalInfo");

            migrationBuilder.RenameTable(
                name: "PastMedicals",
                newName: "PastMedical");

            migrationBuilder.RenameTable(
                name: "Lifestyles",
                newName: "Lifestyle");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameTable(
                name: "EatingHabits",
                newName: "EatingHabit");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalInfos_UserId",
                table: "PersonalInfo",
                newName: "IX_PersonalInfo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PastMedicals_UserId",
                table: "PastMedical",
                newName: "IX_PastMedical_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lifestyles_UserId",
                table: "Lifestyle",
                newName: "IX_Lifestyle_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_UserId",
                table: "Goal",
                newName: "IX_Goal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EatingHabits_UserId",
                table: "EatingHabit",
                newName: "IX_EatingHabit_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalInfo",
                table: "PersonalInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PastMedical",
                table: "PastMedical",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lifestyle",
                table: "Lifestyle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EatingHabit",
                table: "EatingHabit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EatingHabit_AspNetUsers_UserId",
                table: "EatingHabit",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_AspNetUsers_UserId",
                table: "Goal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lifestyle_AspNetUsers_UserId",
                table: "Lifestyle",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PastMedical_AspNetUsers_UserId",
                table: "PastMedical",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
