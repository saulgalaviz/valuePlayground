using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedValueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Values",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem dolor, hendrerit ut justo sit amet, dictum facilisis magna. Nullam pulvinar tempus leo, sit amet dapibus nisi convallis et. Pellentesque sodales vestibulum quam quis laoreet. Aenean rhoncus est nec consectetur.", "https://whvn.cc/47y633", "Lagoon", 8, 200.0, 850, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phasellus tempor sapien vitae ullamcorper gravida. Aenean eget pulvinar risus. Cras molestie ligula nibh. Sed luctus condimentum leo, nec tristique tellus vulputate id. Duis semper lorem sed lacus iaculis, ac fringilla mi vestibulum. Cras vel nunc mollis, sagittis nunc ac.", "https://whvn.cc/47y633", "Park", 4, 600.0, 1500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nullam bibendum lorem sed augue accumsan tempus. Ut quis bibendum eros, at malesuada odio. Duis congue dolor tincidunt orci sodales, id sagittis ante ultrices. Vivamus dapibus magna vel mi malesuada varius. Morbi volutpat eu metus dictum pretium. Proin quis tellus.", "https://whvn.cc/47y633", "Vegas Room", 12, 2000.0, 600, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Morbi mattis dolor mattis, faucibus quam sit amet, vestibulum orci. Quisque congue purus non ligula condimentum, at iaculis tellus faucibus. Proin eu diam sit amet diam finibus feugiat et vitae nisi. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed.", "https://whvn.cc/47y633", "Maui Island Resort", 2, 1000.0, 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In quis mattis erat. Mauris pellentesque diam eu volutpat sagittis. Mauris ullamcorper, elit id consectetur euismod, lacus mauris tincidunt massa, ac rhoncus risus lorem eget lectus. Aliquam erat volutpat. Maecenas efficitur, nulla nec volutpat congue, magna justo interdum nulla, at.", "https://whvn.cc/47y633", "Detroit Condo", 6, 800.0, 800, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Values",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Values",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Values",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Values",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Values",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
