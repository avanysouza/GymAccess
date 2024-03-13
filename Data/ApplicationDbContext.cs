using GymAcess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace GymAcess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Utilizador> Utilizadores { get; set; }

        public DbSet<Exercicio> Exercicios { get; set; }

        public DbSet<Plano> Planos { get; set; }

        public DbSet<Tipo> Tipos { get; set; }

        public DbSet<Objetivo> Objetivos { get; set; }

        public DbSet<FichaExercicio> Fichas { get; set; }
    }
}
