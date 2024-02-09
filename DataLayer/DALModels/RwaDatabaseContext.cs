using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DALModels;

public partial class RwaDatabaseContext : DbContext
{
    public RwaDatabaseContext()
    {
    }

    public RwaDatabaseContext(DbContextOptions<RwaDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoGenre> VideoGenres { get; set; }

    public virtual DbSet<VideoTag> VideoTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:RWAConnStr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("PK__Country__D9D5A694E5ACCA57");

            entity.ToTable("Country");

            entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Idgenre).HasName("PK__Genre__23FDA2F09EECEE69");

            entity.ToTable("Genre");

            entity.Property(e => e.Idgenre).HasColumnName("IDGenre");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Idnotification).HasName("PK__Notifica__5456E7BC630F59E7");

            entity.ToTable("Notification");

            entity.Property(e => e.Idnotification).HasColumnName("IDNotification");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Receiver).HasMaxLength(50);
            entity.Property(e => e.Sender).HasMaxLength(50);
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(500);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Idtag).HasName("PK__Tag__A702375143384089");

            entity.ToTable("Tag");

            entity.Property(e => e.Idtag).HasColumnName("IDTag");
            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__User__EAE6D9DF01EDB232");

            entity.ToTable("User");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.DeactivatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Country");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserType");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.IduserNotification).HasName("PK__User_Not__8D3FDB9FE61F4E48");

            entity.ToTable("User_Notification");

            entity.Property(e => e.IduserNotification).HasColumnName("IDUser_Notification");
            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Notification).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NF_Notification");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NF_User");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.IduserType).HasName("PK__UserType__EA4074F2F8680571");

            entity.ToTable("UserType");

            entity.Property(e => e.IduserType).HasColumnName("IDUserType");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Idvideo).HasName("PK__Video__6521D6B38A9D5302");

            entity.ToTable("Video");

            entity.Property(e => e.Idvideo).HasColumnName("IDVideo");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.StreamingUrl).HasMaxLength(325);
        });

        modelBuilder.Entity<VideoGenre>(entity =>
        {
            entity.HasKey(e => e.IdvideoGenre).HasName("PK__Video_Ge__BF40D96C7D35BFFA");

            entity.ToTable("Video_Genre");

            entity.Property(e => e.IdvideoGenre).HasColumnName("IDVideo_Genre");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.VideoId).HasColumnName("VideoID");

            entity.HasOne(d => d.Genre).WithMany(p => p.VideoGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VG_Genre");

            entity.HasOne(d => d.Video).WithMany(p => p.VideoGenres)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VG_Video");
        });

        modelBuilder.Entity<VideoTag>(entity =>
        {
            entity.HasKey(e => e.IdvideoTag).HasName("PK__Video_Ta__7B77E88C04ED286E");

            entity.ToTable("Video_Tag");

            entity.Property(e => e.IdvideoTag).HasColumnName("IDVideo_Tag");
            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.VideoId).HasColumnName("VideoID");

            entity.HasOne(d => d.Tag).WithMany(p => p.VideoTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VT_Tag");

            entity.HasOne(d => d.Video).WithMany(p => p.VideoTags)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VT_Video");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
