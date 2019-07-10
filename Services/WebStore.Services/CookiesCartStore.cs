using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Services
{
    public class CookiesCartStore : ICartStore
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;

        public Cart Cart
        {
            get
            {
                var http_context = _HttpContextAccessor.HttpContext;
                var cookie = http_context.Request.Cookies[_CartName];

                Cart cart = null;
                if (cookie is null)
                {
                    cart = new Cart();
                    http_context.Response.Cookies.Append(
                        _CartName,
                        JsonConvert.SerializeObject(cart));
                }
                else
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);
                    http_context.Response.Cookies.Delete(_CartName);
                    http_context.Response.Cookies.Append(_CartName, cookie, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                }

                return cart;
            }
            set
            {
                var http_context = _HttpContextAccessor.HttpContext;

                var json = JsonConvert.SerializeObject(value);
                http_context.Response.Cookies.Delete(_CartName);
                http_context.Response.Cookies.Append(_CartName, json, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                });
            }
        }

        public CookiesCartStore(IHttpContextAccessor HttpContextAccessor)
        {
            _HttpContextAccessor = HttpContextAccessor;

            var user = HttpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;
            _CartName = $"cart{user_name}";
        }
    }
}
