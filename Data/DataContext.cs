
using ControleGastos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Data
{
    public class DataContext : DbContext    
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<FormaSaida> FormasSaida { get; set;}
        public DbSet<Transacao> Transacoes { get; set;}
        public DbSet<Classificacao> Classificacoes { get; set; }

        
    }
}
