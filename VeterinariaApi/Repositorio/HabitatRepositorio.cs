using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class HabitatRepositorio : IHabitatRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HabitatRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoHabitatMascota> Create(DtoHabitatMascota habitatDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarHabitatMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@h_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombrehabitatParam = new MySqlParameter("@h_NombreHabitat", MySqlDbType.VarChar, 255)
                {
                    Value = habitatDto.NombreHabitat ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombrehabitatParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return habitatDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el hábitat: " + ex);
            }
        }
        public async Task<DtoHabitatMascota> Update(DtoHabitatMascota habitatDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarHabitatMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@h_Id", MySqlDbType.Int32)
                {
                    Value = habitatDto.Id > 0 ? (object)habitatDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombrehabitatParam = new MySqlParameter("@h_NombreHabitat", MySqlDbType.VarChar, 100)
                {
                    Value = habitatDto.NombreHabitat ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombrehabitatParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return habitatDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el hábitat: " + ex);
            }
        }
        public async Task<bool> DeleteHabitat(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarHabitatMascota";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@h_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar Habitat", ex);
            }
        }

        public async Task<List<DtoHabitatMascota>> GetHabitat()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerHabitat";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var habitats = new List<DtoHabitatMascota>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var habitat = new DtoHabitatMascota
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        NombreHabitat = reader.IsDBNull(reader.GetOrdinal("NombreHabitat")) ? null : reader.GetString(reader.GetOrdinal("NombreHabitat"))
                    };
                    habitats.Add(habitat);
                }
                await connection.CloseAsync();
                return habitats;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los hábitats: " + ex);
            }
        }

        public async Task<DtoHabitatMascota> GetHabitatById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerHabitatPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@h_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var habitat = new DtoHabitatMascota
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        NombreHabitat = reader.IsDBNull(reader.GetOrdinal("NombreHabitat")) ? null : reader.GetString(reader.GetOrdinal("NombreHabitat"))
                    };
                    return habitat;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el hábitat. " + ex);
            }
        }

        public async Task<bool> HabitatExists(int id)
        {
            return await _context.HabitatMascota.AnyAsync(h => h.Id == id);
        }
    }
}
