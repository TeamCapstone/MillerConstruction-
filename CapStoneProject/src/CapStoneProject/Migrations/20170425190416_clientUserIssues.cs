using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CapStoneProject.Migrations
{
    public partial class clientUserIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidRequests_AspNetUsers_UserId",
                table: "BidRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_ClientID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Clients_ClientID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clients_ClientID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdealTimeFrame",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "RequestedStartDate",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "BidRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserIndentityID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Projects",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BidRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidRequests_ClientId",
                table: "BidRequests",
                column: "ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BidRequests_AspNetUsers_ClientId",
                table: "BidRequests",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BidRequests_AspNetUsers_UserId",
                table: "BidRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_ClientId",
                table: "Invoices",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Projects",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Invoices",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ClientID",
                table: "Projects",
                newName: "IX_Projects_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_ClientID",
                table: "Invoices",
                newName: "IX_Invoices_ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidRequests_AspNetUsers_ClientId",
                table: "BidRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_BidRequests_AspNetUsers_UserId",
                table: "BidRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_ClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ClientId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_BidRequests_ClientId",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIndentityID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BidID = table.Column<int>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_Clients_Bids_BidID",
                        column: x => x.BidID,
                        principalTable: "Bids",
                        principalColumn: "BidID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "IdealTimeFrame",
                table: "BidRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedStartDate",
                table: "BidRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Projects",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BidRequests",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientID",
                table: "AspNetUsers",
                column: "ClientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectID",
                table: "AspNetUsers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BidID",
                table: "Clients",
                column: "BidID");

            migrationBuilder.AddForeignKey(
                name: "FK_BidRequests_AspNetUsers_UserId",
                table: "BidRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_ClientID",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Clients_ClientID",
                table: "Projects",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clients_ClientID",
                table: "AspNetUsers",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectID",
                table: "AspNetUsers",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Projects",
                newName: "ClientID");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Invoices",
                newName: "ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                newName: "IX_Projects_ClientID");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                newName: "IX_Invoices_ClientID");
        }
    }
}
