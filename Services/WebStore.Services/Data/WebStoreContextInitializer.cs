﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreContextInitializer
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public WebStoreContextInitializer(
            WebStoreContext db, 
            UserManager<User> UserManager, 
            RoleManager<IdentityRole> RoleManager)
        {
            _db = db;

            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public async Task InitializeAsync()
        {
            await _db.Database.MigrateAsync();

            await InitializeIdentityAsync();

            if (await _db.Products.AnyAsync())
                return;

            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Sections.AddRangeAsync(TestData.Sections);

                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Brands.AddRangeAsync(TestData.Brands);

                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            using (var transaction = _db.Database.BeginTransaction())
            {
                await _db.Products.AddRangeAsync(TestData.Products);

                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }

        }

        private async Task InitializeIdentityAsync()
        {
            if (!await _RoleManager.RoleExistsAsync(User.RoleUser))
            {
                var creation_result = await _RoleManager.CreateAsync(new IdentityRole(User.RoleUser));
                if (!creation_result.Succeeded)
                    throw new InvalidOperationException($"Ошибка создания роли пользователя {string.Join(", ", creation_result.Errors.Select(error => error.Description))}");
            }

            if (!await _RoleManager.RoleExistsAsync(User.RoleAdmin))
            {
                var creation_result = await _RoleManager.CreateAsync(new IdentityRole(User.RoleAdmin));
                if (!creation_result.Succeeded)
                    throw new InvalidOperationException($"Ошибка создания роли администратора {string.Join(", ", creation_result.Errors.Select(error => error.Description))}");
            }

            if (await _UserManager.FindByNameAsync(User.AdminUserName) == null)
            {
                var admin = new User
                {
                    UserName = User.AdminUserName,
                    Email = $"{User.AdminUserName}@server.ru"
                };

                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);

                if (creation_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, User.RoleAdmin);
                else
                    throw new InvalidOperationException($"Ошибка создания пользователя администратора: {string.Join(", ", creation_result.Errors.Select(error => error.Description))}");
            }
        }
    }
}
