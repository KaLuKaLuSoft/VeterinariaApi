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
    public class EmpleadoCapacitacionRepositorio : IEmpleadoCapacitacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmpleadoCapacitacionRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoEmpleadoCapacitacion> Create(DtoEmpleadoCapacitacion empleadoCapacitacionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleadoCapacitacion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ec_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@ec_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoCapacitacionDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var capacitacionIdParam = new MySqlParameter("@ec_CapacitacionId", MySqlDbType.Int32)
                {
                    Value = empleadoCapacitacionDto.CapacitacionId
                };
                command.Parameters.Add(capacitacionIdParam);

                var fechaCapacitacionParam = new MySqlParameter("@ec_FechaCapacitacion", MySqlDbType.DateTime)
                {
                    Value = empleadoCapacitacionDto.Fecha_Capacitacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaCapacitacionParam);

                var estadoAprobacionParam = new MySqlParameter("@ec_EstadoAprobacion", MySqlDbType.VarChar, 50)
                {
                    Value = empleadoCapacitacionDto.EstadoAprobacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoAprobacionParam);

                var calificacionParam = new MySqlParameter("@ec_Calificacion", MySqlDbType.Decimal)
                {
                    Value = empleadoCapacitacionDto.Calificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(calificacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return empleadoCapacitacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al insertar o actualizar la capacitación del empleado", ex);
            }
        }
        public async Task<DtoEmpleadoCapacitacion> Update(DtoEmpleadoCapacitacion empleadoCapacitacionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleadoCapacitacion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ec_Id", MySqlDbType.Int32)
                {
                    Value = empleadoCapacitacionDto.Id > 0 ? (object)empleadoCapacitacionDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@ec_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoCapacitacionDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var capacitacionIdParam = new MySqlParameter("@ec_CapacitacionId", MySqlDbType.Int32)
                {
                    Value = empleadoCapacitacionDto.CapacitacionId
                };
                command.Parameters.Add(capacitacionIdParam);

                var fechaCapacitacionParam = new MySqlParameter("@ec_Fecha_Capacitacion", MySqlDbType.DateTime)
                {
                    Value = empleadoCapacitacionDto.Fecha_Capacitacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaCapacitacionParam);

                var estadoAprobacionParam = new MySqlParameter("@ec_EstadoAprobacion", MySqlDbType.VarChar, 50)
                {
                    Value = empleadoCapacitacionDto.EstadoAprobacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoAprobacionParam);

                var calificacionParam = new MySqlParameter("@ec_Calificacion", MySqlDbType.Decimal)
                {
                    Value = empleadoCapacitacionDto.Calificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(calificacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return empleadoCapacitacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la capacitación del empleado", ex);
            }
        }
        public async Task<bool> DeleteEmpleadoCapacitacion(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarEmpleadoCapacitacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ec_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar la capacitación del empleado", ex);
            }
        }
        public async Task<List<DtoEmpleadoCapacitacion>> GetEmpleadoCapacitacion()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleadoCapacitacion";
                command.CommandType = CommandType.StoredProcedure;

                var empleadocapacitaciones = new List<DtoEmpleadoCapacitacion>();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var empleadoCapacitacion = new DtoEmpleadoCapacitacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                            Empleado = reader.IsDBNull(reader.GetOrdinal("Empleado")) ? null : reader.GetString(reader.GetOrdinal("Empleado")),
                            CapacitacionId = reader.GetInt32(reader.GetOrdinal("CapacitacionId")),
                            CursoCapacitacion = reader.IsDBNull(reader.GetOrdinal("CursoCapacitacion")) ? null : reader.GetString(reader.GetOrdinal("CursoCapacitacion")),
                            Fecha_Capacitacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Capacitacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Capacitacion")),
                            EstadoAprobacion = reader.IsDBNull(reader.GetOrdinal("EstadoAprobacion")) ? null : reader.GetString(reader.GetOrdinal("EstadoAprobacion")),
                            Calificacion = reader.IsDBNull(reader.GetOrdinal("Calificacion")) ? null : reader.GetDecimal(reader.GetOrdinal("Calificacion")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        empleadocapacitaciones.Add(empleadoCapacitacion);
                    }
                    await reader.CloseAsync();
                    return empleadocapacitaciones;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las capacitaciones de los empleados", ex);
            }
        }
        public async Task<DtoEmpleadoCapacitacion> GetEmpleadoCapacitacionById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleadoCapacitacionPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ec_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var empleadocapacitacion = new DtoEmpleadoCapacitacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmpleadoId = reader.GetInt32(reader.GetOrdinal("EmpleadoId")),
                            CapacitacionId = reader.GetInt32(reader.GetOrdinal("CapacitacionId")),
                            Fecha_Capacitacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Capacitacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Capacitacion")),
                            EstadoAprobacion = reader.IsDBNull(reader.GetOrdinal("EstadoAprobacion")) ? null : reader.GetString(reader.GetOrdinal("EstadoAprobacion")),
                            Calificacion = reader.IsDBNull(reader.GetOrdinal("Calificacion")) ? null : reader.GetDecimal(reader.GetOrdinal("Calificacion")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        await connection.CloseAsync();
                        return empleadocapacitacion;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la capacitación del empleado por ID", ex);
            }
        }
        public async Task<bool> EmpleadoCapacitacionExists(int id)
        {
            return await _context.EmpleadoCapacitacion.AnyAsync(ec => ec.Id == id);
        }
    }
}
