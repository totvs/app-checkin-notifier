using Microsoft.EntityFrameworkCore.Migrations;

namespace App.CheckIn.EntityFrameworkCore.Migrations
{
    public partial class AddingNotificationService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationService",
                table: "Subscriptions",
                nullable: false,
                defaultValue: "Firebase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationService",
                table: "Subscriptions");
        }
    }
}
