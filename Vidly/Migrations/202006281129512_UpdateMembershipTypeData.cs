namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeData : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[MembershipTypes] SET  [Name] = 'pay as you go' WHERE Id = 1");
            Sql("UPDATE [dbo].[MembershipTypes] SET  [Name] = 'Monthly' WHERE Id = 2");
            Sql("UPDATE [dbo].[MembershipTypes] SET  [Name] = 'Quartely' WHERE Id = 3");
            Sql("UPDATE [dbo].[MembershipTypes] SET  [Name] = 'Yearly' WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
