using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Dto
{
    public class DtoLoginMenu
    {
        public int LoginId { get; set; }
        public List<int> MenuId { get; set; }
    }
}
