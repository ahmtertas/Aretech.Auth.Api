using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("e9e717ef-9834-4607-8b22-40fc370993bc"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Account",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("680acf89-2a66-4df7-bf83-16e6fea581c7"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "QOlIHAAu+2ppM2D4n5nLElnZDbTtIDi7w4RxagcCEwk3bUPzFam4rGQFLoyT1c5G", "5452158345", null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("680acf89-2a66-4df7-bf83-16e6fea581c7"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Account");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("e9e717ef-9834-4607-8b22-40fc370993bc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "S98gDQzKlfUkVEvprKEQsbeHrEJMyZlcUkC0z8LsZkeAVftDR9kqWvxIaa3WpR6T", "5452158345", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }
    }
}
