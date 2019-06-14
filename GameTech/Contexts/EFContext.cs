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

        public DbSet<Proposta> Propostas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Prod_Venda>()
                .HasRequired(u => u.UsuarioAtual).
                WithMany(v => v.Prod_Vendas).HasForeignKey(u => u.UsuAtualID);

            modelBuilder.Entity<Prod_Troca>()
                .HasRequired(u => u.UsuarioAtual).
                WithMany(t => t.Prod_Trocas).HasForeignKey(u => u.UsuAtualID);

            modelBuilder.Entity<Prod_Aluguel>()
                .HasRequired(u => u.UsuarioAtual).
                WithMany(a => a.Prod_Aluguels).HasForeignKey(u => u.UsuAtualID);

            modelBuilder.Entity<Proposta>()
                .HasRequired(u => u.UsuarioAtual)
                .WithMany(p => p.Propostas).HasForeignKey(u => u.UsuAtualID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proposta>()
                .HasRequired(p => p.Prod_Troca)
                .WithMany(pr => pr.Propostas).HasForeignKey(t => t.ProdTAtualID);

            modelBuilder.Entity<Proposta>()
                .HasRequired(p => p.Prod_Troca)
                .WithMany(pr => pr.Propostas).HasForeignKey(t => t.Prod_P_TrocarID);


            //modelBuilder.Entity<Proposta>()
            //    .HasRequired(u => u.UsuarioReceb)
            //    .WithMany(p => p.Propostas).HasForeignKey(u => u.UsuarioRecebID)
            //    .WillCascadeOnDelete(false);


            modelBuilder.Entity<Usuario>()
                .HasMany(v => v.Prod_Vendas)
                .WithRequired(u => u.UsuarioAtual)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(t => t.Prod_Trocas)
                .WithRequired(u => u.UsuarioAtual)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(a => a.Prod_Aluguels)
                .WithRequired(u => u.UsuarioAtual)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(p => p.Propostas)
                .WithRequired(u => u.UsuarioAtual)
                .WillCascadeOnDelete(false);
            //.WillCascadeOnDelete();

            //modelBuilder.Entity<Usuario>()
            //    .HasMany(p => p.Propostas)
            //    .WithRequired(u => u.UsuarioReceb)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Properties<decimal>().
                Configure(v => v.HasPrecision(5, 2));

        }
    }
}