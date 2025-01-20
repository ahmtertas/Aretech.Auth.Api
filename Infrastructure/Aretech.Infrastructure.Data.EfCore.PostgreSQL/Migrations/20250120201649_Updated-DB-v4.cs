using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationMappings_Account_AccountId",
                table: "AccountApplicationMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationMappings_Applications_ApplicationId",
                table: "AccountApplicationMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountLoginFailHistories_Account_AccountId",
                table: "AccountLoginFailHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountLoginHistories_Account_AccountId",
                table: "AccountLoginHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PasswordHistories_Account_AccountId",
                table: "PasswordHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetRequests_Account_AccountId",
                table: "PasswordResetRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Account_AccountId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResetRequests",
                table: "PasswordResetRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordHistories",
                table: "PasswordHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountLoginHistories",
                table: "AccountLoginHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountLoginFailHistories",
                table: "AccountLoginFailHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountApplicationMappings",
                table: "AccountApplicationMappings");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("79c926d0-9f90-420f-ad97-499fd8177211"));

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "PasswordResetRequests",
                newName: "PasswordResetRequest");

            migrationBuilder.RenameTable(
                name: "PasswordHistories",
                newName: "PasswordHistory");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameTable(
                name: "AccountLoginHistories",
                newName: "AccountLoginHistory");

            migrationBuilder.RenameTable(
                name: "AccountLoginFailHistories",
                newName: "AccountLoginFailHistory");

            migrationBuilder.RenameTable(
                name: "AccountApplicationMappings",
                newName: "AccountApplicationMapping");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetRequests_AccountId",
                table: "PasswordResetRequest",
                newName: "IX_PasswordResetRequest_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordHistories_AccountId",
                table: "PasswordHistory",
                newName: "IX_PasswordHistory_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountLoginHistories_AccountId",
                table: "AccountLoginHistory",
                newName: "IX_AccountLoginHistory_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountLoginFailHistories_AccountId",
                table: "AccountLoginFailHistory",
                newName: "IX_AccountLoginFailHistory_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountApplicationMappings_ApplicationId",
                table: "AccountApplicationMapping",
                newName: "IX_AccountApplicationMapping_ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountApplicationMappings_AccountId",
                table: "AccountApplicationMapping",
                newName: "IX_AccountApplicationMapping_AccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "RefreshToken",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "PasswordResetRequest",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "PasswordResetRequest",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PasswordResetRequest",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "PasswordHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "PasswordHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PasswordHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Application",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Application",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Application",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountLoginHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountLoginHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountLoginHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountLoginFailHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountLoginFailHistory",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountLoginFailHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountApplicationMapping",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountApplicationMapping",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountApplicationMapping",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "RefreshToken_pkey",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PasswordResetRequest_pkey",
                table: "PasswordResetRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PasswordHistory_pkey",
                table: "PasswordHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Application_pkey",
                table: "Application",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "AccountLoginHistory_pkey",
                table: "AccountLoginHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "AccountLoginFailHistory_pkey",
                table: "AccountLoginFailHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "AccountApplicationMapping_pkey",
                table: "AccountApplicationMapping",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("8bb39c8d-e8c6-4221-9421-f24de0167ba4"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "K1YiCfuWsQpOso6SjRoLwR6a8/eFoeAghr7NYGpKUz7aaq77mQusBR6y0O8k0vgi", "5452158345", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationMapping_Account_AccountId",
                table: "AccountApplicationMapping",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationMapping_Application_ApplicationId",
                table: "AccountApplicationMapping",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountLoginFailHistory_Account_AccountId",
                table: "AccountLoginFailHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountLoginHistory_Account_AccountId",
                table: "AccountLoginHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordHistory_Account_AccountId",
                table: "PasswordHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetRequest_Account_AccountId",
                table: "PasswordResetRequest",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationMapping_Account_AccountId",
                table: "AccountApplicationMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountApplicationMapping_Application_ApplicationId",
                table: "AccountApplicationMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountLoginFailHistory_Account_AccountId",
                table: "AccountLoginFailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountLoginHistory_Account_AccountId",
                table: "AccountLoginHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PasswordHistory_Account_AccountId",
                table: "PasswordHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PasswordResetRequest_Account_AccountId",
                table: "PasswordResetRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "RefreshToken_pkey",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PasswordResetRequest_pkey",
                table: "PasswordResetRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PasswordHistory_pkey",
                table: "PasswordHistory");

            migrationBuilder.DropPrimaryKey(
                name: "Application_pkey",
                table: "Application");

            migrationBuilder.DropPrimaryKey(
                name: "AccountLoginHistory_pkey",
                table: "AccountLoginHistory");

            migrationBuilder.DropPrimaryKey(
                name: "AccountLoginFailHistory_pkey",
                table: "AccountLoginFailHistory");

            migrationBuilder.DropPrimaryKey(
                name: "AccountApplicationMapping_pkey",
                table: "AccountApplicationMapping");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: new Guid("8bb39c8d-e8c6-4221-9421-f24de0167ba4"));

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "PasswordResetRequest",
                newName: "PasswordResetRequests");

            migrationBuilder.RenameTable(
                name: "PasswordHistory",
                newName: "PasswordHistories");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameTable(
                name: "AccountLoginHistory",
                newName: "AccountLoginHistories");

            migrationBuilder.RenameTable(
                name: "AccountLoginFailHistory",
                newName: "AccountLoginFailHistories");

            migrationBuilder.RenameTable(
                name: "AccountApplicationMapping",
                newName: "AccountApplicationMappings");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetRequest_AccountId",
                table: "PasswordResetRequests",
                newName: "IX_PasswordResetRequests_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordHistory_AccountId",
                table: "PasswordHistories",
                newName: "IX_PasswordHistories_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountLoginHistory_AccountId",
                table: "AccountLoginHistories",
                newName: "IX_AccountLoginHistories_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountLoginFailHistory_AccountId",
                table: "AccountLoginFailHistories",
                newName: "IX_AccountLoginFailHistories_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountApplicationMapping_ApplicationId",
                table: "AccountApplicationMappings",
                newName: "IX_AccountApplicationMappings_ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountApplicationMapping_AccountId",
                table: "AccountApplicationMappings",
                newName: "IX_AccountApplicationMappings_AccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "PasswordResetRequests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "PasswordResetRequests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PasswordResetRequests",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "PasswordHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "PasswordHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PasswordHistories",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Applications",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "Applications",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Applications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountLoginHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountLoginHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountLoginHistories",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountLoginFailHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountLoginFailHistories",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountLoginFailHistories",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "AccountApplicationMappings",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "AccountApplicationMappings",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AccountApplicationMappings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResetRequests",
                table: "PasswordResetRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordHistories",
                table: "PasswordHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountLoginHistories",
                table: "AccountLoginHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountLoginFailHistories",
                table: "AccountLoginFailHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountApplicationMappings",
                table: "AccountApplicationMappings",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "FailedLoginAttempts", "FirstName", "IdentityNumber", "IsActived", "IsVerified", "LastLogin", "LastName", "LockoutEnd", "PasswordHash", "PhoneNumber", "TwoFactorEnabled", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("79c926d0-9f90-420f-ad97-499fd8177211"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "ar3t3ch@gmail.com", 0, "Süper", "", true, false, null, "Admin", null, "4fqOYKwFogSoYviY1wS9onqBX87SYNu9iciB4w6H8w6gn0cKylei80HoZudEi81u", "5452158345", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "superadmin" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationMappings_Account_AccountId",
                table: "AccountApplicationMappings",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountApplicationMappings_Applications_ApplicationId",
                table: "AccountApplicationMappings",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountLoginFailHistories_Account_AccountId",
                table: "AccountLoginFailHistories",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountLoginHistories_Account_AccountId",
                table: "AccountLoginHistories",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordHistories_Account_AccountId",
                table: "PasswordHistories",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordResetRequests_Account_AccountId",
                table: "PasswordResetRequests",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Account_AccountId",
                table: "RefreshTokens",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
