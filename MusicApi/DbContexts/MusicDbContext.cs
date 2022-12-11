using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MusicApi.DTOs;

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
    }
}
