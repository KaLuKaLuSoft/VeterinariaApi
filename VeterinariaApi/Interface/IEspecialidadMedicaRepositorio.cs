using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEspecialidadMedicaRepositorio
    {
        Task<List<DtoEpecialidadesMedicas>> GetEspecialidades();
        Task<DtoEpecialidadesMedicas> GetEspecialidadesById(int id);
        Task<DtoEpecialidadesMedicas> Create(DtoEpecialidadesMedicas especialidadesDto);
        Task<DtoEpecialidadesMedicas> Update(DtoEpecialidadesMedicas especialidadesDto);
        Task<bool> DeleteEspecialidades(int id);
        Task<bool> EspecialidadesExists(int id);
    }
}