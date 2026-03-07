using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_store.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Homepage",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Explanation", "Homepage", "Image" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "1.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Explanation", "Homepage", "Image" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "2.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Explanation", "Homepage", "Image" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "3.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Explanation", "Homepage", "Image" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", false, "4.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Explanation", "Homepage", "Image" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "5.jpeg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Explanation", "Homepage", "Image", "IsActive" },
                values: new object[] { " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "6.jpeg", false });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Explanation", "Homepage", "Image", "IsActive", "Price", "ProductName" },
                values: new object[,]
                {
                    { 7, " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", false, "7.jpeg", true, 60000.0, "Apple Watch 12" },
                    { 8, " Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ea, debitis blanditiis corrupti ducimus numquam saepe praesentium est sed! Repellendus architecto sit suscipit nobis at veritatis illum rem placeat debitis eius.", true, "8.jpeg", true, 65000.0, "Apple Watch 13" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Homepage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsActive",
                value: true);
        }
    }
}
