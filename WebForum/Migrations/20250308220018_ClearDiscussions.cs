using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForum.Migrations
{
    public partial class ClearDiscussions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Run SQL to delete all discussions
            migrationBuilder.Sql("DELETE FROM Discussions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally, you can leave this empty if you don't want to undo the delete action
            // Alternatively, you could insert some data back if you need to reverse the deletion
        }
    }
}

