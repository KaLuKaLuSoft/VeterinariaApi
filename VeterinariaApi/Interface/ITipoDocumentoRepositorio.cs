using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITipoDocumentoRepositorio
    {
        Task<List<DtoTipoDocumento>> GetTipoDocumento();
        Task<DtoTipoDocumento> GetTipoDocumentoById(int id);
        Task<DtoTipoDocumento> Create(DtoTipoDocumento tipodocumentoDto);
        Task<DtoTipoDocumento> Update(DtoTipoDocumento tipodocumentoDto);
        Task<bool> DeleteTipoDocumento(int id);
        Task<bool> TipoDocumentoExists(int id);
    }
}
