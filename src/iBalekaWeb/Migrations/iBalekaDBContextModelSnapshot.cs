using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iBalekaWeb.Data.Configurations;

namespace iBalekaWeb.Migrations
{
    [DbContext(typeof(iBalekaDBContext))]
    partial class iBalekaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iBalekaWeb.Models.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasName("IX_AspNetRoleClaims_RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderKey")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("PK_AspNetUserLogins");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("RoleId")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("UserId", "RoleId")
                        .HasName("PK_AspNetUserRoles");

                    b.HasIndex("RoleId")
                        .HasName("IX_AspNetUserRoles_RoleId");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserRoles_UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("PK_AspNetUserTokens");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Athlete", b =>
                {
                    b.Property<int>("AthleteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AthleteID");

                    b.Property<DateTime>("DateJoined");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Firstname");

                    b.Property<int?>("Gender");

                    b.Property<double?>("Height");

                    b.Property<string>("LicenseNo");

                    b.Property<string>("Surname");

                    b.Property<double?>("Weight");

                    b.HasKey("AthleteId");

                    b.ToTable("Athlete");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Checkpoint", b =>
                {
                    b.Property<int>("CheckpointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CheckpointID");

                    b.Property<bool>("Deleted");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("RouteId")
                        .HasColumnName("RouteID");

                    b.HasKey("CheckpointId");

                    b.HasIndex("RouteId")
                        .HasName("IX_Checkpoint_RouteID");

                    b.ToTable("Checkpoint");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Club", b =>
                {
                    b.Property<int>("ClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClubID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int>("UserId")
                        .HasColumnName("UserID");

                    b.HasKey("ClubId");

                    b.HasIndex("UserId")
                        .HasName("IX_Club_UserID");

                    b.ToTable("Club");
                });

            modelBuilder.Entity("iBalekaWeb.Models.ClubMember", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MemberID");

                    b.Property<int>("AthleteId")
                        .HasColumnName("AthleteID");

                    b.Property<int>("ClubId")
                        .HasColumnName("ClubID");

                    b.Property<DateTime>("DateJoined");

                    b.Property<DateTime?>("DateLeft");

                    b.Property<bool>("IsaMember");

                    b.HasKey("MemberId")
                        .HasName("PK_ClubMember");

                    b.HasIndex("AthleteId")
                        .HasName("IX_ClubMember_AthleteID");

                    b.HasIndex("ClubId")
                        .HasName("IX_ClubMember_ClubID");

                    b.ToTable("ClubMember");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventID");

                    b.Property<int>("ClubId")
                        .HasColumnName("ClubID");

                    b.Property<DateTime>("DateAndTime");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("EventId");

                    b.HasIndex("ClubId")
                        .HasName("IX_Event_ClubID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("iBalekaWeb.Models.EventRegistration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RegistrationID");

                    b.Property<bool>("Arrived");

                    b.Property<int>("AthleteId")
                        .HasColumnName("AthleteID");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<bool>("Deleted");

                    b.Property<int>("EventId")
                        .HasColumnName("EventID");

                    b.Property<int>("SelectedRoute");

                    b.HasKey("RegistrationId")
                        .HasName("PK_EventRegistration");

                    b.HasIndex("AthleteId")
                        .HasName("IX_EventRegistration_AthleteID");

                    b.HasIndex("EventId")
                        .HasName("IX_EventRegistration_EventID");

                    b.ToTable("EventRegistration");
                });

            modelBuilder.Entity("iBalekaWeb.Models.EventRoute", b =>
                {
                    b.Property<int>("EventRouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventRouteID");

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("EventId")
                        .HasColumnName("EventID");

                    b.Property<int>("RouteId")
                        .HasColumnName("RouteID");

                    b.HasKey("EventRouteId");

                    b.HasIndex("EventId")
                        .HasName("IX_EventRoute_EventID");

                    b.HasIndex("RouteId")
                        .HasName("IX_EventRoute_RouteID");

                    b.ToTable("EventRoute");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RatingID");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("RouteId")
                        .HasColumnName("RouteID");

                    b.Property<int>("RunId")
                        .HasColumnName("RunID");

                    b.Property<int>("Value");

                    b.HasKey("RatingId");

                    b.HasIndex("RouteId")
                        .HasName("IX_Rating_RouteID");

                    b.HasIndex("RunId")
                        .IsUnique()
                        .HasName("IX_Rating_RunID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RouteID");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<double>("Distance");

                    b.Property<string>("MapImage");

                    b.HasKey("RouteId");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Run", b =>
                {
                    b.Property<int>("RunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RunID");

                    b.Property<int>("AthleteId")
                        .HasColumnName("AthleteID");

                    b.Property<double>("CaloriesBurnt");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("EventId")
                        .HasColumnName("EventID");

                    b.Property<int?>("RouteId")
                        .HasColumnName("RouteID");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("RunId");

                    b.HasIndex("AthleteId")
                        .HasName("IX_Run_AthleteID");

                    b.HasIndex("EventId")
                        .HasName("IX_Run_EventID");

                    b.HasIndex("RouteId")
                        .HasName("IX_Run_RouteID");

                    b.ToTable("Run");
                });

            modelBuilder.Entity("iBalekaWeb.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserID");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("DateJoined");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetRoleClaims", b =>
                {
                    b.HasOne("iBalekaWeb.Models.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserClaims", b =>
                {
                    b.HasOne("iBalekaWeb.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserLogins", b =>
                {
                    b.HasOne("iBalekaWeb.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.AspNetUserRoles", b =>
                {
                    b.HasOne("iBalekaWeb.Models.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.Checkpoint", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Route", "Route")
                        .WithMany("Checkpoint")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.Club", b =>
                {
                    b.HasOne("iBalekaWeb.Models.User", "User")
                        .WithMany("Club")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.ClubMember", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Athlete", "Athlete")
                        .WithMany("ClubMember")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.Club", "Club")
                        .WithMany("ClubMember")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.Event", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Club", "Club")
                        .WithMany("Event")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.EventRegistration", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Athlete", "Athlete")
                        .WithMany("EventRegistration")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.Event", "Event")
                        .WithMany("EventRegistration")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.EventRoute", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Event", "Event")
                        .WithMany("EventRoute")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.Route", "Route")
                        .WithMany("EventRoute")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.Rating", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Route", "Route")
                        .WithMany("Rating")
                        .HasForeignKey("RouteId");

                    b.HasOne("iBalekaWeb.Models.Run", "Run")
                        .WithOne("Rating")
                        .HasForeignKey("iBalekaWeb.Models.Rating", "RunId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaWeb.Models.Run", b =>
                {
                    b.HasOne("iBalekaWeb.Models.Athlete", "Athlete")
                        .WithMany("Run")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.Event", "Event")
                        .WithMany("Run")
                        .HasForeignKey("EventId");

                    b.HasOne("iBalekaWeb.Models.Route", "Route")
                        .WithMany("Run")
                        .HasForeignKey("RouteId");
                });
        }
    }
}
