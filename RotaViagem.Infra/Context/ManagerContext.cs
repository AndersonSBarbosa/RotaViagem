using Microsoft.EntityFrameworkCore;
using RotaViagem.Entidades.Entities;

namespace RotaViagem.Infra.Context
{
    public class ManagerContext : DbContext
    {

        public ManagerContext()
        { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        { }
        public virtual DbSet<Rota> Rota { get; set; }
        public virtual DbSet<Local> Local { get; set; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfiguration(new UsersMap());
        //    //builder.ApplyConfiguration(new ColorsMap());
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer();

    }
}
