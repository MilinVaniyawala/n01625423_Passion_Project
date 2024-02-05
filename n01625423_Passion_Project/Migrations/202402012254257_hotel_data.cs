namespace n01625423_Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotel_data : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        HotelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        Amenities = c.String(),
                    })
                .PrimaryKey(t => t.HotelID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        RoomNumber = c.String(),
                        Type = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RoomID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserID", "dbo.Users");
            DropForeignKey("dbo.Reservations", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "HotelID", "dbo.Hotels");
            DropIndex("dbo.Rooms", new[] { "HotelID" });
            DropIndex("dbo.Reservations", new[] { "RoomID" });
            DropIndex("dbo.Reservations", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Hotels");
        }
    }
}
