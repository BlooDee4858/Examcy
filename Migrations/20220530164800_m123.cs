using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examcy.Migrations
{
    public partial class m123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessions_users_UserId",
                table: "sessions");

            migrationBuilder.DropIndex(
                name: "IX_sessions_UserId",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "sessions");

            migrationBuilder.AddColumn<int>(
                name: "teacherId",
                table: "teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IdSession",
                table: "sessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsCommonGroup",
                table: "groupCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "assignedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignedTasks_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignedTasks_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "setTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_setTasks_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vars_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "setTasksAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    AnswersId = table.Column<int>(type: "int", nullable: false),
                    SetTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setTasksAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_setTasksAnswers_answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_setTasksAnswers_setTasks_SetTasksId",
                        column: x => x.SetTasksId,
                        principalTable: "setTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assignedVars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VarId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedVars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignedVars_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignedVars_vars_VarId",
                        column: x => x.VarId,
                        principalTable: "vars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentVars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    VarId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentVars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_studentVars_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentVars_vars_VarId",
                        column: x => x.VarId,
                        principalTable: "vars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taskInVar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numberTaskInVar = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    VarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskInVar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_taskInVar_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskInVar_vars_VarId",
                        column: x => x.VarId,
                        principalTable: "vars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "solvedTaskInVars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedVarId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solvedTaskInVars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_solvedTaskInVars_answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solvedTaskInVars_assignedVars_AssignedVarId",
                        column: x => x.AssignedVarId,
                        principalTable: "assignedVars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_teacherId",
                table: "teachers",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedTasks_StudentId",
                table: "assignedTasks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedTasks_TaskId",
                table: "assignedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedVars_StudentId",
                table: "assignedVars",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_assignedVars_VarId",
                table: "assignedVars",
                column: "VarId");

            migrationBuilder.CreateIndex(
                name: "IX_setTasks_TaskId",
                table: "setTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_setTasksAnswers_AnswersId",
                table: "setTasksAnswers",
                column: "AnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_setTasksAnswers_SetTasksId",
                table: "setTasksAnswers",
                column: "SetTasksId");

            migrationBuilder.CreateIndex(
                name: "IX_solvedTaskInVars_AnswerId",
                table: "solvedTaskInVars",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_solvedTaskInVars_AssignedVarId",
                table: "solvedTaskInVars",
                column: "AssignedVarId");

            migrationBuilder.CreateIndex(
                name: "IX_studentVars_StudentId",
                table: "studentVars",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_studentVars_VarId",
                table: "studentVars",
                column: "VarId");

            migrationBuilder.CreateIndex(
                name: "IX_taskInVar_TaskId",
                table: "taskInVar",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_taskInVar_VarId",
                table: "taskInVar",
                column: "VarId");

            migrationBuilder.CreateIndex(
                name: "IX_vars_CourseId",
                table: "vars",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_teachers_teacherId",
                table: "teachers",
                column: "teacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_teachers_teacherId",
                table: "teachers");

            migrationBuilder.DropTable(
                name: "assignedTasks");

            migrationBuilder.DropTable(
                name: "setTasksAnswers");

            migrationBuilder.DropTable(
                name: "solvedTaskInVars");

            migrationBuilder.DropTable(
                name: "studentVars");

            migrationBuilder.DropTable(
                name: "taskInVar");

            migrationBuilder.DropTable(
                name: "setTasks");

            migrationBuilder.DropTable(
                name: "assignedVars");

            migrationBuilder.DropTable(
                name: "vars");

            migrationBuilder.DropIndex(
                name: "IX_teachers_teacherId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "teacherId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "IsCommonGroup",
                table: "groupCourses");

            migrationBuilder.AlterColumn<int>(
                name: "IdSession",
                table: "sessions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sessions_UserId",
                table: "sessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_users_UserId",
                table: "sessions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
