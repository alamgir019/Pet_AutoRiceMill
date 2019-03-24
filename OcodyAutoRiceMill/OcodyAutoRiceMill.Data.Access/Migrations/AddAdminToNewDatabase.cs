using System.Collections.Generic;
using System.Linq;
using OcodyAutoRiceMill.Data.Access.Constants;
using OcodyAutoRiceMill.Data.Access.DAL;
using OcodyAutoRiceMill.Data.Access.Helpers;
using OcodyAutoRiceMill.Data.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OcodyAutoRiceMill.Data.Access.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("CustomMigration_002_AddAdminToNewDatabase")]
    public class AddAdminToNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminPassword = "admin".WithBCrypt();
            migrationBuilder.Sql($@"
DECLARE @password NVARCHAR(MAX) = N'{adminPassword}'

INSERT INTO dbo.Users
        ( FirstName ,
          LastName ,
          Password ,
          Username ,
          IsDeleted
        )
VALUES  ( N'admin' , -- FirstName - nvarchar(max)
          N'admin' , -- LastName - nvarchar(max)
          @password, -- Password - nvarchar(max)
          N'admin' , -- Username - nvarchar(max)
          0  -- IsDeleted - bit
        )

DECLARE @adminId INT = @@IDENTITY

INSERT INTO dbo.UserRoles
        ( RoleId, UserId )
SELECT Id, @adminId FROM dbo.Roles
WHERE Name = 'Administrator'
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE UR
FROM dbo.Users U
JOIN dbo.UserRoles UR ON UR.UserId = U.Id
WHERE U.Username = 'admin'

DELETE U
FROM dbo.Users U
WHERE U.Username = 'admin'
");
        }
    }
}