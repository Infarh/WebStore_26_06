using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    [ViewComponent(Name = "UserInfo")] // <-- Имя компоненту можно дать тут
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
                return View("UserInfoView");

            return View();
        }
    }
}
