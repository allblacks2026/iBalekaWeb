using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBalekaWeb.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Athlete_AthleteID",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Event_EventID",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoute_Event_EventID",
                table: "EventRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoute_Route_RouteID",
                table: "EventRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_Athlete_AthleteID",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Run_AthleteID",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RunID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_EventRoute_EventID",
                table: "EventRoute");

            migrationBuilder.DropIndex(
                name: "IX_EventRoute_RouteID",
                table: "EventRoute");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_AthleteID",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_EventID",
                table: "EventRegistration");

            migrationBuilder.AddColumn<int>(
                name: "ClubID",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Run_AthleteID",
                table: "Run",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RunID",
                table: "Rating",
                column: "RunID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_EventID",
                table: "EventRoute",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_RouteID",
                table: "EventRoute",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_AthleteID",
                table: "EventRegistration",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventID",
                table: "EventRegistration",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ClubID",
                table: "Event",
                column: "ClubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Club_ClubID",
                table: "Event",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Athlete_AthleteID",
                table: "EventRegistration",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Event_EventID",
                table: "EventRegistration",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoute_Event_EventID",
                table: "EventRoute",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoute_Route_RouteID",
                table: "EventRoute",
                column: "RouteID",
                principalTable: "Route",
                principalColumn: "RouteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating",
                column: "RunID",
                principalTable: "Run",
                principalColumn: "RunID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Athlete_AthleteID",
                table: "Run",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Club_ClubID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Athlete_AthleteID",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Event_EventID",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoute_Event_EventID",
                table: "EventRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoute_Route_RouteID",
                table: "EventRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_Athlete_AthleteID",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Run_AthleteID",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RunID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_EventRoute_EventID",
                table: "EventRoute");

            migrationBuilder.DropIndex(
                name: "IX_EventRoute_RouteID",
                table: "EventRoute");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_AthleteID",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_EventID",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_Event_ClubID",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ClubID",
                table: "Event");

            migrationBuilder.CreateIndex(
                name: "IX_Run_AthleteID",
                table: "Run",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RunID",
                table: "Rating",
                column: "RunID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_EventID",
                table: "EventRoute",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_RouteID",
                table: "EventRoute",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_AthleteID",
                table: "EventRegistration",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventID",
                table: "EventRegistration",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Athlete_AthleteID",
                table: "EventRegistration",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Event_EventID",
                table: "EventRegistration",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoute_Event_EventID",
                table: "EventRoute",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoute_Route_RouteID",
                table: "EventRoute",
                column: "RouteID",
                principalTable: "Route",
                principalColumn: "RouteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating",
                column: "RunID",
                principalTable: "Run",
                principalColumn: "RunID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Athlete_AthleteID",
                table: "Run",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
