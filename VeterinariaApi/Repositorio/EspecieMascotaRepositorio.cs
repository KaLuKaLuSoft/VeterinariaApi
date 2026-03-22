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
    public class EspecieMascotaRepositorio : IEspecieMascotaRepositorio
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public EspecieMascotaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoEspecieMascota> Create(DtoEspecieMascota especieMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEspecieMascota";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@em_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreParam = new MySqlParameter("@em_NombreEspecie", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = especieMascotaDto.NombreEspecie ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreParam);

                var descripcionParam = new MySqlParameter("@em_Descripcion", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = especieMascotaDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return especieMascotaDto;
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la especie de mascota", ex);
            }
        }
        public async Task<DtoEspecieMascota> Update(DtoEspecieMascota especieMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEspecieMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@em_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = especieMascotaDto.Id > 0 ? (object)especieMascotaDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreParam = new MySqlConnector.MySqlParameter("@em_NombreEspecie", MySqlConnector.MySqlDbType.VarChar)
                {
                    Value = especieMascotaDto.NombreEspecie ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreParam);

                var descripcionParam = new MySqlConnector.MySqlParameter("@em_Descripcion", MySqlConnector.MySqlDbType.VarChar)
                {
                    Value = especieMascotaDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return especieMascotaDto;
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la especie de mascota", ex);
            }
        }
        public async Task<bool> DeleteEspecieMascota(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarEspecieMascota";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@em_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                };
                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar Especia Mascotas");
            }
        }
        public async Task<List<DtoEspecieMascota>> GetEspecieMascota()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEspecieMascota";
                command.CommandType = CommandType.StoredProcedure;

                var especiemascotas = new List<DtoEspecieMascota>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var especiemascota = new DtoEspecieMascota
                        {
                            Id = reader.GetInt32(0),
                            NombreEspecie = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        especiemascotas.Add(especiemascota);
                    }
                    await reader.CloseAsync();
                    return especiemascotas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las Especies", ex);
            }
        }
        public async Task<DtoEspecieMascota> GetEspecieMascotaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = ("ObtenerEspecieMascotaPorId");
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@em_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var especiemascotas = new DtoEspecieMascota
                        {
                            Id = reader.GetInt32(0),
                            NombreEspecie = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        await connection.CloseAsync();
                        return especiemascotas;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la Especie. ",ex);
            }
        }
        public async Task<bool> EspecieMascotaExists(int id)
        {
            return await _context.EspecieMascotas.AnyAsync(x => x.Id == id);
        } 
    }
}
