using System;

namespace WebStore.Domain.DTO.Identity
{
    public class SetLockoutDTO : UserInfoDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
