namespace ClothersShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "Ten");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Ten", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
