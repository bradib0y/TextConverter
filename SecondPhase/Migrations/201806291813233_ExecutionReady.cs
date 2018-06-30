namespace SecondPhase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutionReady : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FertilityModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 200),
                        Fertility1960 = c.Double(),
                        Fertility2016 = c.Double(),
                        Iq = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FertilityModels");
        }
    }
}
