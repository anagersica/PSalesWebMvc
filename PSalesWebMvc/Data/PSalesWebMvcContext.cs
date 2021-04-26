using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSalesWebMvc.Models;

namespace PSalesWebMvc.Data
{
    public class PSalesWebMvcContext : DbContext
    {
        public PSalesWebMvcContext (DbContextOptions<PSalesWebMvcContext> options)
            : base(options)
        {
        }
        //inserindo DbSet para que o Entity Frame reconheça as classes inseridas
        public DbSet<PSalesWebMvc.Models.Department> Department { get; set; }
        public DbSet<PSalesWebMvc.Models.Seller> Seller { get; set; }
        public DbSet<PSalesWebMvc.Models.SalesRecord> SalesRecord { get; set; }
    }
}
