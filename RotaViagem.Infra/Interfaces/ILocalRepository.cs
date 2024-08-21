using RotaViagem.Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Infra.Interfaces
{
    public interface ILocalRepository : IBaseRepository<Local>
    {
        Task<Local> BuscaLocal(string NomeLocal);
    }
}
