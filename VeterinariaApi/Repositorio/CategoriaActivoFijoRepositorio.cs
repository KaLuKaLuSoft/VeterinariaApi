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
    public class CategoriaActivoFijoRepositorio : ICategoriaActivoFijoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaActivoFijoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoCategoriaActivoFijo> Create(DtoCategoriaActivoFijo CategoriaActivoFijoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCategoriaActivoFijo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@caf_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var NombreCategoriaFijo = new MySqlParameter("@caf_NombreCategoriaFijo", MySqlDbType.VarChar, 100)
                {
                    Value = CategoriaActivoFijoDto.NombreCategoriaActivoFijo ?? (object)DBNull.Value
                };
                command.Parameters.Add(NombreCategoriaFijo);

                var Descripcion = new MySqlParameter("@caf_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = CategoriaActivoFijoDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(Descripcion);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return CategoriaActivoFijoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la Categoría Activo Fijo", ex);
            }
        }
        public async Task<DtoCategoriaActivoFijo> Update(DtoCategoriaActivoFijo CategoriaActivoFijoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCategoriaActivoFijo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@caf_Id", MySqlDbType.Int32)
                {
                    Value = CategoriaActivoFijoDto.Id > 0 ? (object)CategoriaActivoFijoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var NombreCategoriaFijo = new MySqlParameter("@caf_NombreCategoriaFijo", MySqlDbType.VarChar, 100)
                {
                    Value = CategoriaActivoFijoDto.NombreCategoriaActivoFijo ?? (object)DBNull.Value
                };
                command.Parameters.Add(NombreCategoriaFijo);

                var Descripcion = new MySqlParameter("@caf_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = CategoriaActivoFijoDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(Descripcion);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return CategoriaActivoFijoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la Categoría Activo Fijo", ex);
            }
        }
        public async Task<bool> DeleteCategoriaActivoFijo(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarCategoriaActivoFijo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@caf_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar la Categoría Activo Fijo", ex);
            }
        }
        public async Task<List<DtoCategoriaActivoFijo>> GetCategoriaActivoFijO()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCategoriaActivoFijo";
                command.CommandType = CommandType.StoredProcedure;

                var categoriaActivoFijos = new List<DtoCategoriaActivoFijo>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var categoriaActivoFijo = new DtoCategoriaActivoFijo
                        {
                            Id = reader.GetInt32("Id"),
                            NombreCategoriaActivoFijo = reader.IsDBNull("NombreCategoriaActivoFijo") ? null : reader.GetString("NombreCategoriaActivoFijo"),
                            Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        categoriaActivoFijos.Add(categoriaActivoFijo);
                    }
                    await reader.CloseAsync();
                    return categoriaActivoFijos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Categorías de Activo Fijo", ex);
            }
        }
        public async Task<DtoCategoriaActivoFijo> GetCategoriaActivoFijoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCategoriaActivoFijoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@caf_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var categoriaActivoFijos = new DtoCategoriaActivoFijo
                        {
                            Id = reader.GetInt32("Id"),
                            NombreCategoriaActivoFijo = reader.IsDBNull("NombreCategoriaActivoFijo") ? null : reader.GetString("NombreCategoriaActivoFijo"),
                            Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        await connection.CloseAsync();
                        return categoriaActivoFijos;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la Categoría de Activo Fijo por ID", ex);
            }
        }
        public async Task<bool> CategoriaActivoFijoExists(int id)
        {
            return await _context.CategoriaActivoFijo.AnyAsync(c => c.Id == id);
        }
    }
}
