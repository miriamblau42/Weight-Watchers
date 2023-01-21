using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriber.Data.Migrations
{
    public partial class addforignkeytoCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "Cards",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SubscriberId",
                table: "Cards",
                column: "SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Subscribers_SubscriberId",
                table: "Cards",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Subscribers_SubscriberId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_SubscriberId",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "SubscriberId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
