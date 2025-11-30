using Microsoft.EntityFrameworkCore;
using MyApp.Server.Models;
using MyApp.Shared;
using System;

namespace MyApp.Server.Data
{
    public class AppDbContextCustom : DbContext
    {
        public AppDbContextCustom(DbContextOptions<AppDbContextCustom> options)
            : base(options)
        {
        }

    


    }
}