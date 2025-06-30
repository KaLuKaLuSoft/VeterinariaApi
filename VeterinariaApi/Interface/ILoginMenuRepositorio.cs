using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ILoginMenuRepositorio
    {
        Task<List<DtoLoginMenu>> GetLoginMenu();
        Task<DtoLoginMenu> GetLoginMenuById(int id);
        Task<DtoLoginMenu> Create(DtoLoginMenu loginmenuDto);
        Task<DtoLoginMenu> Update(DtoLoginMenu loginmenuDto);
        Task<bool> DeleteLoginMenu(int id);
        Task<bool> LoginMenuExists(int id);
    }
}
