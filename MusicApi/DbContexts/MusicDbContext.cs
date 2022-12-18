using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

using MusicApi.DTOs;
using MusicApi.Extensions;

namespace MusicApi.DbContexts
{
    public class MusicDbContext : IdentityDbContext<UserDTO, IdentityRole<Guid>, Guid>
    {
        [NotNull]
        public virtual DbSet<SongDTO>? Songs { get; set; }

        [NotNull]
        public virtual DbSet<AlbumDTO>? Albums { get; set; }

        [NotNull]
        public virtual DbSet<ArtistDTO>? Artists { get; set; }

        [NotNull]
        public virtual DbSet<GenreDTO>? Genres { get; set; }

        [NotNull]
        public virtual DbSet<PlaylistDTO>? Playlists { get; set; }

        [NotNull]
        public virtual DbSet<ArtistSongDTO>? ArtistSongs { get; set; }

        [NotNull]
        public virtual DbSet<PlaylistSongDTO>? PlaylistSongs { get; set; }

        [NotNull]
        public virtual DbSet<UserFavouriteEntryDTO>? UserFavouriteEntries { get; set; }

        [NotNull]
        public virtual DbSet<UserPlayedEntryDTO>? UserPlayedEntries { get; set; }

        [NotNull]
        public virtual DbSet<UserFollowDTO>? UserFollows { get; set; }

        [NotNull]
        public virtual DbSet<ArtistAlbumDTO>? ArtistAlbums { get; set; }

        [NotNull]
        public virtual DbSet<EntryGenreDTO>? EntryGenres { get; set; }

        public MusicDbContext(DbContextOptions<MusicDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SongDTO>()
                .HasMany(s => s.Artists)
                .WithMany(a => a.Songs)
                .UsingEntity<ArtistSongDTO>();

            builder.Entity<SongDTO>()
                .HasMany(s => s.Playlists)
                .WithMany(p => p.Songs)
                .UsingEntity<PlaylistSongDTO>();

            builder.Entity<AlbumDTO>()
                .HasMany(a => a.Artists)
                .WithMany(a => a.Albums)
                .UsingEntity<ArtistAlbumDTO>();

            builder.Entity<UserDTO>()
                .HasMany(u => u.UserFollows)
                .WithOne(u => u.Follower)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void AddTimeStamps()
        {
            foreach (EntityEntry<UserDTO> entry in ChangeTracker
                .Entries<UserDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.JoinedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<PlaylistDTO> entry in ChangeTracker
                .Entries<PlaylistDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<UserPlayedEntryDTO> entry in ChangeTracker
                .Entries<UserPlayedEntryDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.PlayedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<AlbumDTO> entry in ChangeTracker
                .Entries<AlbumDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<SongDTO> entry in ChangeTracker
                .Entries<SongDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<ArtistDTO> entry in ChangeTracker
                .Entries<ArtistDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<GenreDTO> entry in ChangeTracker
                .Entries<GenreDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<PlaylistSongDTO> entry in ChangeTracker
                .Entries<PlaylistSongDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }

            foreach (EntityEntry<UserFavouriteEntryDTO> entry in ChangeTracker
                .Entries<UserFavouriteEntryDTO>()
                .Where(e => e.State == EntityState.Added))
            {
                entry.Property(e => e.AddedAt).CurrentValue = DateTime.UtcNow;
            }
        }

        public override int SaveChanges()
        {
            AddTimeStamps();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimeStamps();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddTimeStamps();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimeStamps();

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserDTO> FindUser(IHeaderDictionary headers)
        {
            string apiKey = headers.GetApiKey();

            return await Users.SingleAsync(u => u.ApiKey == apiKey);
        }
    }
}
