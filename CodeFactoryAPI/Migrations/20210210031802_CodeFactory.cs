using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeFactoryAPI.Migrations
{
    public partial class CodeFactory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Password = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag1_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag2_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag3_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tag4_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tag5_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Question_ID);
                    table.ForeignKey(
                        name: "FK_Questions_Tags_Tag1_ID",
                        column: x => x.Tag1_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Tags_Tag2_ID",
                        column: x => x.Tag2_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Tags_Tag3_ID",
                        column: x => x.Tag3_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Tags_Tag4_ID",
                        column: x => x.Tag4_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Tags_Tag5_ID",
                        column: x => x.Tag5_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Message_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Messages = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Message_ID);
                    table.ForeignKey(
                        name: "FK_Messages_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Reply_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Reply_ID);
                    table.ForeignKey(
                        name: "FK_Replies_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Replies_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Question_ID",
                table: "Messages",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User_ID",
                table: "Messages",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Tag1_ID",
                table: "Questions",
                column: "Tag1_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Tag2_ID",
                table: "Questions",
                column: "Tag2_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Tag3_ID",
                table: "Questions",
                column: "Tag3_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Tag4_ID",
                table: "Questions",
                column: "Tag4_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Tag5_ID",
                table: "Questions",
                column: "Tag5_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_User_ID",
                table: "Questions",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_Question_ID",
                table: "Replies",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_User_ID",
                table: "Replies",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
