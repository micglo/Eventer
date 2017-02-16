namespace Eventer.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiActivityLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        User = c.String(),
                        UserHostAddress = c.String(),
                        RequestContentType = c.String(),
                        RequestContentBody = c.String(),
                        RequestUri = c.String(),
                        RequestMethod = c.String(),
                        RequestRouteTemplate = c.String(),
                        RequestRouteData = c.String(),
                        RequestHeaders = c.String(),
                        RequestTimestamp = c.DateTime(),
                        ResponseContentType = c.String(),
                        ResponseContentBody = c.String(),
                        ResponseStatusCode = c.Int(),
                        ResponseHeaders = c.String(),
                        ResponseTimestamp = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        EventName = c.String(),
                        EventDate = c.DateTime(),
                        EventLocalization = c.String(),
                        EventImage = c.String(),
                        EventUrl = c.String(),
                        EventDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientSecret = c.String(),
                        Username = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ErrorDateTime = c.DateTime(),
                        ErrorLevel = c.String(),
                        UserName = c.String(),
                        ErrorMessage = c.String(),
                        InnerErrorMessage = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshToken",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 100),
                        ClientId = c.String(nullable: false, maxLength: 100),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CategoryEvent",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        EventId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.EventId })
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CategoryEvent", "EventId", "dbo.Event");
            DropForeignKey("dbo.CategoryEvent", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.City", "StateId", "dbo.State");
            DropForeignKey("dbo.Event", "CityId", "dbo.City");
            DropIndex("dbo.CategoryEvent", new[] { "EventId" });
            DropIndex("dbo.CategoryEvent", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.City", new[] { "StateId" });
            DropIndex("dbo.Event", new[] { "CityId" });
            DropTable("dbo.CategoryEvent");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshToken");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.Client");
            DropTable("dbo.State");
            DropTable("dbo.City");
            DropTable("dbo.Event");
            DropTable("dbo.Category");
            DropTable("dbo.ApiActivityLog");
        }
    }
}
