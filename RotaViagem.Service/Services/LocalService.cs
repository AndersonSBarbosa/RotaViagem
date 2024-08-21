using AutoMapper;
using LocalViagem.Service.Interfaces;
using RotaViagem.Entidades.Entities;
using RotaViagem.Entidades.Exceptions;
using RotaViagem.Infra.Interfaces;

namespace RotaViagem.Service.Services
{
    public class LocalService : ILocalService
    {
        private readonly IMapper _mapper;
        private readonly ILocalRepository _localRepository;

        public LocalService(IMapper mapper, ILocalRepository localRepository)
        {
            _mapper=mapper;
            _localRepository=localRepository;
        }

        public IMapper Mapper { get; }
        public ILocalRepository LocalRepository { get; }

        public async Task<Local> CreateAsync(Local dto)
        {
            try
            {
                var itemExist = await BuscaLocal(dto.NomeLocal);

                if (itemExist != null)
                    throw new DomainExceptions("Já existe erra Rota Informada");

                var itemCreated = await _localRepository.CreateAsync(dto);
                return itemCreated;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Local> UpdateAsync(Local dto)
        {
            try
            {
                var itemExist = await GetAsync(dto.Id);

                if (itemExist != null)
                    throw new DomainExceptions("não existe Rota com esse ID informado!");

                var clientUpdate = await _localRepository.UpdateAsync(dto);
                return clientUpdate;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<Local>> GetAllAsync()
        {
            try
            {
                var allItens = await _localRepository.GetAllAsync();
                return allItens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Local> GetAsync(int id)
        {
            try
            {
                var item = await _localRepository.GetAsync(id);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                await _localRepository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Local> BuscaLocal(string NomeLocal)
        {
            try
            {
                var item = await _localRepository.BuscaLocal(NomeLocal);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
