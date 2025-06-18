using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IPaisesRepositorio
    {
        Task<List<DtoPaises>> GetPaises();
        Task<DtoPaises> GetPaisesById(int id);
        Task<DtoPaises> Create(DtoPaises paisesDto);
        Task<DtoPaises> Update(DtoPaises paisesDto);
        Task<bool> DeletePaises(int id);
        Task<bool> PaisesExists(int id);
    }
}
