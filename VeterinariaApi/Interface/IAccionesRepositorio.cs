using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IAccionesRepositorio
    {
        Task<List<DtoAcciones>> GetAcciones();
        Task<DtoAcciones> GetAccionesById(int id);
        Task<DtoAcciones> Create(DtoAcciones accionesDto);
        Task<DtoAcciones> Update(DtoAcciones accionesDto);
        Task<bool> DeleteAcciones(int id);
        Task<bool> AccionesExists(int id);
    }
}
