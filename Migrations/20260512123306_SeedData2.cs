using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC02.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Degree", "DeptId", "Hours", "MinDegree", "Name" },
                values: new object[,]
                {
                    { 3, 100, 2, 40, 60, "CCNA" },
                    { 4, 100, 3, 20, 50, "Recruitment" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Manager", "Name" },
                values: new object[,]
                {
                    { 4, "Khaled", "Cyber Security" },
                    { 5, "Youssef", "AI" }
                });

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Crs_id",
                value: 3);

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "Id", "Address", "DeptId", "Grade", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 3, "Alex", 1, 4, "/Images/1.jpg", "Omar" },
                    { 4, "Mansoura", 3, 1, "/Images/1.jpg", "Mariam" },
                    { 7, "Giza", 2, 4, "/Images/1.jpg", "Karim" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Degree", "DeptId", "Hours", "MinDegree", "Name" },
                values: new object[,]
                {
                    { 5, 100, 4, 45, 65, "Ethical Hacking" },
                    { 6, 100, 5, 50, 70, "Machine Learning" }
                });

            migrationBuilder.InsertData(
                table: "CrsResults",
                columns: new[] { "Id", "Crs_Id", "Degree", "TraineeId" },
                values: new object[,]
                {
                    { 3, 1, 75, 3 },
                    { 4, 3, 60, 7 },
                    { 5, 4, 88, 4 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Address", "Crs_id", "DeptId", "ImageUrl", "Name", "Salary" },
                values: new object[] { 4, "Cairo", 4, 3, "/Images/1.jpg", "Mahmoud", 11000m });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "Id", "Address", "DeptId", "Grade", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 5, "Tanta", 4, 2, "/Images/1.jpg", "Yassin" },
                    { 6, "Cairo", 5, 3, "/Images/1.jpg", "Salma" },
                    { 8, "Alex", 5, 1, "/Images/1.jpg", "Nada" }
                });

            migrationBuilder.InsertData(
                table: "CrsResults",
                columns: new[] { "Id", "Crs_Id", "Degree", "TraineeId" },
                values: new object[,]
                {
                    { 6, 5, 95, 5 },
                    { 7, 6, 70, 6 },
                    { 8, 6, 98, 8 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Address", "Crs_id", "DeptId", "ImageUrl", "Name", "Salary" },
                values: new object[,]
                {
                    { 5, "Tanta", 5, 4, "/Images/1.jpg", "Yara", 12000m },
                    { 6, "Giza", 6, 5, "/Images/1.jpg", "Mostafa", 15000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CrsResults",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trainees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Crs_id",
                value: 2);
        }
    }
}
