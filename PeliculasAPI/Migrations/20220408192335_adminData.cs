using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class adminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'd645ff90-5eea-45bc-809e-1d1fcbc41edd', N'58329df0-0af2-423a-b638-c1e32fdf6fa9', N'Admin', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'c55de8ff-e649-4d8b-9463-b90e8dc2dfe5', 0, N'7f755361-ba0b-4dc6-b85a-4478593a1ff7', N'jnicolasbecerraq@gmail.com', CAST(0 AS bit), CAST(0 AS bit), NULL, N'jnicolasbecerraq@gmail.com', N'jnicolasbecerraq@gmail.com', N'AQAAAAEAACcQAAAAEMgddmZ6UMTGzKXMQpfv+DQRvVHUoApnHi2jxiGJ/ZohN/BtwbyE9M3RsRhvce0ESg==', NULL, CAST(0 AS bit), N'c9d0059a-28e5-4098-8b3b-54f010e6d3c6', CAST(0 AS bit), N'jnicolasbecerraq@gmail.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] ON;
INSERT INTO [AspNetUserClaims] ([Id], [ClaimType], [ClaimValue], [UserId])
VALUES (1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin', N'c55de8ff-e649-4d8b-9463-b90e8dc2dfe5');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
VALUES (N'd645ff90-5eea-45bc-809e-1d1fcbc41edd', N'c55de8ff-e649-4d8b-9463-b90e8dc2dfe5');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                            table: "AspNetUserClaims",
                            keyColumn: "Id",
                            keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d645ff90-5eea-45bc-809e-1d1fcbc41edd", "c55de8ff-e649-4d8b-9463-b90e8dc2dfe5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d645ff90-5eea-45bc-809e-1d1fcbc41edd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c55de8ff-e649-4d8b-9463-b90e8dc2dfe5");


        }
    }
}
