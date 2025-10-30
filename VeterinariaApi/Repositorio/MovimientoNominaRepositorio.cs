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
    public class MovimientoNominaRepositorio : IMovimientoNominaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovimientoNominaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoMovimientosNomina> Create(DtoMovimientosNomina movimientoNominaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarMovimientoNomina";

                var idParam = new MySqlParameter("@mn_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@mn_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var conceptonominaIdParam = new MySqlParameter("@mn_ConceptoNominaId", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.ConceptoNominaId
                };
                command.Parameters.Add(conceptonominaIdParam);

                var fechaMovimientoParam = new MySqlParameter("@mn_FechaMovimiento", MySqlDbType.DateTime)
                {
                    Value = movimientoNominaDto.FechaMovimiento
                };
                command.Parameters.Add(fechaMovimientoParam);

                var montoParam = new MySqlParameter("@mn.Monto", MySqlDbType.Decimal)
                {
                    Value = movimientoNominaDto.Monto
                };
                command.Parameters.Add(montoParam);

                var periodonominaParam = new MySqlParameter("@mn_PeriodoNomina", MySqlDbType.VarChar, 150)
                {
                    Value = movimientoNominaDto.PeriodoNomina ?? (object)DBNull.Value
                };
                command.Parameters.Add(periodonominaParam);

                var registroporempleadoParam = new MySqlParameter("@mn_RegistroPorEmpleado", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.RegistradorPorEmpleado
                };
                command.Parameters.Add(registroporempleadoParam);

                var observacionesParam = new MySqlParameter("mn_Observaciones", MySqlDbType.VarChar, 255)
                {
                    Value = movimientoNominaDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return movimientoNominaDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear Movimiento Nómina", ex);
            }
        }
        public async Task<DtoMovimientosNomina> Update(DtoMovimientosNomina movimientoNominaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarMovimientoNomina";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@mn_Id", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.Id > 0 ? (object)movimientoNominaDto.Id : DBNull.Value
                };
                command.Parameters.Add(idParam);

                var empleadoIdParam = new MySqlParameter("@mn_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var conceptonominaIdParam = new MySqlParameter("@mn_ConceptoNominaId", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.ConceptoNominaId
                };
                command.Parameters.Add(conceptonominaIdParam);

                var fechaMovimientoParam = new MySqlParameter("@mn_FechaMovimiento", MySqlDbType.DateTime)
                {
                    Value = movimientoNominaDto.FechaMovimiento
                };
                command.Parameters.Add(fechaMovimientoParam);

                var montoParam = new MySqlParameter("@mn.Monto", MySqlDbType.Decimal)
                {
                    Value = movimientoNominaDto.Monto
                };
                command.Parameters.Add(montoParam);

                var periodonominaParam = new MySqlParameter("@mn_PeriodoNomina", MySqlDbType.VarChar, 150)
                {
                    Value = movimientoNominaDto.PeriodoNomina ?? (object)DBNull.Value
                };
                command.Parameters.Add(periodonominaParam);

                var registroporempleadoParam = new MySqlParameter("@mn_RegistroPorEmpleado", MySqlDbType.Int32)
                {
                    Value = movimientoNominaDto.RegistradorPorEmpleado
                };
                command.Parameters.Add(registroporempleadoParam);

                var observacionesParam = new MySqlParameter("mn_Observaciones", MySqlDbType.VarChar, 255)
                {
                    Value = movimientoNominaDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return movimientoNominaDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar Movimiento Nómina", ex);
            }
        }
        public async Task<bool> DeleteMovimientoNomina(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarMovimientoNomina";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("mn_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultadoParam = new MySqlParameter("resultado", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar Movimiento Nómina", ex);
            }
        }

        public async Task<List<DtoMovimientosNomina>> GetMovimientoNomina()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerMovimientosNomina";
                command.CommandType = CommandType.StoredProcedure;

                var movimientosNomina = new List<DtoMovimientosNomina>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var movimientoNomina = new DtoMovimientosNomina
                        {
                            Id = reader.GetInt32("Id"),
                            EmpleadoId = reader.GetInt32("EmpleadoId"),
                            Empleado = reader.IsDBNull(reader.GetOrdinal("Empleado")) ? null : reader.GetString("Empleado"),
                            ConceptoNominaId = reader.GetInt32("ConceptoNominaId"),
                            FechaMovimiento = reader.IsDBNull(reader.GetOrdinal("FechaMovimiento")) ? null : reader.GetDateTime("FechaMovimiento"),
                            Monto = reader.IsDBNull(reader.GetOrdinal("Monto")) ? null : reader.GetDecimal("Monto"),
                            PeriodoNomina = reader.IsDBNull(reader.GetOrdinal("PeriodoNomina")) ? null : reader.GetString("PeriodoNomina"),
                            RegistradorPorEmpleado = reader.GetInt32("RegistroPorEmpleado"),
                            RegistradorPorEmpleados = reader.IsDBNull(reader.GetOrdinal("RegistradorPorEmpleadoNombre")) ? null : reader.GetString("RegistradorPorEmpleadoNombre"),
                            Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString("Observaciones"),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        movimientosNomina.Add(movimientoNomina);
                    }
                }
                await connection.CloseAsync();
                return movimientosNomina;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los movimientos de nómina", ex);
            }
        }

        public async Task<DtoMovimientosNomina> GetMovimientoNominaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerMovimientoNominaPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("mn_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var movimientoNomina = new DtoMovimientosNomina
                    {
                        Id = reader.GetInt32("mn_Id"),
                        EmpleadoId = reader.GetInt32("mn_EmpleadoId"),
                        Empleado = reader.IsDBNull(reader.GetOrdinal("Empleado")) ? null : reader.GetString("Empleado"),
                        ConceptoNominaId = reader.GetInt32("mn_ConceptoNominaId"),
                        FechaMovimiento = reader.IsDBNull(reader.GetOrdinal("mn_FechaMovimiento")) ? null : reader.GetDateTime("mn_FechaMovimiento"),
                        Monto = reader.IsDBNull(reader.GetOrdinal("mn_Monto")) ? null : reader.GetDecimal("mn_Monto"),
                        PeriodoNomina = reader.IsDBNull(reader.GetOrdinal("mn_PeriodoNomina")) ? null : reader.GetString("mn_PeriodoNomina"),
                        RegistradorPorEmpleado = reader.GetInt32("mn_RegistroPorEmpleado"),
                        RegistradorPorEmpleados = reader.IsDBNull(reader.GetOrdinal("RegistradorPorEmpleadoNombre")) ? null : reader.GetString("RegistradorPorEmpleadoNombre"),
                        Observaciones = reader.IsDBNull(reader.GetOrdinal("mn_Observaciones")) ? null : reader.GetString("mn_Observaciones"),
                        Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("mn_Fecha_Alta")) ? null : reader.GetDateTime("mn_Fecha_Alta"),
                        Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("mn_Fecha_Modificacion")) ? null : reader.GetDateTime("mn_Fecha_Modificacion")
                    };
                    await connection.CloseAsync();
                    return movimientoNomina;
                }
                 await connection.CloseAsync();
                 return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el movimiento de nómina por ID", ex);
            }
        }

        public async Task<bool> MovimientoNominaExists(int id)
        {
            return await _context.MovimientosNomina.AnyAsync(e => e.Id == id);
        }
    }
}
