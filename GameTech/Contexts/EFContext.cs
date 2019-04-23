using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameTech.Models;
using System.Data.Entity;

namespace GameTech.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base("DBGameTech") { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}