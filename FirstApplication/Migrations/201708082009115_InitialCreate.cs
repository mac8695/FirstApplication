namespace FirstApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(nullable: false, maxLength: 128),
                        Rank = c.Decimal(nullable: false, precision: 18, scale: 0),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsMultiplayer = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        GameGenreId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        GenreId = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GameGenreId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.GenreId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameGenres", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameGenres", "GenreId", "dbo.Genres");
            DropIndex("dbo.GameGenres", new[] { "GameId" });
            DropIndex("dbo.GameGenres", new[] { "GenreId" });
            DropIndex("dbo.Ratings", new[] { "GameId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropTable("dbo.Genres");
            DropTable("dbo.GameGenres");
            DropTable("dbo.Games");
            DropTable("dbo.Ratings");
            DropTable("dbo.AspNetUsers");
        }
    }
}
