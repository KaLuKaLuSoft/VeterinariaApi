using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class ConvivenciaMascotaRepositorio : IConvivenciaMascotaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConvivenciaMascotaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoConvivenciaMascota> Create(DtoConvivenciaMascota convivenciaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarConvivenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cm_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var saleCalleParam = new MySqlParameter("@cm_SaleCalle", MySqlDbType.VarChar, 100)
                {
                    Value = convivenciaMascotaDto.SaleCalle ?? (object)DBNull.Value
                };
                command.Parameters.Add(saleCalleParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();
                return convivenciaMascotaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear o actualizar ConvivenciaMascota: " + ex);
            }
        }
        public async Task<DtoConvivenciaMascota> Update(DtoConvivenciaMascota convivenciaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarConvivenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cm_Id", MySqlDbType.Int32)
                {
                    Value = convivenciaMascotaDto.Id > 0 ? (object)convivenciaMascotaDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var saleCalleParam = new MySqlParameter("@cm_SaleCalle", MySqlDbType.Int32)
                {
                    Value = convivenciaMascotaDto.SaleCalle ?? (object)DBNull.Value
                };
                command.Parameters.Add(saleCalleParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();
                return convivenciaMascotaDto;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear o actualizar ConvivenciaMascota: " + ex);
            }
        }
        public async Task<bool> DeleteConvivenciaMascota(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarConvivenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cm_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar ConvivenciaMascota: " + ex);
            }
        }
        public async Task<List<DtoConvivenciaMascota>> GetConvivenciaMascota()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerConvivenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var convivenciaMascotas = new List<DtoConvivenciaMascota>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var convivenciaMascota = new DtoConvivenciaMascota
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SaleCalle = reader.IsDBNull(reader.GetOrdinal("SaleCalle")) ? null : reader.GetString(reader.GetOrdinal("SaleCalle"))
                        };
                        convivenciaMascotas.Add(convivenciaMascota);
                    }
                    await connection.CloseAsync();
                    return convivenciaMascotas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ConvivenciaMascota: " + ex);
            }
        }
        public async Task<DtoConvivenciaMascota> GetConvivenciaMascotaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerConvivenciaMascotaPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cm_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var convivenciaMascota = new DtoConvivenciaMascota
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SaleCalle = reader.IsDBNull(reader.GetOrdinal("SaleCalle")) ? null : reader.GetString(reader.GetOrdinal("SaleCalle"))
                        };
                        return convivenciaMascota;
                    }
                    else
                    {
                        await connection.CloseAsync();
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ConvivenciaMascota. " + ex);
            }
        }
        public async Task<bool> ConvivenciaMascotaExists(int id)
        {
            return await _context.ConvivenciaMascota.AnyAsync(cm => cm.Id == id);
        }
    }
}
