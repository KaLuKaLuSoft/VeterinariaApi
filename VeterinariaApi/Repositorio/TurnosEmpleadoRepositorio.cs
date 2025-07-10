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
    public class TurnosEmpleadoRepositorio : ITurnosEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TurnosEmpleadoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoTurnosEmpleado> Create(DtoTurnosEmpleado turnosEmpleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTurnoEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@t_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var sucursalIdParam = new MySqlParameter("@t_SucursalId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.SucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                var turnoIdParam = new MySqlParameter("@t_TurnoId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.TurnoId
                };
                command.Parameters.Add(turnoIdParam);

                var fechaParam = new MySqlParameter("@t_Fecha", MySqlDbType.DateTime)
                {
                    Value = turnosEmpleadoDto.Fecha
                };
                command.Parameters.Add(fechaParam);

                var confirmadoParam = new MySqlParameter("@t_Confirmado", MySqlDbType.Bit)
                {
                    Value = turnosEmpleadoDto.Confirmado
                };
                command.Parameters.Add(confirmadoParam);

                var observacionesParam = new MySqlParameter("@t_Observaciones", MySqlDbType.VarChar, 250)
                {
                    Value = turnosEmpleadoDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return turnosEmpleadoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el turno del empleado", ex);
            }
        }
        public async Task<DtoTurnosEmpleado> Update(DtoTurnosEmpleado turnosEmpleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTurnoEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.Id
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@t_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var sucursalIdParam = new MySqlParameter("@t_SucursalId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.SucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                var turnoIdParam = new MySqlParameter("@t_TurnoId", MySqlDbType.Int32)
                {
                    Value = turnosEmpleadoDto.TurnoId
                };
                command.Parameters.Add(turnoIdParam);

                var fechaParam = new MySqlParameter("@t_Fecha", MySqlDbType.DateTime)
                {
                    Value = turnosEmpleadoDto.Fecha
                };
                command.Parameters.Add(fechaParam);

                var confirmadoParam = new MySqlParameter("@t_Confirmado", MySqlDbType.Bit)
                {
                    Value = turnosEmpleadoDto.Confirmado
                };
                command.Parameters.Add(confirmadoParam);

                var observacionesParam = new MySqlParameter("@t_Observaciones", MySqlDbType.VarChar, 500)
                {
                    Value = turnosEmpleadoDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return turnosEmpleadoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el turno del empleado", ex);
            }
        }
        public async Task<bool> DeleteTurnosEmpleado(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTurnoEmpleado";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el turno del empleado", ex);
            }
        }
        public async Task<List<DtoTurnosEmpleado>> GetTurnosEmpleado()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTurnosEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var turnoempleados = new List<DtoTurnosEmpleado>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var turnoempleado = new DtoTurnosEmpleado
                        {
                            Id = reader.GetInt32(0),
                            EmpleadoId = reader.GetInt32(1),
                            NombreEmpleado = reader.IsDBNull(2) ? null : reader.GetString(2),
                            SucursalId = reader.GetInt32(3),
                            NombreSucursal = reader.IsDBNull(4) ? null : reader.GetString(4),
                            TurnoId = reader.GetInt32(5),
                            NombreTurno = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Fecha = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Confirmado = reader.GetBoolean(8),
                            Observaciones = reader.IsDBNull(9) ? null : reader.GetString(9),
                            Fecha_Alta = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            Fecha_Modificacion = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11)
                        };
                        turnoempleados.Add(turnoempleado);
                    }
                    await reader.CloseAsync();
                    return turnoempleados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los turnos de empleados", ex);
            }
        }
        public async Task<DtoTurnosEmpleado> GetTurnosEmpleadoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTurnoEmpleadoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var turnoempleado = new DtoTurnosEmpleado
                        {
                            Id = reader.GetInt32(0),
                            EmpleadoId = reader.GetInt32(1),
                            NombreEmpleado = reader.IsDBNull(2) ? null : reader.GetString(2),
                            SucursalId = reader.GetInt32(3),
                            NombreSucursal = reader.IsDBNull(4) ? null : reader.GetString(4),
                            TurnoId = reader.GetInt32(5),
                            NombreTurno = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Fecha = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Confirmado = reader.GetBoolean(8),
                            Observaciones = reader.IsDBNull(9) ? null : reader.GetString(9),
                            Fecha_Alta = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            Fecha_Modificacion = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11)
                        };
                        await connection.CloseAsync();
                        return turnoempleado;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el turno del empleado por ID", ex);
            }
        }
        public async Task<bool> TurnosEmpleadoExists(int id)
        {
            return await _context.TurnosEmpleado.AnyAsync(te => te.Id == id);
        }
    }
}
