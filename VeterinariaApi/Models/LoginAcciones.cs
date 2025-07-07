using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class LoginAcciones
    {
        public int ReglasId { get; set; }
        [ForeignKey("ReglasId")]
        public Acciones? Regla { get; set; }

        public int LoginId { get; set; }
        [ForeignKey("LoginId")]
        public Login? Login { get; set; }
    }
}
