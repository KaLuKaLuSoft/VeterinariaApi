using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class LoginMenu
    {
        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public SubModulo SubModulo { get; set; }
        public int LoginId { get; set; }
        [ForeignKey("LoginId")]
        public Login Login { get; set; }
    }
}
