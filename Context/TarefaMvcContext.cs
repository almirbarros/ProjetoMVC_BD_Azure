using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Context
{
    public class TarefaMvcContext : DbContext
    {
        public TarefaMvcContext(DbContextOptions<TarefaMvcContext> options) : base(options)
        {
        }

        public DbSet<TarefaMvc> Tarefas { get; set; }
    }
}