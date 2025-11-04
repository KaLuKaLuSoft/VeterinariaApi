using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Seguridad;

namespace VeterinariaApi.Repositorio
{
    public class LogueoRepositorio : ILogueoRepositorio
    {
        private readonly ApplicationDbContext? _context;
        private IMapper? _mapper;

        public LogueoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoLogin?> AuthenticateUser(string usuario, string password)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "Logueo";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioParam = new MySqlParameter("@Usuario", MySqlDbType.VarChar, 100) { Value = usuario };
                command.Parameters.Add(usuarioParam);

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();

                    var loginDto = new DtoLogin
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("LoginId")),
                        NombreUsuario = reader.GetString(reader.GetOrdinal("Usuario")),
                        Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
                        IdRol = reader.GetInt32(reader.GetOrdinal("IdRol")),
                        NombreRol = reader.GetString(reader.GetOrdinal("Roles")),
                        IdEmpleado = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                        Activo = reader.GetBoolean(reader.GetOrdinal("Activo")),
                    };

                    // Lanzar una excepción si el usuario está inactivo
                    if (loginDto.Activo != true)
                    {
                        throw new InvalidOperationException("El usuario está inactivo.");
                    }

                    var passwordHelper = new PasswordHelper();
                    bool passwordVerified = passwordHelper.VerifyPassword(password, loginDto.Contrasena);

                    if (!passwordVerified)
                    {
                        await transaction.RollbackAsync();
                        return null;
                    }

                    var sucursalDto = new DtoSucursales
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id_sucursal")),
                        NombreSucursal = reader.GetString(reader.GetOrdinal("Sucursal"))
                    };
                    string reglasStr = reader.IsDBNull(reader.GetOrdinal("Reglas"))
                    ? string.Empty
    :               reader.GetString(reader.GetOrdinal("Reglas"));

                    var reglas = reglasStr
                                       .Split(',')
                                       .Where(x => !string.IsNullOrWhiteSpace(x))
                                       .Select(int.Parse)
                                       .ToList();

                    var reglasDto = new DtoLoginAcciones { LoginAccion = reglas };
                    string menuStr = reader.IsDBNull(reader.GetOrdinal("Menu"))
                    ? string.Empty
    :               reader.GetString(reader.GetOrdinal("Menu"));
                    var menus = menuStr
                  .Split(',')
                  .Where(x => !string.IsNullOrWhiteSpace(x))
                  .Select(int.Parse)
                  .ToList();

                    var loginId = reader.GetInt32(reader.GetOrdinal("LoginId")); // Agregamos esta línea

                    var menuDto = new DtoLoginMenu
                    {
                        LoginMenu = menus,
                        LoginId = loginId  // Agregamos el LoginId al DTO
                    };
                    //var cajaDto = new CajasDto { Id = reader.GetInt32(reader.GetOrdinal("Caja")) };

                    await reader.CloseAsync();
                    await transaction.CommitAsync();

                    loginDto.Sucursal = sucursalDto;
                    loginDto.Reglas = new List<DtoLoginAcciones> { reglasDto };
                    loginDto.Menus = new List<DtoLoginMenu> { menuDto };
                    //loginDto.Caja = cajaDto;

                    return loginDto;
                }
                else
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
            catch (InvalidOperationException ex) when (ex.Message == "El usuario está inactivo.")
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al ingresar", ex);
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }
    }
}
