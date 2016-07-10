using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using iBalekaWeb.Models;
using JetBrains.Annotations;

namespace iBalekaWeb.Data.Configurations
{
    public partial class iBalekaDBContext : DbContext
    {
        public iBalekaDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserRoles_UserId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Athlete>(entity =>
            {
                entity.Property(e => e.AthleteId).HasColumnName("AthleteID");
            });

            modelBuilder.Entity<Checkpoint>(entity =>
            {
                entity.HasIndex(e => e.RouteId)
                    .HasName("IX_Checkpoint_RouteID");

                entity.Property(e => e.CheckpointId).HasColumnName("CheckpointID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Checkpoint)
                    .HasForeignKey(d => d.RouteId);
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Club_UserID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Club)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ClubMember>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK_ClubMember");

                entity.HasIndex(e => e.AthleteId)
                    .HasName("IX_ClubMember_AthleteID");

                entity.HasIndex(e => e.ClubId)
                    .HasName("IX_ClubMember_ClubID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.AthleteId).HasColumnName("AthleteID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.HasOne(d => d.Athlete)
                    .WithMany(p => p.ClubMember)
                    .HasForeignKey(d => d.AthleteId);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.ClubMember)
                    .HasForeignKey(d => d.ClubId);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.ClubId)
                    .HasName("IX_Event_ClubID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.ClubId);
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.HasKey(e => e.RegistrationId)
                    .HasName("PK_EventRegistration");

                entity.HasIndex(e => e.AthleteId)
                    .HasName("IX_EventRegistration_AthleteID");

                entity.HasIndex(e => e.EventId)
                    .HasName("IX_EventRegistration_EventID");

                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.AthleteId).HasColumnName("AthleteID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.HasOne(d => d.Athlete)
                    .WithMany(p => p.EventRegistration)
                    .HasForeignKey(d => d.AthleteId);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRegistration)
                    .HasForeignKey(d => d.EventId);
            });

            modelBuilder.Entity<EventRoute>(entity =>
            {
                entity.HasIndex(e => e.EventId)
                    .HasName("IX_EventRoute_EventID");

                entity.HasIndex(e => e.RouteId)
                    .HasName("IX_EventRoute_RouteID");

                entity.Property(e => e.EventRouteId).HasColumnName("EventRouteID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRoute)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.EventRoute)
                    .HasForeignKey(d => d.RouteId);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.RouteId)
                    .HasName("IX_Rating_RouteID");

                entity.HasIndex(e => e.RunId)
                    .HasName("IX_Rating_RunID")
                    .IsUnique();

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.Property(e => e.RunId).HasColumnName("RunID");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.RouteId);

                entity.HasOne(d => d.Run)
                    .WithOne(p => p.Rating)
                    .HasForeignKey<Rating>(d => d.RunId);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.RouteId).HasColumnName("RouteID");
            });

            modelBuilder.Entity<Run>(entity =>
            {
                entity.HasIndex(e => e.AthleteId)
                    .HasName("IX_Run_AthleteID");

                entity.HasIndex(e => e.EventId)
                    .HasName("IX_Run_EventID");

                entity.HasIndex(e => e.RouteId)
                    .HasName("IX_Run_RouteID");

                entity.Property(e => e.RunId).HasColumnName("RunID");

                entity.Property(e => e.AthleteId).HasColumnName("AthleteID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.HasOne(d => d.Athlete)
                    .WithMany(p => p.Run)
                    .HasForeignKey(d => d.AthleteId);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Run)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Run)
                    .HasForeignKey(d => d.RouteId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Surname).IsRequired();
            });
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Athlete> Athlete { get; set; }
        public virtual DbSet<Checkpoint> Checkpoint { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<ClubMember> ClubMember { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventRegistration> EventRegistration { get; set; }
        public virtual DbSet<EventRoute> EventRoute { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Run> Run { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}