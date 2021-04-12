using Microsoft.EntityFrameworkCore;
using OAuth.Data.Contract.Models;

#nullable disable

namespace OAuth.Data.Contract.Data
{
    public partial class userContext : DbContext
    {
        public userContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public userContext(DbContextOptions<userContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TokenManager> TokenManagers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCredential> UserCredentials { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=YAANSON;Initial Catalog=user_master;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TokenManager>(entity =>
            {
                entity.ToTable("TokenManager");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessTokenSecret)
                    .IsRequired()
                    .HasColumnName("access_token_secret");

                entity.Property(e => e.AccessTokenUpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("access_token_updateAt");

                entity.Property(e => e.RefreshTokenSecret)
                    .IsRequired()
                    .HasColumnName("refresh_token_secret");

                entity.Property(e => e.RefreshTokenUpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("refresh_token_updateAt");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Annonymous')");

                entity.Property(e => e.Intro).HasMaxLength(200);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.ToTable("UserCredential");

                entity.HasIndex(e => e.UserName, "UQ__UserCred__C9F284564B544DF7")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EncriptionKey)
                    .IsRequired()
                    .HasColumnName("Encription_Key");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.RegisterAt).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCredentials)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_UserCredential_userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
