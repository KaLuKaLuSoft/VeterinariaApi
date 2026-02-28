using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IRegistroRepositorio
    {
        Task<DtoRegistro> Create(DtoRegistro registroDto);
    }
}
