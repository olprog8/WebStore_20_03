using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebStore.DAL.Context;

namespace WebStore1p.Data
{
    public class WebStoreDBInitializer
    {
        private readonly WebStoreDB _db;

        //5 в конструкторе запрашиваем контекст БД
        public WebStoreDBInitializer(WebStoreDB db) => _db = db;
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

            if (await _db.Products.AnyAsync()) return;

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
    }
}
