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
    public class ConsumoAlimentoRepositorio : IConsumoAlimentoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConsumoAlimentoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoConsumoAlimento> Create(DtoConsumoAlimento consumoAlimentoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "InsertarActualizarConsumoAlimento";

                var idParam = new MySqlParameter("@ca_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var consumoParam = new MySqlParameter("@ca_Veces", MySqlDbType.VarChar)
                {
                    Value = consumoAlimentoDto.Veces ?? (object)DBNull.Value
                };
                command.Parameters.Add(consumoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return consumoAlimentoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al insertar o actualizar el consumo de alimento.", ex);
            }
        }
        public async Task<DtoConsumoAlimento> Update(DtoConsumoAlimento consumoAlimentoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarConsumoAlimento";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@ca_Id", MySqlDbType.Int32)
                {
                    Value = consumoAlimentoDto.Id > 0 ? (object)consumoAlimentoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var consumoParam = new MySqlParameter("@ca_Veces", MySqlDbType.VarChar)
                {
                    Value = consumoAlimentoDto.Veces ?? (object)DBNull.Value
                };
                command.Parameters.Add(consumoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return consumoAlimentoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al insertar o actualizar el consumo de alimento.", ex);
            }
        }
        public async Task<bool> DeleteConsumoAlimento(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarConsumoAlimento";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@ca_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el consumo de alimento.", ex);
            }
        }
        public async Task<List<DtoConsumoAlimento>> GetConsumoAlimento()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "@ObtenerConsumoAlimento";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var consumoalimentos = new List<DtoConsumoAlimento>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var consumoalimento = new DtoConsumoAlimento
                        {
                            Id = reader.GetInt32("Id"),
                            Veces = reader.GetString("Veces")
                        };
                        consumoalimentos.Add(consumoalimento);
                    }
                }
                await connection.CloseAsync();
                return consumoalimentos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el consumo de alimento.", ex);
            }
        }
        public async Task<DtoConsumoAlimento> GetConsumoAlimentoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "@ObtenerConsumoAlimentoPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@ca_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var consumoalimento = new DtoConsumoAlimento
                    {
                        Id = reader.GetInt32("Id"),
                        Veces = reader.GetString("Veces")
                    };
                    await connection.CloseAsync();
                    return consumoalimento;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del consumo de alimento.", ex);
            }
        }
        public async Task<bool> ConsumoAlimentoExists(int id)
        {
            return await _context.ConsumoAlimento.AnyAsync(c => c.Id == id);
        }
    }
}
