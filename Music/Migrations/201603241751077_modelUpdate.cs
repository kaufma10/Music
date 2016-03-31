namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Artists", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Bio");
            DropColumn("dbo.Albums", "Likes");
        }
    }
}
