namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Many2Many1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Playlist_PlaylistID", c => c.Int());
            CreateIndex("dbo.Albums", "Playlist_PlaylistID");
            AddForeignKey("dbo.Albums", "Playlist_PlaylistID", "dbo.Playlists", "PlaylistID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "album_AlbumID", c => c.Int());
            DropForeignKey("dbo.Albums", "Playlist_PlaylistID", "dbo.Playlists");
            DropIndex("dbo.Albums", new[] { "Playlist_PlaylistID" });
            DropColumn("dbo.Albums", "Playlist_PlaylistID");
            CreateIndex("dbo.Playlists", "album_AlbumID");
            AddForeignKey("dbo.Playlists", "album_AlbumID", "dbo.Albums", "AlbumID");
        }
    }
}
