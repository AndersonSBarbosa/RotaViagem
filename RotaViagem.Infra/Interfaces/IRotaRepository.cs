using RotaViagem.Entidades.Entities;

namespace RotaViagem.Infra.Interfaces
{
    public interface IRotaRepository : IBaseRepository<Rota>
    {
        Task<Rota> ObterRota(string origem, string destino);
    }
}
