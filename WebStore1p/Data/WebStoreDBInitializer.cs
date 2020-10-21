using System;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebStore.DAL.Context;

using Microsoft.AspNetCore.Identity;

using WebStore1p.Domain.Identity;

namespace WebStore1p.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        //5 в конструкторе запрашиваем контекст БД
        public WebStoreDBInitializer(WebStoreDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager) 
        { 
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }
        public void Initialize() => InitializeAsync().Wait();
        
        public async Task InitializeAsync()
        {
            var db = _db.Database;

            ////5 ПШ проверка что база удалена
            //if(await db.EnsureDeletedAsync().ConfigureAwait(false))
            //{
            //    //5 ПШ создание базы заново
            //    if (!await db.EnsureCreatedAsync().ConfigureAwait(false))
            //        throw new InvalidOperationException("Не удалось создать БД");
            //}

            await db.MigrateAsync().ConfigureAwait(false);

            //await InitializeIdentityAsync().ConfigureAwait(false);

            await InitializeProductsAsync().ConfigureAwait(false);
        }

        private async Task InitializeProductsAsync()
        {
            if (await _db.Products.AnyAsync()) return;

            var db = _db.Database;

            //5 ПШ добавление данных в БД
            using (var transaction = await db.BeginTransactionAsync().ConfigureAwait(false))
            {
                //5 ПШ добавление данных в БД из класса
                await _db.Sections.AddRangeAsync(TestData.Sections).ConfigureAwait(false);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                //5 ПШ перенос всех изменений в БД
                await _db.SaveChangesAsync().ConfigureAwait(false);
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var transaction = await db.BeginTransactionAsync().ConfigureAwait(false))
            {
                //5 ПШ добавление данных в БД из класса
                await _db.Brands.AddRangeAsync(TestData.Brands).ConfigureAwait(false);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                //5 ПШ перенос всех изменений в БД
                await _db.SaveChangesAsync().ConfigureAwait(false);
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

            using (var transaction = await db.BeginTransactionAsync().ConfigureAwait(false))
            {
                //5 ПШ добавление данных в БД из класса
                await _db.Products.AddRangeAsync(TestData.Products).ConfigureAwait(false);

                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                //5 ПШ перенос всех изменений в БД
                await _db.SaveChangesAsync().ConfigureAwait(false);
                await db.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await transaction.CommitAsync().ConfigureAwait(false);
            }

        }

        //Инициализация пользователя и ролей
        private async Task InitializeIdentityAsync()
        {
            if (!await _RoleManager.RoleExistsAsync(Role.Administrator))
                await _RoleManager.CreateAsync(new Role { Name = Role.Administrator });

            if (!await _RoleManager.RoleExistsAsync(Role.User))
                await _RoleManager.CreateAsync(new Role { Name = Role.User });

            if(await _UserManager.FindByEmailAsync(User.Administrator) is null)
            {
                var admin = new User
                {
                    UserName = User.Administrator,
                    //Email = "admin@server.com"
                };

                var create_result = await _UserManager.CreateAsync(admin, User.AdminDefaultPassword);

                //Role.Administrator - это константа в классе Role
                if (create_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = create_result.Errors.Select(error => error.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя - Администратора: {string.Join(',', errors)}");
                }
            }

        }
    }
}
