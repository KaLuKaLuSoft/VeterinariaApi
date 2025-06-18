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
    public class CiudadRepositorio : ICiudadRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CiudadRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoCiudad> Create(DtoCiudad ciudadDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCiudad";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCiudadParam = new MySqlParameter("@c_NombreCiudad", MySqlDbType.VarChar, 100)
                {
                    Value = ciudadDto.NombreCiudad ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCiudadParam);

                var idRegionParam = new MySqlParameter("@c_IdRegion", MySqlDbType.Int32)
                {
                    Value = ciudadDto.IdRegion
                };
                command.Parameters.Add(idRegionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return ciudadDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear la ciudad", ex);
            }
        }

        public async Task<DtoCiudad> Update(DtoCiudad ciudadDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarCiudad";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = ciudadDto.Id > 0 ? (object)ciudadDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreCiudadParam = new MySqlParameter("@c_NombreCiudad", MySqlDbType.VarChar, 100)
                {
                    Value = ciudadDto.NombreCiudad ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCiudadParam);

                var idRegionParam = new MySqlParameter("@c_IdRegion", MySqlDbType.Int32)
                {
                    Value = ciudadDto.IdRegion
                };
                command.Parameters.Add(idRegionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return ciudadDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la ciudad", ex);
            }
        }

        public async Task<bool> DeleteCiudad(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarCiudad";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@c_Resultado", MySqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync(); ;
                
                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al eliminar la ciudad", ex); 
            }
        }

        public async Task<List<DtoCiudad>> GetCiudad()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCiudad";
                command.CommandType = CommandType.StoredProcedure;

                var region = new List<DtoCiudad>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var ciudad = new DtoCiudad
                        {
                            Id = reader.GetInt32(0),
                            NombreCiudad = reader.GetString(1),
                            IdRegion = reader.GetInt32(2),
                            NombreRegion = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        region.Add(ciudad);
                    }
                    await connection.CloseAsync();
                    return region;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ciudades", ex);
            }
        }

        public async Task<DtoCiudad> GetCiudadById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerCiudadPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var ciudad = new DtoCiudad
                    {
                        Id = reader.GetInt32(0),
                        NombreCiudad = reader.GetString(1),
                        IdRegion = reader.GetInt32(2),
                        NombreRegion = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                    };
                    await connection.CloseAsync();
                    return ciudad;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la ciudad por ID", ex);
            }
        }

        public async Task<bool> CiudadExists(int id)
        {
            return await _context.Ciudades.AnyAsync(c => c.Id == id);
        }
    }
}
