using RotaViagem.Entidades.Entities;

namespace LocalViagem.Service.Interfaces
{
    public interface ILocalService
    {
        Task<Local> CreateAsync(Local dto);
        Task<Local> UpdateAsync(Local dto);
        Task RemoveAsync(int id);
        Task<Local> GetAsync(int id);
        Task<List<Local>> GetAllAsync();
        Task<Local> BuscaLocal(string NomeLocal);
    }
}
