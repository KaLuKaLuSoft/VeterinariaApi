using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoLoginAcciones
    {
        public int ReglasId { get; set; }
        public int LoginId { get; set; }
        public List<int> LoginAccion { get; set; }
    }
}
