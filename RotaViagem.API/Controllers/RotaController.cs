using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.API.Utilities;
using RotaViagem.API.ViewModels;
using RotaViagem.Entidades.Entities;
using RotaViagem.Entidades.Exceptions;
using RotaViagem.Service.Interfaces;

namespace RotaViagem.API.Controllers
{
    [ApiController]
    public class RotaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRotaService _rotaService;

        public RotaController(IMapper mapper, IRotaService rotaService)
        {
            _mapper=mapper;
            _rotaService=rotaService;
        }

        [HttpPost]
        [Route("/api/v1/Rota/create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRotaViewModel itemViewModel)
        {
            try
            {
                var itemDTO = _mapper.Map<Rota>(itemViewModel);
                var itemCreated = await _rotaService.CreateAsync(itemDTO);
                return Ok(new ResultViewModel
                {
                    Message = "Rota criada com sucesso!",
                    Success = true,
                    Data = itemCreated
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Route("/api/v1/Rota/update")]
        public async Task<IActionResult> updateasync([FromBody] UpdateRotaViewModel itemviewmodel)
        {
            var itemDTO = _mapper.Map<Rota>(itemviewmodel);
            var itemUpdated = await _rotaService.UpdateAsync(itemDTO);

            return Ok(new ResultViewModel
            {
                Message = "Rota atualizada com sucesso!",
                Success = true,
                Data = itemUpdated
            });
        }

        [HttpDelete]
        [Route("/api/v1/Rota/remove/{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            await _rotaService.RemoveAsync(id);
            return Ok(new ResultViewModel
            {
                Message = "Item removido com sucesso!",
                Success = true,
                Data = null
            });
        }

        [HttpGet]
        [Route("/api/v1/Rota/get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await _rotaService.GetAsync(id);
            return Ok(item);
        }


        [HttpGet]
        [Route("/api/v1/Rota/GetAll")]
        public async Task<IActionResult> GetAsync()
        {
            var allItens = await _rotaService.GetAllAsync();

            return Ok(new ResultViewModel
            {
                Message = "Items encontrados com sucesso!",
                Success = true,
                Data = allItens
            });
        }


        [HttpGet]
        [Route("/api/v1/Rota/MelhorRota")]
        public async Task<IActionResult> ObterMelhorRota([FromQuery] string origem, string destino)
        {
            var itens = await _rotaService.ObterMelhorRota(origem, destino);

            return Ok(itens);
        }
    }
}
