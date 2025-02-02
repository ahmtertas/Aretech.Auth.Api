using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBv8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("741e5fe8-a6d7-4f3d-ad52-3a4daeb2b567"));

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "PasswordResetRequest");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("1ea5d923-0981-4b8f-8305-52e659ae5e9e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "4sKYmvhaOPtHQGFmcBHT6cZGutOT1ubsFYkBhESp5nXp8JskZr21ztYRJE8asyd4", "5452158345", null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("1ea5d923-0981-4b8f-8305-52e659ae5e9e"));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "PasswordResetRequest",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("741e5fe8-a6d7-4f3d-ad52-3a4daeb2b567"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "qDvusClNeMP1NicA8W7O5H4GniDZ2IXseR0DVu0tdBqR71k8Ww8WvD4k3x+Ah1m2", "5452158345", null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }
    }
}
