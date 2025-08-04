using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ICategoriaActivoFijoRepositorio
    {
        Task<List<DtoCategoriaActivoFijo>> GetCategoriaActivoFijO();
        Task<DtoCategoriaActivoFijo> GetCategoriaActivoFijoById(int id);
        Task<DtoCategoriaActivoFijo> Create(DtoCategoriaActivoFijo CategoriaActivoFijoDto);
        Task<DtoCategoriaActivoFijo> Update(DtoCategoriaActivoFijo CategoriaActivoFijoDto);
        Task<bool> DeleteCategoriaActivoFijo(int id);
        Task<bool> CategoriaActivoFijoExists(int id);
    }
}
