namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaylistAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title_AlbumID = c.Int(),
                    })
                .PrimaryKey(t => t.PlaylistID)
                .ForeignKey("dbo.Albums", t => t.Title_AlbumID)
                .Index(t => t.Title_AlbumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "Title_AlbumID", "dbo.Albums");
            DropIndex("dbo.Playlists", new[] { "Title_AlbumID" });
            DropTable("dbo.Playlists");
        }
    }
}
