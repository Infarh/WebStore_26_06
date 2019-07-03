using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.DTO.Identity
{
    public class AddLoginDTO : UserInfoDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
