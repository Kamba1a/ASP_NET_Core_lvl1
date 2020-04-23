using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebStore.Domain.Entities;

namespace WebStore.DAL
{
    public class WebStoreContext: DbContext
    {
        public WebStoreContext(DbContextOptions options) : base(options)
        {
        }
        //для создания таблиц
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Section> Sections { get; set; }
    }
}
