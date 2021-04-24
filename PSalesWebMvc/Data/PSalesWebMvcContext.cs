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

        public DbSet<PSalesWebMvc.Models.Department> Department { get; set; }
    }
}
