using Microsoft.EntityFrameworkCore.Migrations;

namespace App.CheckIn.EntityFrameworkCore.Migrations
{
    public partial class AddingNotificationToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationToken",
                table: "Subscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationToken",
                table: "Subscriptions");
        }
    }
}
