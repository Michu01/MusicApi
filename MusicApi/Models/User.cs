using MusicApi.DTOs;

namespace MusicApi.Models
{
    public class User
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Email { get; }

        public DateTime JoinedAt { get; }

        public User(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Email = userDTO.Email;
            JoinedAt = userDTO.JoinedAt;
            Name = userDTO.UserName;
        }
    }
}
