using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iBalekaWeb.Data.Configurations;

namespace iBalekaWeb.Migrations
{
    [DbContext(typeof(iBalekaDBContext))]
    [Migration("20160726032413_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iBalekaWeb.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
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
                    b.Property<int>("ClubID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClubId")
                        .HasColumnName("ClubID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("UserId")
                        .HasColumnName("UserID")
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("ClubID");

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
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClubID");

                    b.Property<string>("Date");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Time");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UserID");

                    b.HasKey("EventId");

                    b.HasIndex("ClubID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("iBalekaWeb.Models.EventRegistration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RegistrationID");

                    b.Property<bool>("Arrived");

                    b.Property<int>("AthleteID");

                    b.Property<int>("AthleteId")
                        .HasColumnName("AthleteID");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<bool>("Deleted");

                    b.Property<int>("EventID");

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

                    b.Property<int>("EventID");

                    b.Property<int>("EventId")
                        .HasColumnName("EventID");

                    b.Property<int>("RouteID");

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

                    b.Property<int?>("RouteId");

                    b.Property<int>("RunID");

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

                    b.Property<string>("Title");

                    b.Property<string>("UserID");

                    b.HasKey("RouteId");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("iBalekaWeb.Models.Run", b =>
                {
                    b.Property<int>("RunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RunID");

                    b.Property<int>("AthleteID");

                    b.Property<int>("AthleteId")
                        .HasColumnName("AthleteID");

                    b.Property<double>("CaloriesBurnt");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("EventId");

                    b.Property<int?>("RouteId");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
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
                    b.HasOne("iBalekaWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
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
                    b.HasOne("iBalekaWeb.Models.Club")
                        .WithMany("Event")
                        .HasForeignKey("ClubID")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("iBalekaWeb.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("iBalekaWeb.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaWeb.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
