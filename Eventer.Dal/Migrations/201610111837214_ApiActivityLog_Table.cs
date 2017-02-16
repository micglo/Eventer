namespace Eventer.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiActivityLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiActivityLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Application = c.String(),
                        UserId = c.String(nullable: false),
                        ClientId = c.String(nullable: false),
                        Machine = c.String(),
                        RequestIpAddress = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApiActivityLog");
        }
    }
}
