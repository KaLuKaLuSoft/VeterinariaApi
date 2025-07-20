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
    public class TipoAusenciaRepositorio : ITipoAusenciaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoAusenciaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoTipoAusencia> Create(DtoTipoAusencia tipoAusenciaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoAusencia";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ta_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreAusenciaParam = new MySqlParameter("@ta_NombreAusencia", MySqlDbType.VarChar, 100)
                {
                    Value = tipoAusenciaDto.NombreAusencia ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreAusenciaParam);

                var requiereAprobacionParam = new MySqlParameter("@ta_RequiereAprobacion", MySqlDbType.Bit)
                {
                    Value = tipoAusenciaDto.RequiereAprobacion
                };
                command.Parameters.Add(requiereAprobacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoAusenciaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el tipo de ausencia", ex);
            }
        }
        public async Task<DtoTipoAusencia> Update(DtoTipoAusencia tipoAusenciaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoAusencia";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ta_Id", MySqlDbType.Int32)
                {
                    Value = tipoAusenciaDto.Id > 0 ? (object)tipoAusenciaDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreAusenciaParam = new MySqlParameter("@ta_NombreAusencia", MySqlDbType.VarChar, 100)
                {
                    Value = tipoAusenciaDto.NombreAusencia ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreAusenciaParam);

                var requiereAprobacionParam = new MySqlParameter("@ta_RequiereAprobacion", MySqlDbType.Bit)
                {
                    Value = tipoAusenciaDto.RequiereAprobacion
                };
                command.Parameters.Add(requiereAprobacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoAusenciaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el tipo de ausencia", ex);
            }
        }
        public async Task<bool> DeleteTipoAusencia(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoAusencia";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ta_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el tipo de ausencia", ex);
            }
        }
        public async Task<List<DtoTipoAusencia>> GetTipoAusencia()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoAusencia";
                command.CommandType = CommandType.StoredProcedure;

                var tipoasuencias = new List<DtoTipoAusencia>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var tipoAusencia = new DtoTipoAusencia
                        {
                            Id = reader.GetInt32(0),
                            NombreAusencia = reader.IsDBNull(1) ? null : reader.GetString(1),
                            RequiereAprobacion = reader.GetBoolean(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        tipoasuencias.Add(tipoAusencia);
                    }
                    await reader.CloseAsync();
                    return tipoasuencias;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los tipos de ausencia", ex);
            }
        }
        public async Task<DtoTipoAusencia> GetTipoAusenciaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoAusenciaPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@ta_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var tipoausencias = new DtoTipoAusencia
                        {
                            Id = reader.GetInt32(0),
                            NombreAusencia = reader.IsDBNull(1) ? null : reader.GetString(1),
                            RequiereAprobacion = reader.GetBoolean(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        await connection.CloseAsync();
                        return tipoausencias;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el tipo de ausencia por ID", ex);
            }
        }
        public async Task<bool> TipoAusenciaExists(int id)
        {
            return await _context.TipoAusencia.AnyAsync(t => t.Id == id);
        }
    }
}