using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("b40f84d4-5f74-4e92-abc1-c92b9235364a"));

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("9ad5b9f4-4850-4ce8-b651-7bae3af71b51"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "Iw8vXlmgiw88hbpfyLKv4jj8/F/s62HpbgPgJN/v30h5QVBqZxESWyNy4pIq0c6E", "5452158345", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("9ad5b9f4-4850-4ce8-b651-7bae3af71b51"));

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("b40f84d4-5f74-4e92-abc1-c92b9235364a"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "0bJ5mzuGyfqz2/fefXSC9yg/xsICJlxX3JZZ2+m4alA9VOuRI6xBc9FGk3h18hDH", "5452158345", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }
    }
}
