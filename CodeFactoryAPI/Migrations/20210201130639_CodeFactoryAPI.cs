using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFactoryAPI.Migrations
{
    public partial class CodeFactoryAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AskedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Question_ID);
                    table.ForeignKey(
                        name: "FK_Questions_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Reply_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.Reply_ID);
                    table.ForeignKey(
                        name: "FK_Reply_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reply_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersTags",
                columns: table => new
                {
                    Tag_ID = table.Column<int>(type: "int", nullable: false),
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag_ID1 = table.Column<int>(type: "int", nullable: false),
                    Question_ID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTags", x => new { x.Question_ID, x.Tag_ID });
                    table.ForeignKey(
                        name: "FK_UsersTags_Questions_Question_ID1",
                        column: x => x.Question_ID1,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTags_Tags_Tag_ID1",
                        column: x => x.Tag_ID1,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_User_ID",
                table: "Questions",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_Question_ID",
                table: "Reply",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_User_ID",
                table: "Reply",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTags_Question_ID1",
                table: "UsersTags",
                column: "Question_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTags_Tag_ID1",
                table: "UsersTags",
                column: "Tag_ID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "UsersTags");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
