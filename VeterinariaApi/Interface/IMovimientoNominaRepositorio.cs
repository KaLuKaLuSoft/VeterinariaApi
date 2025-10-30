using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IMovimientoNominaRepositorio
    {
        Task<List<DtoMovimientosNomina>> GetMovimientoNomina();
        Task<DtoMovimientosNomina> GetMovimientoNominaById(int id);
        Task<DtoMovimientosNomina> Create(DtoMovimientosNomina movimientoNominaDto);
        Task<DtoMovimientosNomina> Update(DtoMovimientosNomina movimientoNominaDto);
        Task<bool> DeleteMovimientoNomina(int id);
        Task<bool> MovimientoNominaExists(int id);
    }
}
