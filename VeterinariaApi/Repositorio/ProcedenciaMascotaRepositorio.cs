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
    public class ProcedenciaMascotaRepositorio : IProcedenciaMascotaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProcedenciaMascotaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoProcedenciaMascota> Create(DtoProcedenciaMascota procedenciaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarProcedenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@pm_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var procedenciaParam = new MySqlParameter("@pm_Procedencia", MySqlDbType.VarChar, 100)
                {
                    Value = procedenciaMascotaDto.Procedencia ?? (object)DBNull.Value
                };
                command.Parameters.Add(procedenciaParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();
                return procedenciaMascotaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear o actualizar ProcedenciaMascota: " + ex);
            }
        }
        public async Task<DtoProcedenciaMascota> Update(DtoProcedenciaMascota procedenciaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarProcedenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@pm_Id", MySqlDbType.Int32)
                {
                    Value = procedenciaMascotaDto.Id
                };
                command.Parameters.Add(idParam);

                var procedenciaParam = new MySqlParameter("@pm_Procedencia", MySqlDbType.VarChar, 100)
                {
                    Value = procedenciaMascotaDto.Procedencia ?? (object)DBNull.Value
                };
                command.Parameters.Add(procedenciaParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();
                return procedenciaMascotaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear o actualizar ProcedenciaMascota: " + ex);
            }
        }
        public async Task<bool> DeleteProcedenciaMascota(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarProcedenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@pm_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();
                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar ProcedenciaMascota: " + ex);
            }
        }

        public async Task<List<DtoProcedenciaMascota>> GetProcedenciaMascota()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerProcedenciaMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var procedencias = new List<DtoProcedenciaMascota>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var procedencia = new DtoProcedenciaMascota
                        {
                            Id = reader.GetInt32("Id"),
                            Procedencia = reader.GetString("Procedencia")
                        };
                        procedencias.Add(procedencia);
                    }
                    await connection.CloseAsync();
                    return procedencias;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ProcedenciaMascota: " + ex);
            }
        }

        public async Task<DtoProcedenciaMascota> GetProcedenciaMascotaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerProcedenciaMascotaPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@pm_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var procedencia = new DtoProcedenciaMascota
                        {
                            Id = reader.GetInt32("Id"),
                            Procedencia = reader.GetString("Procedencia")
                        };
                        return procedencia;
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
                throw new Exception("Error al obtener ProcedenciaMascota por Id: " + ex);
            }
        }

        public async Task<bool> ProcedenciaMascotaExists(int id)
        {
            return await _context.ProcedenciaMascota.AnyAsync(e => e.Id == id);
        }
    }
}
