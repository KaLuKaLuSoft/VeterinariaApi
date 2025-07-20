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
    public class SubModuloRepositorio : ISubModuloRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public SubModuloRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoSubModulo> Create(DtoSubModulo submoduloDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarSubModulos";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@sm_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreSubModuloParam = new MySqlParameter("@sm_NombreSubModulo", MySqlDbType.VarChar, 100)
                {
                    Value = submoduloDto.NombreSubModulo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreSubModuloParam);

                var descripcionParam = new MySqlParameter("@sm_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = submoduloDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                var idModuloParam = new MySqlParameter("@sm_ModuloId", MySqlDbType.Int32)
                {
                    Value = submoduloDto.ModuloId
                };
                command.Parameters.Add(idModuloParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return submoduloDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear el submódulo", ex);
            }
        }    
        public async Task<DtoSubModulo> Update(DtoSubModulo submoduloDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarSubModulos";
                command.CommandType = CommandType.StoredProcedure;

                var idaram = new MySqlParameter("@sm_Id", MySqlDbType.Int32)
                {
                    Value = submoduloDto.Id > 0 ? (object)submoduloDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idaram);

                var nombreSubModuloParam = new MySqlParameter("@sm_NombreSubModulo", MySqlDbType.VarChar, 100)
                {
                    Value = submoduloDto.NombreSubModulo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreSubModuloParam);

                var descripcionParam = new MySqlParameter("@sm_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = submoduloDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);
                
                var idModuloParam = new MySqlParameter("@sm_ModuloId", MySqlDbType.Int32)
                {
                    Value = submoduloDto.ModuloId
                };
                command.Parameters.Add(idModuloParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return submoduloDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar el submódulo", ex);
            }
        }
        public async Task<bool> DeleteSubModulo(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarSubModulo";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@sm_Id", MySqlDbType.Int32)
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
                transaction.Rollback();
                throw new Exception("Error al eliminar el submódulo", ex);
            }
        }
        public async Task<List<DtoSubModulo>> GetSubModulo()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerSubModulo";
                command.CommandType = CommandType.StoredProcedure;

                var submodulos = new List<DtoSubModulo>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var submodulosDto = new DtoSubModulo
                        {
                            Id = reader.GetInt32(0),
                            NombreSubModulo = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Descripcion =   reader.IsDBNull(2) ? null : reader.GetString(2),
                            ModuloId = reader.GetInt32(3),
                            NombreModulo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Fecha_Alta = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            Fecha_Modificacion = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                        };
                        submodulos.Add(submodulosDto);
                    }
                    await connection.CloseAsync();
                    return submodulos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los submódulos", ex);
            }
        }
        public async Task<DtoSubModulo> GetSubModuloById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerSubModuloPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@sm_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var submoduloDto = new DtoSubModulo
                    {
                        Id = reader.GetInt32(0),
                        NombreSubModulo = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                        ModuloId = reader.GetInt32(3),
                        NombreModulo = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Fecha_Alta = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                        Fecha_Modificacion = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                    };
                    await connection.CloseAsync();
                    return submoduloDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el submódulo por ID", ex);
            }
        }
        public async Task<bool> SubModuloExists(int id)
        {
            return await _context.SubModulos.AnyAsync(sm => sm.Id == id);
        }
    }
}
