namespace SleepWatcher.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 4000),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(nullable: false, maxLength: 4000),
                        StepId = c.Int(nullable: false),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.StepId)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StepName = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        AlarmTime = c.DateTime(nullable: false),
                        ModifiedOn = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        IsCompleted = c.Boolean(nullable: false),
                        IsCancled = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 4000),
                        LastName = c.String(nullable: false, maxLength: 4000),
                        CurrentStepId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CurrentSteps",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StepName = c.Int(nullable: false),
                        AlarmTime = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        IsCancled = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Steps", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Notes", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.CurrentSteps", "Id", "dbo.Patients");
            DropForeignKey("dbo.Notes", "StepId", "dbo.Steps");
            DropIndex("dbo.CurrentSteps", new[] { "Id" });
            DropIndex("dbo.Steps", new[] { "PatientId" });
            DropIndex("dbo.Notes", new[] { "Patient_Id" });
            DropIndex("dbo.Notes", new[] { "StepId" });
            DropTable("dbo.CurrentSteps");
            DropTable("dbo.Patients");
            DropTable("dbo.Steps");
            DropTable("dbo.Notes");
        }
    }
}
