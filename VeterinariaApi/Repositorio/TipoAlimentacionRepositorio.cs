using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class TipoAlimentacionRepositorio : ITipoAlimentacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TipoAlimentacionRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoTipoAlimentacion> Create(DtoTipoAlimentacion tipoAlimentacionDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoAlimentacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlConnector.MySqlParameter("@ta_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                var nombreTipoAlimentacionParam = new MySqlConnector.MySqlParameter("@ta_TipoAlimento", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = tipoAlimentacionDto.TipoAlimento ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreTipoAlimentacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoAlimentacionDto;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear el tipo de alimentación", ex);
            }
        }
        public async Task<DtoTipoAlimentacion> Update(DtoTipoAlimentacion tipoAlimentacionDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoAlimentacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlConnector.MySqlParameter("@ta_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = tipoAlimentacionDto.Id > 0 ? (object)tipoAlimentacionDto.Id : DBNull.Value
                };
                command.Parameters.Add(idParam);
                var nombreTipoAlimentacionParam = new MySqlConnector.MySqlParameter("@ta_NombreTipoAlimento", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = tipoAlimentacionDto.TipoAlimento ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreTipoAlimentacionParam);
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoAlimentacionDto;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar el tipo de alimentación", ex);
            }
        }
        public async Task<bool> DeleteTipoAlimentacion(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoAlimentacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@ta_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlConnector.MySqlParameter("@resultado", MySqlConnector.MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
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
                throw new Exception("Error al eliminar el tipo de alimentación", ex);
            }
        }
        public async Task<List<DtoTipoAlimentacion>> GetTipoAlimentacion()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoAlimentacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var tipoAlimentos = new List<DtoTipoAlimentacion>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var tipoAlimentacion = new DtoTipoAlimentacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            TipoAlimento = reader.IsDBNull(reader.GetOrdinal("TipoAlimento"))? null: reader.GetString(reader.GetOrdinal("TipoAlimento"))
                        };
                        tipoAlimentos.Add(tipoAlimentacion);
                    }
                    await reader.CloseAsync();
                    return tipoAlimentos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los tipos de alimentación", ex);
            }
        }
        public async Task<DtoTipoAlimentacion> GetTipoAlimentacionById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoAlimentacionPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlConnector.MySqlParameter("@ta_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var tipoAlimentacion = new DtoTipoAlimentacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            TipoAlimento = reader.IsDBNull(reader.GetOrdinal("TipoAlimento")) ? null : reader.GetString(reader.GetOrdinal("TipoAlimento"))
                        };
                        return tipoAlimentacion;
                    }
                    else
                    {
                        await connection.CloseAsync();
                        return null;
                    }
                }
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error al obtener el tipo de alimentación por ID", ex); 
            }
        }
        public async Task<bool> TipoAlimentacionExists(int id)
        {
            return await _context.TipoAlimentacion.AnyAsync(t => t.Id == id);
        }
    }
}
