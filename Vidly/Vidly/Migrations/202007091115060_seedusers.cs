namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedusers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'089b3057-b242-4a24-9593-fb717fd17b67', N'admin@vidly.com', 0, N'AARzzZ9f3kI6ybdN+4HS/SUImSF6iwNlIkANrwGqN9iblwTq0HE2+6MoSh62FtmTsg==', N'a920ba17-d70d-4f13-9de1-6ff662ba471c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e24bf01c-79ba-4979-b16f-d85b5000b24f', N'guest@vidly.com', 0, N'AJXjIU+B65XdZVto0+uAJxp1D6eV7JnfmvBUSmPOYvlHmBPc7KAC0Vtnqvh9PhDteA==', N'5eeed021-3515-448d-b751-47d1b8fdf0bb', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9e4e22a1-aee3-466c-9bc0-44faf164c286', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'089b3057-b242-4a24-9593-fb717fd17b67', N'9e4e22a1-aee3-466c-9bc0-44faf164c286')

");
        }
        
        public override void Down()
        {
        }
    }
}
