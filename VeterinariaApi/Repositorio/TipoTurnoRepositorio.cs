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
    public class TipoTurnoRepositorio : ITipoTurnoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoTurnoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoTipoTurno> Create(DtoTipoTurno tipoturnoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoTurno";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreTurnoParam = new MySqlParameter("@t_NombreTurno", MySqlDbType.VarChar, 100)
                {
                    Value = tipoturnoDto.NombreTurno ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreTurnoParam);

                var horaInicioParam = new MySqlParameter("@t_HoraInicio", MySqlDbType.Time)
                {
                    Value = tipoturnoDto.HoraInicio ?? (object)DBNull.Value
                };
                command.Parameters.Add(horaInicioParam);

                var horaFinParam = new MySqlParameter("@t_HoraFin", MySqlDbType.Time)
                {
                    Value = tipoturnoDto.HoraFin ?? (object)DBNull.Value
                };
                command.Parameters.Add(horaFinParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoturnoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el tipo de turno", ex);
            }
        }
        public async Task<DtoTipoTurno> Update(DtoTipoTurno tipoturnoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoTurno";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = tipoturnoDto.Id > 0 ? (object)tipoturnoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreTurnoParam = new MySqlParameter("@t_NombreTurno", MySqlDbType.VarChar, 100)
                {
                    Value = tipoturnoDto.NombreTurno ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreTurnoParam);

                var horaInicioParam = new MySqlParameter("@t_HoraInicio", MySqlDbType.Time)
                {
                    Value = tipoturnoDto.HoraInicio ?? (object)DBNull.Value
                };
                command.Parameters.Add(horaInicioParam);

                var horaFinParam = new MySqlParameter("@t_HoraFin", MySqlDbType.Time)
                {
                    Value = tipoturnoDto.HoraFin ?? (object)DBNull.Value
                };
                command.Parameters.Add(horaFinParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoturnoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el tipo de turno", ex);
            }
        }
        public async Task<bool> DeleteTipoTurno(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoTurno";
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
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el tipo de turno", ex);
            }
        }
        public async Task<List<DtoTipoTurno>> GetTipoTurno()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoTurno";
                command.CommandType = CommandType.StoredProcedure;

                var tipoTurnos = new List<DtoTipoTurno>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var tipoTurno = new DtoTipoTurno
                        {
                            Id = reader.GetInt32(0),
                            NombreTurno = reader.IsDBNull(1) ? null : reader.GetString(1),
                            HoraInicio = reader.IsDBNull(2) ? (TimeSpan?)null : (TimeSpan)reader.GetValue(2),
                            HoraFin = reader.IsDBNull(3) ? (TimeSpan?)null : (TimeSpan)reader.GetValue(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        tipoTurnos.Add(tipoTurno);
                    }
                    await reader.CloseAsync();
                    return tipoTurnos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los tipos de turno", ex);
            }
        }
        public async Task<DtoTipoTurno> GetTipoTurnoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoTurnoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idaram = new MySqlParameter("@t_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idaram);
                
                using(var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var tipoTurno = new DtoTipoTurno
                        {
                            Id = reader.GetInt32(0),
                            NombreTurno = reader.IsDBNull(1) ? null : reader.GetString(1),
                            HoraInicio = reader.IsDBNull(2) ? (TimeSpan?)null : (TimeSpan)reader.GetValue(2),
                            HoraFin = reader.IsDBNull(3) ? (TimeSpan?)null : (TimeSpan)reader.GetValue(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        await connection.CloseAsync();
                        return tipoTurno;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el tipo de turno por ID", ex);
            }
        }
        public async Task<bool> TipoTurnoExists(int id)
        {
            return await _context.TipoTurno.AnyAsync(t => t.Id == id);
        }
    }
}
