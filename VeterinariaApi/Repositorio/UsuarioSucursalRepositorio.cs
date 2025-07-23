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
    public class UsuarioSucursalRepositorio : IUsuarioSucursalRepositorio
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        public UsuarioSucursalRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoUsuarioSucursal> Create(DtoUsuarioSucursal usuarioSucursalDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarUsuarioSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@us_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioSucursalDto.UsuarioId
                };
                command.Parameters.Add(usuarioIdParam);

                var sucursalIdParam = new MySqlParameter("@us_SucursalId", MySqlDbType.Int32)
                {
                    Value = usuarioSucursalDto.SucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                command.ExecuteNonQuery();
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return usuarioSucursalDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear UsuarioSucursal", ex);
            }
        }
        public async Task<DtoUsuarioSucursal> Update(DtoUsuarioSucursal usuarioSucursalDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarUsuarioSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@us_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioSucursalDto.UsuarioId
                };
                command.Parameters.Add(usuarioIdParam);

                var sucursalIdParam = new MySqlParameter("@us_SucursalId", MySqlDbType.Int32)
                {
                    Value = usuarioSucursalDto.SucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return usuarioSucursalDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar UsuarioSucursal", ex);
            }
        }
        public async Task<bool> DeleteUsuarioSucursal(int usuarioId, int sucursalId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarUsuarioSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@us_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioId
                };

                var sucursalIdParam = new MySqlParameter("@us_SucursalId", MySqlDbType.Int32)
                {
                    Value = sucursalId
                };

                var resultadoParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(usuarioIdParam);
                command.Parameters.Add(sucursalIdParam);
                command.Parameters.Add(resultadoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultadoParam.Value);
                return result == 0;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar UsuarioSucursal", ex);
            }
        }
        public async Task<List<DtoUsuarioSucursal>> GetUsuarioSucursal()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerUsuarioSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var usuariosucursales = new List<DtoUsuarioSucursal>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var usuarioSucursal = new DtoUsuarioSucursal
                        {
                            UsuarioId = reader.GetInt32(0),
                            SucursalId = reader.GetInt32(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?) null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3)
                        };
                        usuariosucursales.Add(usuarioSucursal);
                    }
                    await reader.CloseAsync();
                    return usuariosucursales;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener UsuarioSucursal", ex);
            }
        }

        public async Task<DtoUsuarioSucursal> GetUsuarioSucursalById(int usuarioId, int sucursalId)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerUsuarioSucursalPorId";
                command.CommandType = CommandType.StoredProcedure;

                var usuarioIdParam = new MySqlParameter("@us_UsuarioId", MySqlDbType.Int32)
                {
                    Value = usuarioId
                };
                command.Parameters.Add(usuarioIdParam);
                var sucursalIdParam = new MySqlParameter("@us_SucursalId", MySqlDbType.Int32)
                {
                    Value = sucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var usuarioSucursal = new DtoUsuarioSucursal
                        {
                            UsuarioId = reader.GetInt32(0),
                            SucursalId = reader.GetInt32(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        };
                        await connection.CloseAsync();
                        return usuarioSucursal;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener UsuarioSucursal por ID", ex);
            }
        }
        public async Task<bool> UsuarioSucursalExists(int usuarioId, int sucursalId)
        {
            return await _context.UsuarioSucursal.AnyAsync(us => us.UsuarioId == usuarioId && us.SucursalId == sucursalId);
        }
    }
}
