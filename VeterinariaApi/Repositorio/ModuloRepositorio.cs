using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using System.Transactions;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
namespace VeterinariaApi.Repositorio
{
    public class ModuloRepositorio : IModuloRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ModuloRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoModulo> Create(DtoModulo moduloDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarModulo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@m_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreModuloParam = new MySqlParameter("@m_NombreModulo", MySqlDbType.VarChar, 100)
                {
                    Value = moduloDto.NombreModulo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreModuloParam);

                var descripcionParam = new MySqlParameter("@m_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = moduloDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return moduloDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el módulo", ex);
            }
        }
        public async Task<DtoModulo> Update(DtoModulo moduloDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarModulo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@m_Id", MySqlDbType.Int32)
                {
                    Value = moduloDto.Id > 0 ? (object)moduloDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreModuloParam = new MySqlParameter("@m_NombreModulo", MySqlDbType.VarChar, 100)
                {
                    Value = moduloDto.NombreModulo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreModuloParam);

                var descripcionParam = new MySqlParameter("@m_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = moduloDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return moduloDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el módulo", ex);
            }
        }
        public async Task<bool> DeleteModulo(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarModulo";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@m_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el módulo", ex);
            }
        }
        public async Task<List<DtoModulo>> GetModulo()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerModulos";
                command.CommandType = CommandType.StoredProcedure;

                var modulo = new List<DtoModulo>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var ModuloDto = new DtoModulo
                        {
                            Id = reader.GetInt32(0),
                            NombreModulo = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        modulo.Add(ModuloDto);
                    }
                }
                await connection.CloseAsync();
                return modulo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los módulos", ex);
            }
        }
        public async Task<DtoModulo> GetModuloById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerModuloPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@m_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var moduloDto = new DtoModulo
                    {
                        Id = reader.GetInt32(0),
                        NombreModulo = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                    };
                    await connection.CloseAsync();
                    return moduloDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el módulo por ID", ex);
            }
        }
        public async Task<bool> ModuloExists(int id)
        {
            return await _context.Modulos.AnyAsync(m => m.Id == id);
        }
    }
}
