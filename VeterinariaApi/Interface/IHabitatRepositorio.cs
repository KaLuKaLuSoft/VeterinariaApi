using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IHabitatRepositorio
    {
        Task<List<DtoHabitatMascota>> GetHabitat();
        Task<DtoHabitatMascota> GetHabitatById(int id);
        Task<DtoHabitatMascota> Create(DtoHabitatMascota habitatDto);
        Task<DtoHabitatMascota> Update(DtoHabitatMascota habitatDto);
        Task<bool> DeleteHabitat(int id);
        Task<bool> HabitatExists(int id);
    }
}
