using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ProductContext : DbContext
    {
        public ProductContext() : base("conn") { }
        public DbSet<Product> Products { get; set; }
    }
}