using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.DTO.Identity
{
    public abstract class UserInfoDTO
    {
        public User User { get; set; }
    }
}
