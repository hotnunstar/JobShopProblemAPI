using Microsoft.EntityFrameworkCore;

namespace BackEndV2.Context
{    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<Simulacao> Simulacao { get; set; }
        public DbSet<Job> Job { get; set; }

        public DbSet<Maquina> Maquina { get; set; }

        public DbSet<Operacao> Operacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plano>().HasKey(c => new { c.IdUtilizador, c.IdJob, c.IdOperacao, c.IdSimulacao });
        }
    }
}
