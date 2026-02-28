using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Dto
{
    public class DtoRegistro
    {
        // Datos Empresa
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        public string? NombreComercial { get; set; }
        [Required(ErrorMessage = "Indique el número de trabajadores")]
        public int? NumeroTrabajadores { get; set; }
        [Required]
        public int IdPais { get; set; }
        [Required]
        public int IdCiudad { get; set; }

        // Datos Empleado
        [Required(ErrorMessage = "El nombre del empleado es obligatorio")]
        public string? NombreEmpleado { get; set; }
        public string? Celular { get; set; }

        // Datos Login
        [Required(ErrorMessage = "El usuario/correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string? Usuario { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? Contrasena { get; set; }
    }

    public class DtoRegistroResult
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdEmpleado { get; set; }
        public int IdLogin { get; set; }
    }
}
