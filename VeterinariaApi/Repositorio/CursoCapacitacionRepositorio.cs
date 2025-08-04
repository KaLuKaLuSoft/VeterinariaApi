using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class CursoCapacitacionRepositorio : ICursoCapacitacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CursoCapacitacionRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoCursoCapacitacion> Create(DtoCursoCapacitacion cursoCapacitacionDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCursoCapacitacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cc_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCursoParam = new MySqlParameter("@cc_NombreCurso", MySqlDbType.VarChar, 100)
                {
                    Value = cursoCapacitacionDto.NombreCurso ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCursoParam);

                var descripcionParam = new MySqlParameter("@cc_Descripcion", MySqlDbType.VarChar, 100)
                {
                    Value = cursoCapacitacionDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                var duracionParam = new MySqlParameter("@cc_Duracion", MySqlDbType.VarChar, 50)
                {
                    Value = cursoCapacitacionDto.Duracion ?? (object)DBNull.Value
                };
                command.Parameters.Add(duracionParam);

                var proveedorParam = new MySqlParameter("@cc_Proveedor", MySqlDbType.VarChar, 100)
                {
                    Value = cursoCapacitacionDto.Proveedor ?? (object)DBNull.Value
                };
                command.Parameters.Add(proveedorParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return cursoCapacitacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el curso de capacitación", ex);
            }
        }
        public async Task<DtoCursoCapacitacion> Update(DtoCursoCapacitacion cursoCapacitacionDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCursoCapacitacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cc_Id", MySqlDbType.Int32)
                {
                    Value = cursoCapacitacionDto.Id > 0 ? cursoCapacitacionDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCursoParam = new MySqlParameter("@cc_NombreCurso", MySqlDbType.VarChar, 255)
                {
                    Value = cursoCapacitacionDto.NombreCurso ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCursoParam);

                var descripcionParam = new MySqlParameter("@cc_Descripcion", MySqlDbType.VarChar, 100)
                {
                    Value = cursoCapacitacionDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                var duracionParam = new MySqlParameter("@cc_Duracion", MySqlDbType.VarChar, 50)
                {
                    Value = cursoCapacitacionDto.Duracion ?? (object)DBNull.Value
                };
                command.Parameters.Add(duracionParam);

                var proveedorParam = new MySqlParameter("@cc_Proveedor", MySqlDbType.VarChar, 100)
                {
                    Value = cursoCapacitacionDto.Proveedor ?? (object)DBNull.Value
                };
                command.Parameters.Add(proveedorParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return cursoCapacitacionDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el curso de capacitación", ex);
            }
        }
        public async Task<bool> DeleteCursoCapacitacion(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarCursoCapacitacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cc_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el curso de capacitación", ex);
            }
        }
        public async Task<List<DtoCursoCapacitacion>> GetCursoCapacitacion()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCursoCapacitacion";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var cursocapacitacion = new List<DtoCursoCapacitacion>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var cursoCapacitacionDto = new DtoCursoCapacitacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreCurso = reader.IsDBNull(reader.GetOrdinal("NombreCurso")) ? null : reader.GetString(reader.GetOrdinal("NombreCurso")),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                            Duracion = reader.IsDBNull(reader.GetOrdinal("Duracion")) ? null : reader.GetString(reader.GetOrdinal("Duracion")),
                            Proveedor = reader.IsDBNull(reader.GetOrdinal("Proveedor")) ? null : reader.GetString(reader.GetOrdinal("Proveedor")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        cursocapacitacion.Add(cursoCapacitacionDto);
                    }
                    await reader.CloseAsync();
                    return cursocapacitacion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los cursos de capacitación", ex);
            }
        }
        public async Task<DtoCursoCapacitacion> GetCursoCapacitacionById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCursoCapacitacionPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cc_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var cursoCapacitacionDto = new DtoCursoCapacitacion
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreCurso = reader.IsDBNull(reader.GetOrdinal("NombreCurso")) ? null : reader.GetString(reader.GetOrdinal("NombreCurso")),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                            Duracion = reader.IsDBNull(reader.GetOrdinal("Duracion")) ? null : reader.GetString(reader.GetOrdinal("Duracion")),
                            Proveedor = reader.IsDBNull(reader.GetOrdinal("Proveedor")) ? null : reader.GetString(reader.GetOrdinal("Proveedor")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        await connection.CloseAsync();
                        return cursoCapacitacionDto;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el curso de capacitación por ID", ex);
            }
        }
        public async Task<bool> CursoCapacitacionExists(int id)
        {
            return await _context.CursoCapacitacion.AnyAsync(c => c.Id == id);
        }
    }
}
