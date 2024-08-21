using AutoMapper;
using RotaViagem.Entidades.Entities;
using RotaViagem.Entidades.Exceptions;
using RotaViagem.Infra.Interfaces;
using RotaViagem.Service.Interfaces;

namespace RotaViagem.Service.Services
{
    public class RotaService : IRotaService
    {
        private readonly IMapper _mapper;
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IMapper mapper, IRotaRepository rotaRepository)
        {
            _mapper=mapper;
            _rotaRepository=rotaRepository;
        }

        public IMapper Mapper {get;}
        public IRotaRepository RotaRepository { get; }
        public async Task<Rota> CreateAsync(Rota dto)
        {
            try
            {
                var itemExist = await ObterRota(dto.Origem, dto.Destino);

                if (itemExist != null)
                    throw new DomainExceptions("Já existe erra Rota Informada");

                var itemCreated = await _rotaRepository.CreateAsync(dto);
                return itemCreated;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Rota> UpdateAsync(Rota dto)
        {
            try
            {
                var itemExist = await GetAsync(dto.Id);

                if (itemExist != null)
                    throw new DomainExceptions("não existe Rota com esse ID informado!");

                var clientUpdate = await _rotaRepository.UpdateAsync(dto);
                return clientUpdate;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<Rota>> GetAllAsync()
        {
            try
            {
                var allItens = await _rotaRepository.GetAllAsync();
                return allItens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Rota> GetAsync(int id)
        {
            try
            {
                var item = await _rotaRepository.GetAsync(id);
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
                await _rotaRepository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Rota> ObterRota(string origem, string destino)
        {
            try
            {
                var item = await _rotaRepository.ObterRota(origem, destino);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Rota> ObterMelhorRota(string origem, string destino)
        {
            var todasRotas = await GetAllAsync();
            if (todasRotas.Count == 0)
                return null;

            var rotasPossiveis = CalcularTodasRotas(origem, destino, todasRotas);
            if (rotasPossiveis.Count == 0)
                return null;

            // Encontrar a rota com o menor custo total
            var melhorRota = rotasPossiveis.OrderBy(r => r.Valor).First();

            return melhorRota;
        }

        private List<Rota> CalcularTodasRotas(string origem, string destino, List<Rota> todasRotas)
        {
            var rotasPossiveis = new List<Rota>();
            CalcularRotasRecursivamente(origem, destino, new List<string>(), todasRotas, ref rotasPossiveis);
            return rotasPossiveis;
        }

        private void CalcularRotasRecursivamente(string origem, string destino, List<string> rotaAtual, List<Rota> todasRotas, ref List<Rota> rotasPossiveis)
        {
            rotaAtual.Add(origem);

            if (origem == destino)
            {
                var rota = new Rota
                {
                    Origem = string.Join(" - ", rotaAtual),
                    Destino = destino,
                    Valor = CalcularValorRota(rotaAtual, todasRotas)
                };
                rotasPossiveis.Add(rota);
            }
            else
            {
                foreach (var proximaRota in todasRotas.Where(r => r.Origem == origem))
                {
                    CalcularRotasRecursivamente(proximaRota.Destino, destino, rotaAtual, todasRotas, ref rotasPossiveis);
                }
            }

            rotaAtual.RemoveAt(rotaAtual.Count - 1);
        }

        private decimal CalcularValorRota(List<string> rotaAtual, List<Rota> todasRotas)
        {
            decimal valorTotal = 0;
            for (int i = 0; i < rotaAtual.Count - 1; i++)
            {
                var rota = todasRotas.FirstOrDefault(r => r.Origem == rotaAtual[i] && r.Destino == rotaAtual[i + 1]);
                if (rota == null)
                    throw new InvalidOperationException($"Rota de {rotaAtual[i]} para {rotaAtual[i + 1]} não encontrada.");

                valorTotal += rota.Valor;
            }
            return valorTotal;
        }
    }
}
