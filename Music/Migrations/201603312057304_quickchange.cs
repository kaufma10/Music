namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quickchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Playlists", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Playlists", "Name", c => c.String(nullable: false));
        }
    }
}
