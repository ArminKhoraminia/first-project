using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_UpdateDataBaseForCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseGroup_SubGroup",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisode_Course_CourseId",
                table: "CourseEpisode");

            migrationBuilder.DropIndex(
                name: "IX_Course_SubGroup",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "SubGroup",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "SubGroupId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroup_ParentId",
                table: "CourseGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubGroupId",
                table: "Course",
                column: "SubGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseGroup_SubGroupId",
                table: "Course",
                column: "SubGroupId",
                principalTable: "CourseGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisode_Course_CourseId",
                table: "CourseEpisode",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroup_CourseGroup_ParentId",
                table: "CourseGroup",
                column: "ParentId",
                principalTable: "CourseGroup",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseGroup_SubGroupId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisode_Course_CourseId",
                table: "CourseEpisode");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroup_CourseGroup_ParentId",
                table: "CourseGroup");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroup_ParentId",
                table: "CourseGroup");

            migrationBuilder.DropIndex(
                name: "IX_Course_SubGroupId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "SubGroupId",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "SubGroup",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubGroup",
                table: "Course",
                column: "SubGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseGroup_SubGroup",
                table: "Course",
                column: "SubGroup",
                principalTable: "CourseGroup",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisode_Course_CourseId",
                table: "CourseEpisode",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
