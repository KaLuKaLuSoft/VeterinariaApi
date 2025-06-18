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
    public class RegionesRepositorio : IRegionesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public RegionesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoRegiones> Create(DtoRegiones regionesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRegion";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                var idNombreDepartamentoParam = new MySqlParameter("@r_NombreDepartamento", MySqlDbType.VarChar, 100)
                {
                    Value = regionesDto.NombreDepartamento ?? (object)DBNull.Value
                };
                command.Parameters.Add(idNombreDepartamentoParam);
                var idPaisParam = new MySqlParameter("@r_IdPais", MySqlDbType.Int32)
                {
                    Value = regionesDto.IdPais
                };
                command.Parameters.Add(idPaisParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return regionesDto;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la región", ex);
            }
        }
        public async Task<DtoRegiones> Update(DtoRegiones regionesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRegion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = regionesDto.Id > 0 ? (object)regionesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                var nombreDepartamentoParam = new MySqlParameter("@r_NombreDepartamento", MySqlDbType.VarChar, 100)
                {
                    Value = regionesDto.NombreDepartamento ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreDepartamentoParam);
                var idPaisParam = new MySqlParameter("@r_IdPais", MySqlDbType.Int32)
                {
                    Value = regionesDto.IdPais
                };
                command.Parameters.Add(idPaisParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return regionesDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar la región", ex);
            }
        }
        public async Task<bool> DeleteRegiones(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarRegion";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@r_Resultado", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar la región", ex);
            }
        }
        public async Task<List<DtoRegiones>> GetRegiones()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRegion";
                command.CommandType = CommandType.StoredProcedure;

                var region = new List<DtoRegiones>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var regionDto = new DtoRegiones
                        {
                            Id = reader.GetInt32(0),
                            NombreDepartamento = reader.GetString(1),
                            IdPais = reader.GetInt32(2),
                            NombrePais = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        region.Add(regionDto);
                    }
                    await connection.CloseAsync();
                    return region;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las regiones", ex);
            }
        }
        public async Task<DtoRegiones> GetRegionesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRegionPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var regionDto = new DtoRegiones
                    {
                        Id = reader.GetInt32(0),
                        NombreDepartamento = reader.GetString(1),
                        IdPais = reader.GetInt32(2),
                        NombrePais = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                    };
                    await connection.CloseAsync();
                    return regionDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al obtener la región por ID", ex);
            }
        }
        public async Task<bool> RegionesExists(int id)
        {
            return await _context.Regiones.AnyAsync(r => r.Id == id);
        }
    }
}
