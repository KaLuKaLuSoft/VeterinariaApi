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
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmpleadoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoEmpleado> Create(DtoEmpleado empleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var codEmpleadoParam = new MySqlParameter("@e_CodEmpleado", MySqlDbType.VarChar, 50)
                {
                    Value = empleadoDto.CodEmpleado ?? (object)DBNull.Value
                };
                command.Parameters.Add(codEmpleadoParam);

                var nombreParam = new MySqlParameter("@e_Nombre", MySqlDbType.VarChar, 100)
                {
                    Value = empleadoDto.Nombre ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreParam);

                var apellidoParam = new MySqlParameter("@e_Apellido", MySqlDbType.VarChar, 100)
                {
                    Value = empleadoDto.Apellido ?? (object)DBNull.Value
                };
                command.Parameters.Add(apellidoParam);

                var fechaNacimientoParam = new MySqlParameter("@e_FechaNacimiento", MySqlDbType.Date)
                {
                    Value = empleadoDto.FechaNacimiento ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaNacimientoParam);

                var telefonoParam = new MySqlParameter("@e_Telefono", MySqlDbType.VarChar, 15)
                {
                    Value = empleadoDto.Celular ?? (object)DBNull.Value
                };
                command.Parameters.Add(telefonoParam);

                var ciParam = new MySqlParameter("@e_Ci", MySqlDbType.VarChar, 20)
                {
                    Value = empleadoDto.Ci ?? (object)DBNull.Value
                };
                command.Parameters.Add(ciParam);

                var fechaContratacionParam = new MySqlParameter("@e_FechaContratacion", MySqlDbType.Date)
                {
                    Value = empleadoDto.FechaContratacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaContratacionParam);

                var salarioParam = new MySqlParameter("@e_Salario", MySqlDbType.Decimal)
                {
                    Value = empleadoDto.Salario ?? (object)DBNull.Value
                };
                command.Parameters.Add(salarioParam);

                var idDepartamentoParam = new MySqlParameter("@e_IdDepartamento", MySqlDbType.Int32)
                {
                    Value = empleadoDto.IdDepartamento
                };
                command.Parameters.Add(idDepartamentoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return empleadoDto;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el empleado", ex);
            }
        }
        public async Task<DtoEmpleado> Update(DtoEmpleado empleadoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = empleadoDto.Id > 0 ? (object)empleadoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var codEmpleadoParam = new MySqlParameter("@e_CodEmpleado", MySqlDbType.VarChar, 50)
                {
                    Value = empleadoDto.CodEmpleado ?? (object)DBNull.Value
                };
                command.Parameters.Add(codEmpleadoParam);

                var nombreParam = new MySqlParameter("@e_Nombre", MySqlDbType.VarChar, 100)
                {
                    Value = empleadoDto.Nombre ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreParam);

                var apellidoParam = new MySqlParameter("@e_Apellido", MySqlDbType.VarChar, 100)
                {
                    Value = empleadoDto.Apellido ?? (object)DBNull.Value
                };
                command.Parameters.Add(apellidoParam);

                var fechaNacimientoParam = new MySqlParameter("@e_FechaNacimiento", MySqlDbType.Date)
                {
                    Value = empleadoDto.FechaNacimiento ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaNacimientoParam);

                var telefonoParam = new MySqlParameter("@e_Telefono", MySqlDbType.VarChar, 15)
                {
                    Value = empleadoDto.Celular ?? (object)DBNull.Value
                };
                command.Parameters.Add(telefonoParam);

                var ciParam = new MySqlParameter("@e_Ci", MySqlDbType.VarChar, 20)
                {
                    Value = empleadoDto.Ci ?? (object)DBNull.Value
                };
                command.Parameters.Add(ciParam);

                var fechaContratacionParam = new MySqlParameter("@e_FechaContratacion", MySqlDbType.Date)
                {
                    Value = empleadoDto.FechaContratacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaContratacionParam);

                var salarioParam = new MySqlParameter("@e_Salario", MySqlDbType.Decimal)
                {
                    Value = empleadoDto.Salario ?? (object)DBNull.Value
                };
                command.Parameters.Add(salarioParam);

                var idDepartamentoParam = new MySqlParameter("@e_IdDepartamento", MySqlDbType.Int32)
                {
                    Value = empleadoDto.IdDepartamento
                };
                command.Parameters.Add(idDepartamentoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return empleadoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el empleado", ex);
            }
        }
        public async Task<bool> DeleteEmpleado(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarEmpleado";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el empleado", ex);
            }
        }
        public async Task<List<DtoEmpleado>> GetEmpleados()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleado";
                command.CommandType = CommandType.StoredProcedure;

                var empleados = new List<DtoEmpleado>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var empleado = new DtoEmpleado
                        {
                            Id = reader.GetInt32(0),
                            CodEmpleado = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Nombre = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Apellido = reader.IsDBNull(3) ? null : reader.GetString(3),
                            FechaNacimiento = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                            Celular = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Ci = reader.IsDBNull(6) ? null : reader.GetString(6),
                            FechaContratacion = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Salario = reader.IsDBNull(8) ? null : reader.GetDecimal(8),
                            IdDepartamento = reader.GetInt32(9),
                            NombreDepartamento = reader.IsDBNull(10) ? null : reader.GetString(10),
                            Activo = reader.IsDBNull(11) ? (bool?)null : reader.GetBoolean(11),
                            Fecha_Alta = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12),
                            Fecha_Modificacion = reader.IsDBNull(13) ? (DateTime?)null : reader.GetDateTime(13)
                        };
                        empleados.Add(empleado);
                    }
                    await reader.CloseAsync();
                    return empleados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los empleados", ex);
            }
        }
        public async Task<DtoEmpleado> GetEmpleadoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleadoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using(var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var empleados = new DtoEmpleado
                        {
                            Id = reader.GetInt32(0),
                            CodEmpleado = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Nombre = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Apellido = reader.IsDBNull(3) ? null : reader.GetString(3),
                            FechaNacimiento = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                            Celular = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Ci = reader.IsDBNull(6) ? null : reader.GetString(6),
                            FechaContratacion = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Salario = reader.IsDBNull(8) ? null : reader.GetDecimal(8),
                            IdDepartamento = reader.GetInt32(9),
                            NombreDepartamento = reader.IsDBNull(10) ? null : reader.GetString(10),
                            Fecha_Alta = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                            Fecha_Modificacion = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12)
                        };
                        await connection.CloseAsync();
                        return empleados;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el empleado.", ex);
            }
        }
        public async Task<bool> EmpleadoExists(int id)
        {
            return await _context.Empleados.AnyAsync(e => e.Id == id);
        }
    }
}
