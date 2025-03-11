using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using REVISAO.Models;

namespace revisao.models;

public class Context : DbContext
{
    public DbSet<User> TabelaUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=Banco.db");
    }

}
