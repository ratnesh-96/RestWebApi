﻿using Microsoft.EntityFrameworkCore;
using RestWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWebApi.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext>options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
