using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iBalekaWeb.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Athlete",
                columns: table => new
                {
                    AthleteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    LicenseNo = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athlete", x => x.AthleteID);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    MapImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    ClubID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.ClubID);
                    table.ForeignKey(
                        name: "FK_Club_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoint",
                columns: table => new
                {
                    CheckpointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deleted = table.Column<bool>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    RouteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoint", x => x.CheckpointID);
                    table.ForeignKey(
                        name: "FK_Checkpoint_Route_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    RoleId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: true),
                    RoleId1 = table.Column<string>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMember",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AthleteID = table.Column<int>(nullable: false),
                    ClubID = table.Column<int>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    DateLeft = table.Column<DateTime>(nullable: true),
                    IsaMember = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMember", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_ClubMember_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMember_Club_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Club",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClubID = table.Column<int>(nullable: false),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_Club_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Club",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistration",
                columns: table => new
                {
                    RegistrationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Arrived = table.Column<bool>(nullable: false),
                    AthleteID = table.Column<int>(nullable: false),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EventID = table.Column<int>(nullable: false),
                    SelectedRoute = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistration", x => x.RegistrationID);
                    table.ForeignKey(
                        name: "FK_EventRegistration_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistration_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRoute",
                columns: table => new
                {
                    EventRouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EventID = table.Column<int>(nullable: false),
                    RouteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRoute", x => x.EventRouteID);
                    table.ForeignKey(
                        name: "FK_EventRoute_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRoute_Route_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Run",
                columns: table => new
                {
                    RunID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AthleteID = table.Column<int>(nullable: false),
                    CaloriesBurnt = table.Column<double>(nullable: false),
                    DateRecorded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<int>(nullable: true),
                    RouteId = table.Column<int>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Run", x => x.RunID);
                    table.ForeignKey(
                        name: "FK_Run_Athlete_AthleteID",
                        column: x => x.AthleteID,
                        principalTable: "Athlete",
                        principalColumn: "AthleteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Run_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Run_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    RouteId = table.Column<int>(nullable: true),
                    RunID = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Rating_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rating_Run_RunID",
                        column: x => x.RunID,
                        principalTable: "Run",
                        principalColumn: "RunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoint_RouteID",
                table: "Checkpoint",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Club_UserID",
                table: "Club",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_AthleteID",
                table: "ClubMember",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_ClubID",
                table: "ClubMember",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ClubID",
                table: "Event",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_AthleteID",
                table: "EventRegistration",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventID",
                table: "EventRegistration",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_EventID",
                table: "EventRoute",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventRoute_RouteID",
                table: "EventRoute",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RouteID",
                table: "Rating",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RunID",
                table: "Rating",
                column: "RunID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Run_AthleteID",
                table: "Run",
                column: "AthleteID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_EventID",
                table: "Run",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Run_RouteID",
                table: "Run",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId1",
                table: "AspNetUserLogins",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkpoint");

            migrationBuilder.DropTable(
                name: "ClubMember");

            migrationBuilder.DropTable(
                name: "EventRegistration");

            migrationBuilder.DropTable(
                name: "EventRoute");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Run");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Athlete");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
