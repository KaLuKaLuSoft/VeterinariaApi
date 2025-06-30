using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Dto
{
    public class DtoLoginMenu
    {
        public int SubMenuId { get; set; }
        public int LoginId { get; set; }
        public List<int> LoginMenu { get; set; }
    }
}
