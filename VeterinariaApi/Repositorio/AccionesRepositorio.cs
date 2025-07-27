using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using System.Runtime.InteropServices;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class AccionesRepositorio : IAccionesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccionesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoAcciones> Create(DtoAcciones accionesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarAcciones";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@a_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreAccionesParam = new MySqlParameter("@a_NombreAcciones", MySqlDbType.VarChar, 100)
                {
                    Value = accionesDto.NombreAcciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreAccionesParam);

                var descripcionParam = new MySqlParameter("@a_Descripcion", MySqlDbType.VarChar, 100)
                {
                    Value = accionesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return accionesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la acción", ex);
            }
        }
        public async Task<DtoAcciones> Update(DtoAcciones accionesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarAcciones";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@a_Id", MySqlDbType.Int32)
                {
                    Value = accionesDto.Id > 0 ? (object)accionesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreAccionesParam = new MySqlParameter("@a_NombreAcciones", MySqlDbType.VarChar, 100)
                {
                    Value = accionesDto.NombreAcciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreAccionesParam);

                var descripcionParam = new MySqlParameter("@a_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = accionesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return accionesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la acción", ex);
            }
        }
        public async Task<bool> DeleteAcciones(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarAcciones";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@a_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar la acción", ex);
            }
        }
        public async Task<List<DtoAcciones>> GetAcciones()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerAcciones";
                command.CommandType = CommandType.StoredProcedure;

                var acciones = new List<DtoAcciones>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var accion = new DtoAcciones
                        {
                            Id = reader.GetInt32(0),
                            NombreAcciones = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        acciones.Add(accion);
                    }
                    await reader.CloseAsync();
                    return acciones;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las acciones", ex);
            }
        }
        public async Task<DtoAcciones> GetAccionesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerAccionesPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@a_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var acciones = new DtoAcciones
                        {
                            Id = reader.GetInt32(0),
                            NombreAcciones = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        await connection.CloseAsync();
                        return acciones;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la acción. ", ex);
            }
        }
        public async Task<bool> AccionesExists(int id)
        {
            return await _context.Acciones.AnyAsync(a => a.Id == id);
        }
    }
}