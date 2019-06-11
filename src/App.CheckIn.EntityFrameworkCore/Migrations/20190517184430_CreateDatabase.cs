using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.CheckIn.EntityFrameworkCore.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    EventCode = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    EventDescription = table.Column<string>(nullable: true),
                    EventRoom = table.Column<string>(nullable: true),
                    EventStartTime = table.Column<DateTimeOffset>(nullable: false),
                    EventDuration = table.Column<TimeSpan>(nullable: false),
                    EnablePushNotification = table.Column<bool>(nullable: false),
                    Notified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
