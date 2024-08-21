using Microsoft.EntityFrameworkCore;
using RotaViagem.Entidades.Entities;
using RotaViagem.Infra.Context;
using RotaViagem.Infra.Interfaces;

namespace RotaViagem.Infra.Repositories
{
    public class LocalRepository : BaseRepository<Local>, ILocalRepository
    {
        private readonly ManagerContext _context;
        public LocalRepository(ManagerContext context) : base(context)
        {
            _context=context;
        }

        public async Task<Local> BuscaLocal(string NomeLocal)
        {
            return await _context.Local.FirstOrDefaultAsync(r => r.NomeLocal == NomeLocal);
        }
    }
}
