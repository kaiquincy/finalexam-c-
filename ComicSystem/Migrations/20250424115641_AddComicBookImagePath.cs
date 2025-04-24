﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddComicBookImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ComicBooks",
                type: "TEXT",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ComicBooks");
        }
    }
}
