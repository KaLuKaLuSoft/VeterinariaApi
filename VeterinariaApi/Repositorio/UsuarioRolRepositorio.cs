using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class UsuarioRolRepositorio : IUsuarioRolRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRolRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoUsuarioRol> Create(DtoUsuarioRol usuarioRolDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarUsuarioRol";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@ur_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioRolDto.UsuarioId
                };
                command.Parameters.Add(usuarioIdParam);

                var rolIdParam = new MySqlParameter("@ur_RolId", MySqlDbType.Int32)
                {
                    Value = usuarioRolDto.RolId
                };
                command.Parameters.Add(rolIdParam);

                command.ExecuteNonQuery();
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return usuarioRolDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear UsuarioRol", ex);
            }
        }
        public async Task<DtoUsuarioRol> Update(DtoUsuarioRol usuarioRolDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarUsuarioRol";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@ur_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioRolDto.UsuarioId
                };
                command.Parameters.Add(usuarioIdParam);

                var rolIdParam = new MySqlParameter("@ur_RolId", MySqlDbType.Int32)
                {
                    Value = usuarioRolDto.RolId
                };
                command.Parameters.Add(rolIdParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return usuarioRolDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear UsuarioRol", ex);
            }
        }
        public async Task<bool> DeleteUsuarioRol(int usuarioId, int rolId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarUsuarioRol";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@ur_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioId
                };

                var rolIdParam = new MySqlParameter("@ur_RolId", MySqlDbType.Int32)
                {
                    Value = rolId
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(usuarioIdParam);
                command.Parameters.Add(rolIdParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 0;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar UsuarioRol", ex);
            }
        }
        public async Task<List<DtoUsuarioRol>> GetUsuarioRol()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerUsuarioRol";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioroles = new List<DtoUsuarioRol>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var usuariorol = new DtoUsuarioRol
                        {
                            UsuarioId = reader.GetInt32(0),
                            RolId = reader.GetInt32(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?) null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3)
                        };
                        usuarioroles.Add(usuariorol);
                    }
                    await reader.CloseAsync();
                    return usuarioroles;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener UsuarioRol0", ex);
            }
        }
        public async Task<DtoUsuarioRol> GetUsuarioRolById(int usuarioId, int rolId)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerUsuarioRolPorId";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@ur_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioId
                };
                command.Parameters.Add(usuarioIdParam);
                var rolIdParam = new MySqlParameter("@ur_RolId", MySqlDbType.Int32)
                {
                    Value = rolId
                };
                command.Parameters.Add(rolIdParam);

                using(var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var usuariorol = new DtoUsuarioRol
                        {
                            UsuarioId = reader.GetInt32(0),
                            RolId = reader.GetInt32(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        };
                        await connection.CloseAsync();
                        return usuariorol;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener UsuarioRol", ex);
            }
        }
        public async Task<bool> UsuarioRolExists(int usuarioId, int rolId)
        {
            return await _context.UsuarioRol.AnyAsync(e => e.UsuarioId == usuarioId && e.RolId == rolId);
        }
    }
}
