using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBv9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetRequest_Account_AccountId",
                table: "PasswordResetRequest");

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetRequest_AccountId",
                table: "PasswordResetRequest");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("1ea5d923-0981-4b8f-8305-52e659ae5e9e"));

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "PasswordResetRequest");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("15702382-0bbf-4474-a422-4616da24d240"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "TQ/z5tTLQABTnZfmzOT1Zi5tyStSAVVaAEz/q7QBmWH1/1ue0tzrXlzhV93VrNzQ", "5452158345", null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("15702382-0bbf-4474-a422-4616da24d240"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "PasswordResetRequest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("1ea5d923-0981-4b8f-8305-52e659ae5e9e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "4sKYmvhaOPtHQGFmcBHT6cZGutOT1ubsFYkBhESp5nXp8JskZr21ztYRJE8asyd4", "5452158345", null, null, false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetRequest_AccountId",
                table: "PasswordResetRequest",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetRequest_Account_AccountId",
                table: "PasswordResetRequest",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
