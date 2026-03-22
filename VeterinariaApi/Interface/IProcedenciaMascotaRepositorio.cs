using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IProcedenciaMascotaRepositorio
    {
        Task<List<DtoProcedenciaMascota>> GetProcedenciaMascota();
        Task<DtoProcedenciaMascota> GetProcedenciaMascotaById(int id);
        Task<DtoProcedenciaMascota> Create(DtoProcedenciaMascota procedenciaMascotaDto);
        Task<DtoProcedenciaMascota> Update(DtoProcedenciaMascota procedenciaMascotaDto);
        Task<bool> DeleteProcedenciaMascota(int id);
        Task<bool> ProcedenciaMascotaExists(int id);
    }
}
