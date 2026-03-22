using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IConsumoAlimentoRepositorio
    {
        Task<List<DtoConsumoAlimento>> GetConsumoAlimento();
        Task<DtoConsumoAlimento> GetConsumoAlimentoById(int id);
        Task<DtoConsumoAlimento> Create(DtoConsumoAlimento consumoAlimentoDto);
        Task<DtoConsumoAlimento> Update(DtoConsumoAlimento consumoAlimentoDto);
        Task<bool> DeleteConsumoAlimento(int id);
        Task<bool> ConsumoAlimentoExists(int id);
    }
}
