namespace Music.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Many2Many : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Playlists", name: "Title_AlbumID", newName: "album_AlbumID");
            RenameIndex(table: "dbo.Playlists", name: "IX_Title_AlbumID", newName: "IX_album_AlbumID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Playlists", name: "IX_album_AlbumID", newName: "IX_Title_AlbumID");
            RenameColumn(table: "dbo.Playlists", name: "album_AlbumID", newName: "Title_AlbumID");
        }
    }
}
