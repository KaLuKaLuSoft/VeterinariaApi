using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Transactions;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class AusenciaEmpleadoRepositorio : IAusenciaEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AusenciaEmpleadoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoAusenciaEmpleado> Create(DtoAusenciaEmpleado ausenciaEmpleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarAusenciaEmpleado";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@ae_Id",MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlConnector.MySqlParameter("@ae_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var tipoAusenciaIdParam = new MySqlConnector.MySqlParameter("@ae_TipoAusenciaId", MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.TipoAusenciaId
                };
                command.Parameters.Add(tipoAusenciaIdParam);

                var estadoParam = new MySqlConnector.MySqlParameter("@ae_Estado", MySqlDbType.VarChar, 50)
                {
                    Value = ausenciaEmpleadoDto.Estado ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoParam);

                var fechaInicioParam = new MySqlConnector.MySqlParameter("@ae_FechaInicio", MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaInicio
                };
                command.Parameters.Add(fechaInicioParam);

                var fechaFinParam = new MySqlConnector.MySqlParameter("@ae_FechaFin", MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaFin
                };
                command.Parameters.Add(fechaFinParam);

                var motivoParam = new MySqlConnector.MySqlParameter("@ae_Motivo", MySqlDbType.VarChar, 255)
                {
                    Value = ausenciaEmpleadoDto.Motivo ?? (object)DBNull.Value
                };
                command.Parameters.Add(motivoParam);

                var aprobadoPorEmpleadoParam = new MySqlConnector.MySqlParameter("@ae_AprobadoPorEmpleado", MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.AprobadoPorEmpleado
                };
                command.Parameters.Add(aprobadoPorEmpleadoParam);

                var fechaSolicitudParam = new MySqlConnector.MySqlParameter("@ae_FechaSolicitud", MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaSolicitud
                };
                command.Parameters.Add(fechaSolicitudParam);

                var fechaAprobacionParam = new MySqlConnector.MySqlParameter("@ae_FechaAprobacion", MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaAprobacion
                };
                command.Parameters.Add(fechaAprobacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return ausenciaEmpleadoDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear la ausencia del empleado", ex);
            }
        }
        public async Task<DtoAusenciaEmpleado> Update(DtoAusenciaEmpleado ausenciaEmpleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarAusenciaEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@ae_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.Id > 0 ? (object)ausenciaEmpleadoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlConnector.MySqlParameter("@ae_EmpleadoId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var tipoAusenciaIdParam = new MySqlConnector.MySqlParameter("@ae_TipoAusenciaId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.TipoAusenciaId
                };
                command.Parameters.Add(tipoAusenciaIdParam);

                var estadoParam = new MySqlConnector.MySqlParameter("@ae_Estado", MySqlConnector.MySqlDbType.VarChar, 50)
                {
                    Value = ausenciaEmpleadoDto.Estado ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoParam);

                var fechaInicioParam = new MySqlConnector.MySqlParameter("@ae_FechaInicio", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaInicio
                };
                command.Parameters.Add(fechaInicioParam);

                var fechaFinParam = new MySqlConnector.MySqlParameter("@ae_FechaFin", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaFin
                };
                command.Parameters.Add(fechaFinParam);

                var motivoParam = new MySqlConnector.MySqlParameter("@ae_Motivo", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = ausenciaEmpleadoDto.Motivo ?? (object)DBNull.Value
                };
                command.Parameters.Add(motivoParam);

                var aprobadoPorEmpleadoParam = new MySqlConnector.MySqlParameter("@ae_AprobadoPorEmpleado", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = ausenciaEmpleadoDto.AprobadoPorEmpleado
                };
                command.Parameters.Add(aprobadoPorEmpleadoParam);

                var fechaSolicitudParam = new MySqlConnector.MySqlParameter("@ae_FechaSolicitud", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaSolicitud
                };
                command.Parameters.Add(fechaSolicitudParam);

                var fechaAprobacionParam = new MySqlConnector.MySqlParameter("@ae_FechaAprobacion", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = ausenciaEmpleadoDto.FechaAprobacion
                };
                command.Parameters.Add(fechaAprobacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return ausenciaEmpleadoDto;
            }
            catch
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar la ausencia del empleado");
            }
        }
        public async Task<bool> DeleteAusenciaEmpleado(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = " ";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@ae_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultadoParam = new MySqlConnector.MySqlParameter("@resultado", MySqlConnector.MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(idParam);
                command.Parameters.Add(resultadoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultadoParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al eliminar la ausencia del empleado", ex);
            }
        }
        public async Task<List<DtoAusenciaEmpleado>> GetAusenciaEmpleado()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerAusenciaEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var ausenciaempleados = new List<DtoAusenciaEmpleado>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var ausenciaEmpleado = new DtoAusenciaEmpleado
                        {
                            Id = reader.GetInt32("Id"),
                            EmpleadoId = reader.GetInt32("EmpleadoId"),
                            Empleado = reader.IsDBNull("Nombre") ? null : reader.GetString("Nombre"),
                            TipoAusenciaId = reader.GetInt32("TipoAusenciaId"),
                            TipoAusencia = reader.IsDBNull("NombreAusencia") ? null : reader.GetString("NombreAusencia"),
                            Estado = reader.IsDBNull("Estado") ? null : reader.GetString("Estado"),
                            FechaInicio = reader.IsDBNull("FechaInicio") ? (DateTime?)null : reader.GetDateTime("FechaInicio"),
                            FechaFin = reader.IsDBNull("FechaFin") ? (DateTime?)null : reader.GetDateTime("FechaFin"),
                            Motivo = reader.IsDBNull("Motivo") ? null : reader.GetString("Motivo"),
                            AprobadoPorEmpleado = reader.GetInt32("AprobadoPorEmpleado"),
                            AprobadoPorEmpleados = reader.IsDBNull("AprobadoPorEmpleados") ? null : reader.GetString("AprobadoPorEmpleados"),
                            FechaSolicitud = reader.IsDBNull("FechaSolicitud") ? (DateTime?)null : reader.GetDateTime("FechaSolicitud"),
                            FechaAprobacion = reader.IsDBNull("FechaAprobacion") ? (DateTime?)null : reader.GetDateTime("FechaAprobacion"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        ausenciaempleados.Add(ausenciaEmpleado);
                    }
                }
                await connection.CloseAsync();
                return ausenciaempleados;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ausencias de los empleados", ex);
            }
        }
        public async Task<DtoAusenciaEmpleado> GetAusenciaEmpleadoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerAusenciaEmpleadoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@e_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var ausenciaEmpleadoDto = new DtoAusenciaEmpleado
                    {
                        Id = reader.GetInt32("Id"),
                        EmpleadoId = reader.GetInt32("EmpleadoId"),
                        Empleado = reader.IsDBNull("Nombre") ? null : reader.GetString("Nombre"),
                        TipoAusenciaId = reader.GetInt32("TipoAusenciaId"),
                        TipoAusencia = reader.IsDBNull("NombreAusencia") ? null : reader.GetString("NombreAusencia"),
                        Estado = reader.IsDBNull("Estado") ? null : reader.GetString("Estado"),
                        FechaInicio = reader.IsDBNull("FechaInicio") ? (DateTime?)null : reader.GetDateTime("FechaInicio"),
                        FechaFin = reader.IsDBNull("FechaFin") ? (DateTime?)null : reader.GetDateTime("FechaFin"),
                        Motivo = reader.IsDBNull("Motivo") ? null : reader.GetString("Motivo"),
                        AprobadoPorEmpleado = reader.GetInt32("AprobadoPorEmpleado"),
                        AprobadoPorEmpleados = reader.IsDBNull("AprobadoPorEmpleados") ? null : reader.GetString("AprobadoPorEmpleados"),
                        FechaSolicitud = reader.IsDBNull("FechaSolicitud") ? (DateTime?)null : reader.GetDateTime("FechaSolicitud"),
                        FechaAprobacion = reader.IsDBNull("FechaAprobacion") ? (DateTime?)null : reader.GetDateTime("FechaAprobacion"),
                        Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                        Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                    };
                    await connection.CloseAsync();
                    return ausenciaEmpleadoDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la ausencia del empleado por ID", ex);
            }
        }
        public async Task<bool> AusenciaEmpleadoExists(int id)
        {
            return await _context.AusenciaEmpleado.AnyAsync(a => a.Id == id);
        }
    }
}
