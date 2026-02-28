using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Seguridad;

namespace VeterinariaApi.Repositorio
{
    public class RegistroRepositorio : IRegistroRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHelper _passwordHelper;

        public RegistroRepositorio(ApplicationDbContext context, PasswordHelper passwordHelper)
        {
            _context = context;
            _passwordHelper = passwordHelper;
        }

        public async Task<DtoRegistro> Create(DtoRegistro registroDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var connection = _context.Database.GetDbConnection();
                if (connection.State != ConnectionState.Open) await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "registro"; // Tu SP
                command.CommandType = CommandType.StoredProcedure;

                // --- Cifrado de contraseña antes del mapeo ---
                // Asumiendo que _passwordHelper ya está inyectado en el constructor
                string hashedPassword = _passwordHelper.HashPassword(registroDto.Contrasena);

                // Mapeo manual directo a parámetros de MySql
                var NombreComercial = new MySqlParameter("p_NombreComercial", MySqlDbType.VarChar)
                {
                    Value = registroDto.NombreComercial
                };
                command.Parameters.Add(NombreComercial);

                var NumeroTrabajadores = new MySqlParameter("p_NumeroTrabajadores", MySqlDbType.Int32)
                {
                    Value = registroDto.NumeroTrabajadores ?? (object)DBNull.Value
                };
                command.Parameters.Add(NumeroTrabajadores);

                var IdPais = new MySqlParameter("p_IdPais", MySqlDbType.Int32)
                {
                    Value = registroDto.IdPais
                };
                command.Parameters.Add(IdPais);

                var IdCiudad = new MySqlParameter("p_IdCiudad", MySqlDbType.Int32)
                {
                    Value = registroDto.IdCiudad
                };
                command.Parameters.Add(IdCiudad);

                var NombreEmpleado = new MySqlParameter("p_NombreEmpleado", MySqlDbType.VarChar)
                {
                    Value = registroDto.NombreEmpleado
                };
                command.Parameters.Add(NombreEmpleado);

                var Celular = new MySqlParameter("p_Celular", MySqlDbType.VarChar)
                {
                    Value = registroDto.Celular ?? (object)DBNull.Value
                };
                command.Parameters.Add(Celular);

                var Usuario = new MySqlParameter("p_Usuario", MySqlDbType.VarChar)
                {
                    Value = registroDto.Usuario
                };
                command.Parameters.Add(Usuario);

                // Aquí asignamos la contraseña ya cifrada
                var Contrasena = new MySqlParameter("p_Contrasena", MySqlDbType.VarChar)
                {
                    Value = hashedPassword
                };
                command.Parameters.Add(Contrasena);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return registroDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error en el registro integral", ex);
            }
        }
    }
}