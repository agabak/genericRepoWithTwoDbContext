using Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data.DataContexts
{
    public class AuthContext: IdentityDbContext<StoreUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options): base(options)
        {
        }

        public DbSet<StoreUser> StoreUsers { get; set; }
    }
}
