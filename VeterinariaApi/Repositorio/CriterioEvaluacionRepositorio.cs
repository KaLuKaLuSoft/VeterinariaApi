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
    public class CriterioEvaluacionRepositorio : ICriterioEvaluacionRepositorio
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        public CriterioEvaluacionRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoCriterioEvaluacion> Create(DtoCriterioEvaluacion criterioEvaluacionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCriterioEvaluacion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ce_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCriterioParam = new MySqlParameter("@ce_NombreCriterio", MySqlDbType.VarChar, 100)
                {
                    Value = criterioEvaluacionDto.NombreCriterio ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCriterioParam);

                var descripcionParam = new MySqlParameter("@ce_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = criterioEvaluacionDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                var tipoCriterioParam = new MySqlParameter("@ce_TipoCriterio", MySqlDbType.VarChar, 50)
                {
                    Value = criterioEvaluacionDto.TipoCriterio ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipoCriterioParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return criterioEvaluacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el criterio de evaluación", ex);
            }
        }
        public async Task<DtoCriterioEvaluacion> Update(DtoCriterioEvaluacion criterioEvaluacionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCriterioEvaluacion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ce_Id", MySqlDbType.Int32)
                {
                    Value = criterioEvaluacionDto.Id > 0 ? (object)criterioEvaluacionDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCriterioParam = new MySqlParameter("@ce_NombreCriterio", MySqlDbType.VarChar, 100)
                {
                    Value = criterioEvaluacionDto.NombreCriterio ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCriterioParam);

                var descripcionParam = new MySqlParameter("@ce_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = criterioEvaluacionDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                var tipoCriterioParam = new MySqlParameter("@ce_TipoCriterio", MySqlDbType.VarChar, 50)
                {
                    Value = criterioEvaluacionDto.TipoCriterio ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipoCriterioParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return criterioEvaluacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar CriterioEvaluacion. ", ex);
            }
        }
        public async Task<bool> DeleteCriterioEvaluacion(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarCriterioEvaluacion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ce_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar CriterioEvaluacion. ", ex);
            }
        }
        public async Task<List<DtoCriterioEvaluacion>> GetCriterioEvaluacion()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCriterioEvaluacion";
                command.CommandType = CommandType.StoredProcedure;

                var criterioevaluaciones = new List<DtoCriterioEvaluacion>();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var criterioevaluacion = new DtoCriterioEvaluacion
                        {
                            Id = reader.GetInt32(0),
                            NombreCriterio = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            TipoCriterio = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        criterioevaluaciones.Add(criterioevaluacion);
                    }
                    await reader.CloseAsync();
                    return criterioevaluaciones;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener CriterioEvaluacion. ", ex);
            }
        }
        public async Task<DtoCriterioEvaluacion> GetCriterioEvaluacionById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCriterioEvaluacionPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ce_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var criterioevaluaciones = new DtoCriterioEvaluacion
                        {
                            Id = reader.GetInt32(0),
                            NombreCriterio = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            TipoCriterio = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        await connection.CloseAsync();
                        return criterioevaluaciones;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener CriterioEvaluacion. ", ex);
            }
        }
        public async Task<bool> CriterioEvaluacionExists(int id)
        {
            return await _context.CriterioEvaluacion.AnyAsync(a => a.Id == id);
        }
    }
}
