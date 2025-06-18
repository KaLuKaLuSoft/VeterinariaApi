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
    public class PaisesRepositorio : IPaisesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public PaisesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoPaises> Create(DtoPaises paisesDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try{
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarPais";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@p_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                var nombresParam = new MySqlParameter("@p_Nombre", MySqlDbType.VarChar, 100)
                {
                    Value = paisesDto.Nombre ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombresParam);
                var codigoParam = new MySqlParameter("@p_Codigo", MySqlDbType.VarChar, 10)
                {
                    Value = paisesDto.Codigo ?? (object)DBNull.Value
                };
                command.Parameters.Add(codigoParam);

                await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return paisesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el país", ex);
            }
        }
        public async Task<DtoPaises> Update(DtoPaises paisesDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarPais";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@p_Id", MySqlDbType.Int32)
                {
                    Value = paisesDto.Id > 0 ? (object)paisesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                var nombresParam = new MySqlParameter("@p_Nombre", MySqlDbType.VarChar, 100)
                {
                    Value = paisesDto.Nombre ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombresParam);
                var codigoParam = new MySqlParameter("@p_Codigo", MySqlDbType.VarChar, 3)
                {
                    Value = paisesDto.Codigo ?? (object)DBNull.Value
                };
                command.Parameters.Add(codigoParam);

                await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return paisesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el país", ex);
            }
        }
        public async Task<bool> DeletePaises(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarPais";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@p_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el país", ex);
            }
        }
        public async Task<List<DtoPaises>> GetPaises()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerPais";
                command.CommandType = CommandType.StoredProcedure;

                var pais = new List<DtoPaises>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var paisDto = new DtoPaises
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Codigo = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        pais.Add(paisDto);
                    }
                }
                await connection.CloseAsync();
                return pais;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los países", ex);
            }
        }
        public async Task<DtoPaises> GetPaisesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerPaisPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@p_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var paisDto = new DtoPaises
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Codigo = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                    };
                    await connection.CloseAsync();
                    return paisDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el país por ID", ex);
            }
        }
        public async Task<bool> PaisesExists(int id)
        {
            return await _context.Paises.AnyAsync(p => p.Id == id);
        }
    }
}
