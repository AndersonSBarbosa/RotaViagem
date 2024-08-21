using Microsoft.EntityFrameworkCore;
using RotaViagem.Entidades.Entities;
using RotaViagem.Infra.Context;
using RotaViagem.Infra.Interfaces;

namespace RotaViagem.Infra.Repositories
{
    public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        private readonly ManagerContext _context;

        public RotaRepository(ManagerContext context) : base(context)
        {
            _context=context;
        }

        public async Task<Rota> ObterRota(string origem, string destino)
        {
            return await _context.Rota.FirstOrDefaultAsync(r => r.Origem == origem && r.Destino == destino);
        }

    }
}
