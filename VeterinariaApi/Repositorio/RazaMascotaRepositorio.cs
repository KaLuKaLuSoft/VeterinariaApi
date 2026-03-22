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
    public class RazaMascotaRepositorio : IRazaMascotaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RazaMascotaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoRazaMascota> Create(DtoRazaMascota razaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRazaMascota";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@rm_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreRazaParam = new MySqlParameter("@rm_NombreRaza", MySqlDbType.VarChar, 100)
                {
                    Value = razaMascotaDto.NombreRaza ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreRazaParam);

                var IdEspecieMascotaParam = new MySqlParameter("@rm_IdEspecieMascota", MySqlDbType.Int32)
                {
                    Value = razaMascotaDto.IdEspecieMascota
                };
                command.Parameters.Add(IdEspecieMascotaParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return razaMascotaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear o actualizar la raza de mascota.", ex);
            }
        }
        public async Task<DtoRazaMascota> Update(DtoRazaMascota razaMascotaDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRazaMascota";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@rm_Id", MySqlDbType.Int32)
                {
                    Value = razaMascotaDto.Id > 0 ? (object)razaMascotaDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreRazaParam = new MySqlParameter("@rm_NombreRaza", MySqlDbType.VarChar, 100)
                {
                    Value = razaMascotaDto.NombreRaza ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreRazaParam);

                var IdEspecieMascotaParam = new MySqlParameter("@rm_IdEspecieMascota", MySqlDbType.Int32)
                {
                    Value = razaMascotaDto.IdEspecieMascota
                };
                command.Parameters.Add(IdEspecieMascotaParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return razaMascotaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la raza de mascota.", ex);
            }
        }

        public async Task<bool> DeleteRazaMascota(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarRazaMascota";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@rm_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar la raza de mascota.", ex);
            }
        }

        public async Task<List<DtoRazaMascota>> GetRazaMascota()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRazaMascota";
                command.CommandType = CommandType.StoredProcedure;

                var razamascotas = new List<DtoRazaMascota>();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var razaMascota = new DtoRazaMascota
                        {
                            Id = reader.GetInt32("Id"),
                            NombreRaza = reader.GetString("NombreRaza"),
                            IdEspecieMascota = reader.GetInt32("IdEspecieMascota"),
                            EspecieMascota = reader.IsDBNull("EspecieMascota") ? null : reader.GetString("EspecieMascota"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        razamascotas.Add(razaMascota);
                    }
                }
                return razamascotas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las razas de mascotas.", ex);
            }
        }

        public async Task<DtoRazaMascota> GetRazaMascotaById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync()
                    ;
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRazaMascotaPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var razaMascotas = new DtoRazaMascota
                    {
                        Id = reader.GetInt32("Id"),
                        NombreRaza = reader.GetString("NombreRaza"),
                        IdEspecieMascota = reader.GetInt32("IdEspecieMascota"),
                        EspecieMascota = reader.IsDBNull("EspecieMascota") ? null : reader.GetString("EspecieMascota"),
                        Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                        Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                    };
                    await connection.CloseAsync();
                    return razaMascotas;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la raza de mascota por ID.", ex);
            }
        }

        public async Task<bool> RazaMascotaExists(int id)
        {
            return await _context.RazaMascotas.AnyAsync(r => r.Id == id);
        }
    }
}
