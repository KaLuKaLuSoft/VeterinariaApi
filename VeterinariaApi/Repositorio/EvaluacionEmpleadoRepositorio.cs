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
    public class EvaluacionEmpleadoRepositorio : IEvaluacionEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EvaluacionEmpleadoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoEvaluacionEmpleado> Create(DtoEvaluacionEmpleado evaluacionEmpleadoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEvaluacionEmpleado";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ee_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@ee_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var criterioEvaluacionIdParam = new MySqlParameter("@ee_CriterioEvaluacionId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.CriterioEvaluacionId
                };
                command.Parameters.Add(criterioEvaluacionIdParam);

                var fechaEvaluacionParam = new MySqlParameter("@ee_Fecha_Evaluacion", MySqlDbType.DateTime)
                {
                    Value = evaluacionEmpleadoDto.Fecha_Evaluacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaEvaluacionParam);

                var calificacionParam = new MySqlParameter("@ee_Calificacion", MySqlDbType.Decimal)
                {
                    Value = evaluacionEmpleadoDto.Calificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(calificacionParam);

                var comentariosParam = new MySqlParameter("@ee_Comentarios", MySqlDbType.VarChar, 500)
                {
                    Value = evaluacionEmpleadoDto.Comentarios ?? (object)DBNull.Value
                };
                command.Parameters.Add(comentariosParam);

                var evaluadoPorEmpleadoIdParam = new MySqlParameter("@ee_EvaluadoPorEmpleadoId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.EvaluadoPorEmpleadoId
                };
                command.Parameters.Add(evaluadoPorEmpleadoIdParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return evaluacionEmpleadoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la evaluación del empleado", ex);
            }
        }
        public async Task<DtoEvaluacionEmpleado> Update(DtoEvaluacionEmpleado evaluacionEmpleadoDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEvaluacionEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ee_Id", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.Id
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@ee_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var criterioEvaluacionIdParam = new MySqlParameter("@ee_CriterioEvaluacionId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.CriterioEvaluacionId
                };
                command.Parameters.Add(criterioEvaluacionIdParam);

                var fechaEvaluacionParam = new MySqlParameter("@ee_Fecha_Evaluacion", MySqlDbType.DateTime)
                {
                    Value = evaluacionEmpleadoDto.Fecha_Evaluacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaEvaluacionParam);

                var calificacionParam = new MySqlParameter("@ee_Calificacion", MySqlDbType.Decimal)
                {
                    Value = evaluacionEmpleadoDto.Calificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(calificacionParam);

                var comentariosParam = new MySqlParameter("@ee_Comentarios", MySqlDbType.VarChar, 500)
                {
                    Value = evaluacionEmpleadoDto.Comentarios ?? (object)DBNull.Value
                };
                command.Parameters.Add(comentariosParam);

                var evaluadoPorEmpleadoIdParam = new MySqlParameter("@ee_EvaluadoPorEmpleadoId", MySqlDbType.Int32)
                {
                    Value = evaluacionEmpleadoDto.EvaluadoPorEmpleadoId
                };
                command.Parameters.Add(evaluadoPorEmpleadoIdParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return evaluacionEmpleadoDto;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la evaluación del empleado", ex);
            }
        }
        public async Task<bool> DeleteEvaluacionEmpleado(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarEvaluacionEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ee_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliinar Evaluacion Empleado. ");
            }
        }
        public async Task<List<DtoEvaluacionEmpleado>> GetEvaluacionEmpleado()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEvaluacionEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var evaluacionempleados = new List<DtoEvaluacionEmpleado>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var evaluacionEmpleado = new DtoEvaluacionEmpleado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                            Empleado = reader.IsDBNull(reader.GetOrdinal("Empleado")) ? null : reader.GetString(reader.GetOrdinal("Empleado")),
                            CriterioEvaluacionId = reader.GetInt32(reader.GetOrdinal("CriterioEvaluacionId")),
                            CriterioEvaluacion = reader.IsDBNull(reader.GetOrdinal("CriterioEvaluacion")) ? null : reader.GetString(reader.GetOrdinal("CriterioEvaluacion")),
                            Fecha_Evaluacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Evaluacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Evaluacion")),
                            Calificacion = reader.IsDBNull(reader.GetOrdinal("Calificacion")) ? null : reader.GetDecimal(reader.GetOrdinal("Calificacion")),
                            Comentarios = reader.IsDBNull(reader.GetOrdinal("Comentarios")) ? null : reader.GetString(reader.GetOrdinal("Comentarios")),
                            EvaluadoPorEmpleadoId = reader.GetInt32(reader.GetOrdinal("EvaluadoPorEmpleadoId")),
                            EmpleadoEvaluador = reader.IsDBNull(reader.GetOrdinal("EmpleadoEvaluador")) ? null : reader.GetString(reader.GetOrdinal("EmpleadoEvaluador")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        evaluacionempleados.Add(evaluacionEmpleado);
                    }
                    await reader.CloseAsync();
                    return evaluacionempleados;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener las evaluaciones de empleados", ex);
            }
        }
        public async Task<DtoEvaluacionEmpleado> GetEvaluacionEmpleadoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEvaluacionEmpleadoPorId";
                command.CommandType = CommandType.StoredProcedure;
                
                var idParam = new MySqlParameter("@ee_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var evaluacionEmpleados = new DtoEvaluacionEmpleado
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                            Empleado = reader.IsDBNull(reader.GetOrdinal("Empleado")) ? null : reader.GetString(reader.GetOrdinal("Empleado")),
                            CriterioEvaluacionId = reader.GetInt32(reader.GetOrdinal("CriterioEvaluacionId")),
                            CriterioEvaluacion = reader.IsDBNull(reader.GetOrdinal("CriterioEvaluacion")) ? null : reader.GetString(reader.GetOrdinal("CriterioEvaluacion")),
                            Fecha_Evaluacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Evaluacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Evaluacion")),
                            Calificacion = reader.IsDBNull(reader.GetOrdinal("Calificacion")) ? null : reader.GetDecimal(reader.GetOrdinal("Calificacion")),
                            Comentarios = reader.IsDBNull(reader.GetOrdinal("Comentarios")) ? null : reader.GetString(reader.GetOrdinal("Comentarios")),
                            EvaluadoPorEmpleadoId = reader.GetInt32(reader.GetOrdinal("EvaluadoPorEmpleadoId")),
                            EmpleadoEvaluador = reader.IsDBNull(reader.GetOrdinal("EmpleadoEvaluador")) ? null : reader.GetString(reader.GetOrdinal("EmpleadoEvaluador")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        await connection.CloseAsync();
                        return evaluacionEmpleados;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la evaluación del empleado por ID", ex);
            }
        }
        public async Task<bool> EvaluacionEmpleadoExists(int id)
        {
            return await _context.EvaluacionEmpleado.AnyAsync(e => e.Id == id);
        }
    }
}
