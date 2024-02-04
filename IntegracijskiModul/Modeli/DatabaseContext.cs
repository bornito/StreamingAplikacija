using IntegracijskiModul.Modeli;
using Microsoft.EntityFrameworkCore;

namespace IntegracijskiModul.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<VideoTag> VideoTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name= ConnectionStrings:DefaultConnection");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Video

            modelBuilder.Entity<Video>(

                entity =>
                {
                    entity.ToTable("Video");

                    entity.Property(v => v.Created)
                        .HasDefaultValueSql("(getutcdate())");

                    entity.Property(v => v.Description)
                        .HasMaxLength(1024);

                    entity.Property(v => v.Name)
                         .HasMaxLength(128);

                    entity.Property(v => v.StreamingUrl)
                         .HasMaxLength(128);

                    entity.HasOne(g => g.Genre).WithMany(v => v.Videos)
                            .HasForeignKey(g => g.GenreId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Video_Genre");

                    entity.HasOne(i => i.Image).WithMany(v => v.Videos)
                            .HasForeignKey(i => i.ImageId)
                            .HasConstraintName("FK_Video_Images");
                });

            // Video Tag

            modelBuilder.Entity<VideoTag>(

                entity =>
                {
                    entity.ToTable("VideoTag");

                    entity.HasOne(t => t.Tag).WithMany(v => v.VideoTags)
                           .HasForeignKey(t => t.TagId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK_VideoTag_Tag");

                    entity.HasOne(v => v.Video).WithMany(t => t.VideoTags)
                           .HasForeignKey(v => v.VideoId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK_VideoTag_Video");
                });

            // Tag

            modelBuilder.Entity<Tag>(

                entity =>
                {
                    entity.ToTable("Tag");

                    entity.Property(t => t.Name)
                        .HasMaxLength(50);
                }); 

            // Zanr
            
            modelBuilder.Entity<Genre>(

                entity =>
                {
                    entity.ToTable("Genre");

                    entity.Property(g => g.Name)
                        .HasMaxLength(128);
                    entity.Property(g => g.Description)
                        .HasMaxLength(1024);
                });  

            // Slika
            
            modelBuilder.Entity<Image>(

                entity =>
                {
                    entity.ToTable("Image");
                });

            // drzava

            modelBuilder.Entity<Country>(

                entity =>
                {
                    entity.ToTable("Country");

                    entity.Property(c => c.Name)
                         .HasMaxLength(128);

                 entity.Property(c => c.CountryCode)
                       .HasMaxLength(2)
                         .IsUnicode(false)
                         .IsFixedLength();
                });

            // Korisnik

            modelBuilder.Entity<User>(

                entity =>
                {
                    entity.ToTable("User");

                    entity.Property(u => u.FirstName)
                         .HasMaxLength(128); 

                    entity.Property(u => u.LastName)
                         .HasMaxLength(128);   
                    
                    entity.Property(u => u.UserName)
                         .HasMaxLength(128);  
                    
                    entity.Property(u => u.Email)
                         .HasMaxLength(128);  
                    
                    entity.Property(u => u.TelephoneNumber)
                         .HasMaxLength(128); 
                    
                    entity.Property(u => u.PasswordHash)
                         .HasMaxLength(128); 
                    
                    entity.Property(u => u.PasswordSalt)
                         .HasMaxLength(128); 
                    
                    entity.Property(u => u.Token)
                         .HasMaxLength(128);

                    entity.HasOne(c => c.Country).WithMany(t => t.Users)
                        .HasForeignKey(c => c.Country)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_User_Country");
                });
            
            // notifikacija
            
            modelBuilder.Entity<Notification>(

                entity =>
                {
                    entity.ToTable("Notification");

                    entity.Property(n => n.Reciever)
                         .HasMaxLength(128); 

                    entity.Property(n => n.Subject)
                         .HasMaxLength(128);   
                    
                    entity.Property(n => n.EmailBody)
                         .HasMaxLength(1024);                    
                });

            OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}


