using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class migggggtttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseLevel_CourseLevelLevelId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseStatus_CourseStatusStatusId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseLevelLevelId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseStatusStatusId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseLevelLevelId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseStatusStatusId",
                table: "Course");

            migrationBuilder.CreateIndex(
                name: "IX_Course_LevelId",
                table: "Course",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_StatusId",
                table: "Course",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseLevel_LevelId",
                table: "Course",
                column: "LevelId",
                principalTable: "CourseLevel",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseStatus_StatusId",
                table: "Course",
                column: "StatusId",
                principalTable: "CourseStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseLevel_LevelId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseStatus_StatusId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_LevelId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_StatusId",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "CourseLevelLevelId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseStatusStatusId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseLevelLevelId",
                table: "Course",
                column: "CourseLevelLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseStatusStatusId",
                table: "Course",
                column: "CourseStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseLevel_CourseLevelLevelId",
                table: "Course",
                column: "CourseLevelLevelId",
                principalTable: "CourseLevel",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseStatus_CourseStatusStatusId",
                table: "Course",
                column: "CourseStatusStatusId",
                principalTable: "CourseStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
