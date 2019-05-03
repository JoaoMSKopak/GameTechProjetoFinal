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
        public DbSet<Prod_Venda> Prod_Vendas { get; set; }
        public DbSet<Prod_Aluguel> Prod_Aluguels { get; set; }
        public DbSet<Prod_Troca> Prod_Trocas { get; set; }
        public DbSet<Historico> Historicos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().
                 HasMany(v => v.Prod_Vendas).
                 WithMany(u => u.Usuarios).
                 Map(vu =>

                        {
                            vu.MapLeftKey("UsuarioRefId");
                            vu.MapRightKey("Prod_VendaRefId");
                            vu.ToTable("UsuarioProd_Venda");

                        }
                 );

            modelBuilder.Entity<Usuario>().
                         HasMany(t => t.Prod_Trocas).
                         WithMany(u => u.Usuarios).
                         Map(tu =>
                         {
                             tu.MapLeftKey("UsuarioRefId");
                             tu.MapRightKey("Prod_TrocaRefId");
                             tu.ToTable("UsuarioProd_Troca");
                         });

            modelBuilder.Entity<Usuario>().
                        HasMany(a => a.Prod_Aluguels).
                        WithMany(u => u.Usuarios).
                        Map(au =>
                        {
                            au.MapLeftKey("UsuarioRefId");
                            au.MapRightKey("Prod_AluguelRefId");
                            au.ToTable("UsuarioProd_Aluguel");
                        });
        }
    }
}