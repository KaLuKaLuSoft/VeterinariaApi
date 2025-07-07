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
    public class EmpleadoEspecialidadRepositorio : IEmpleadoEspecialidadRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmpleadoEspecialidadRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoEmpleadoEspecialidad> Create(DtoEmpleadoEspecialidad empleadoespecialidadDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleadoEspecialidad";
                command.CommandType = CommandType.StoredProcedure;

                var empleadoIdParam = new MySqlParameter("@e_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoespecialidadDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var especialidadIdParam = new MySqlParameter("@e_EspecialidadId", MySqlDbType.Int32)
                {
                    Value = empleadoespecialidadDto.EspecialidadId
                };
                command.Parameters.Add(especialidadIdParam);

                var fechaCertificacionParam = new MySqlParameter("@e_FechaCertificacion", MySqlDbType.DateTime)
                {
                    Value = empleadoespecialidadDto.FechaCertificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaCertificacionParam);
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return empleadoespecialidadDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear EmpleadoEspecialidad", ex);
            }
        }
        public async Task<DtoEmpleadoEspecialidad> Update(DtoEmpleadoEspecialidad empleadoespecialidadDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpleadoEspecialidad";
                command.CommandType = CommandType.StoredProcedure;

                var empleadoIdParam = new MySqlParameter("@e_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoespecialidadDto.EmpleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var especialidadIdParam = new MySqlParameter("@e_EspecialidadId", MySqlDbType.Int32)
                {
                    Value = empleadoespecialidadDto.EspecialidadId
                };
                command.Parameters.Add(especialidadIdParam);

                var fechaCertificacionParam = new MySqlParameter("@e_FechaCertificacion", MySqlDbType.DateTime)
                {
                    Value = empleadoespecialidadDto.FechaCertificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaCertificacionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return empleadoespecialidadDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar EmpleadoEspecialidad");
            }
        }
        public async Task<bool> DeleteEmpleadoEspecialidad(int empleadoId,int especialidadId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarEmpleadoEspecialidad";
                command.CommandType = CommandType.StoredProcedure;

                var empleadoIdParam = new MySqlParameter("@e_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoId
                };

                var especialidadIdParam = new MySqlParameter("@e_EspecialidadId", MySqlDbType.Int32)
                {
                    Value = especialidadId
                };

                var resultParam = new MySqlParameter("@Resultado", MySqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(empleadoIdParam);
                command.Parameters.Add(especialidadIdParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar EmpleadoEspecialidad", ex);
            }
        }
        public async Task<List<DtoEmpleadoEspecialidad>> GetEmpleadoEspecialidad()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleadoEspecialidad";
                command.CommandType = CommandType.StoredProcedure;

                var empleadoespecialidades = new List<DtoEmpleadoEspecialidad>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var empleadoespecialidad = new DtoEmpleadoEspecialidad
                        {
                            EmpleadoId = reader.GetInt32(0),
                            EspecialidadId = reader.GetInt32(1),
                            FechaCertificacion = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        empleadoespecialidades.Add(empleadoespecialidad);
                    }
                    await reader.CloseAsync();
                    return empleadoespecialidades;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener EmpleadoEspecialidad", ex);
            }
        }
        public async Task<DtoEmpleadoEspecialidad> GetEmpleadoEspecialidadById(int empleadoId, int especialidadId)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEmpleadoEspecialidadPorId";
                command.CommandType = CommandType.StoredProcedure;

                var empleadoIdParam = new MySqlParameter("@e_EmpleadoId", MySqlDbType.Int32)
                {
                    Value = empleadoId
                };
                command.Parameters.Add(empleadoIdParam);

                var especialidadIdParam = new MySqlParameter("@e_EspecialidadId", MySqlDbType.Int32)
                {
                    Value = especialidadId
                };
                command.Parameters.Add(especialidadIdParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var empleadoespecialidad = new DtoEmpleadoEspecialidad
                        {
                            EmpleadoId = reader.GetInt32(0),
                            EspecialidadId = reader.GetInt32(1),
                            FechaCertificacion = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        await connection.CloseAsync();
                        return empleadoespecialidad;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener EmpleadoEspecialidad por ID", ex);
            }
        }
        public async Task<bool> EmpleadoEspecialidadExists(int empleadoId, int especialidadId)
        {
            return await _context.EmpleadoEsepecialidad.AnyAsync(e => e.EmpleadoId == empleadoId && e.EspecialidadId == especialidadId);
        }
    }
}
