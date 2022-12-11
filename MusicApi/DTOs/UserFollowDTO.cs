using Microsoft.EntityFrameworkCore;

namespace MusicApi.DTOs
{
    [PrimaryKey(nameof(FollowerId), nameof(FollowedId))]
    public class UserFollowDTO
    {
        public Guid FollowerId { get; set; }

        public virtual UserDTO? Follower { get; set; }

        public Guid FollowedId { get; set; }

        public virtual UserDTO? Followed { get; set; }
    }
}
