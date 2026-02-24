using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEmpresaRepositorio
    {
        // Obtener la lista de todas las empresas
        Task<List<DtoEmpresa>> GetEmpresas();

        // Obtener una empresa específica por su Id
        Task<DtoEmpresa> GetEmpresaById(int id);

        // Crear una nueva empresa y devolver el objeto creado
        Task<DtoEmpresa> Create(DtoEmpresa empresaDto);

        // Actualizar una empresa y devolver el objeto con los cambios
        Task<DtoEmpresa> Update(DtoEmpresa empresaDto);

        // Borrado lógico (devuelve true si se marcó como IsDeleted)
        Task<bool> DeleteEmpresa(int id);

        // Verificar si la empresa existe antes de operar con ella
        Task<bool> EmpresaExists(int id);
    }
}
