using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTierArchitecture.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_added_updatedUserId_and_deletedUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "AppRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "AppRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "AppRoles",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "AppRoles");
        }
    }
}
