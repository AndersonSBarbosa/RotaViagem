using RotaViagem.Entidades.Entities;

namespace RotaViagem.Service.Interfaces
{
    public interface IRotaService
    {
        Task<Rota> CreateAsync(Rota dto);
        Task<Rota> UpdateAsync(Rota dto);
        Task RemoveAsync(int id);
        Task<Rota> GetAsync(int id);
        Task<List<Rota>> GetAllAsync();
        Task<Rota> ObterRota(string origem, string destino);
        Task<Rota> ObterMelhorRota(string origem, string destino);
    }
}
