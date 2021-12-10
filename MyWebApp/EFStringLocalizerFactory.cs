using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using MyWebApp.Models;
namespace MyWebApp
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        string _connectionString;
        public EFStringLocalizerFactory(string connection)
        {
            _connectionString = connection;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }

        private IStringLocalizer CreateStringLocalizer()
        {
            LocalizationContext _db = new LocalizationContext(
                new DbContextOptionsBuilder<LocalizationContext>()
                    .UseSqlServer(_connectionString)
                    .Options);
            // инициализация базы данных
            if (!_db.Cultures.Any())
            {
                _db.AddRange(
                    new Culture
                    {
                        Name = "en-US",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "About", Value = "Vladko" },
                            new Resource { Key = "Blog", Value = "Blog" }
                        }
                    },
                    new Culture
                    {
                        Name = "uk-UA",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "About", Value = "Про Владка" },
                            new Resource { Key = "Blog", Value = "Блог" }
                        }
                    }
                );
                _db.SaveChanges();
            }
            
            return new EFStringLocalizer(_db);
        }
    }
}
