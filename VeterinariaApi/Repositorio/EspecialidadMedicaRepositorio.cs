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
    public class EspecialidadMedicaRepositorio : IEspecialidadMedicaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EspecialidadMedicaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<DtoEpecialidadesMedicas> Create(DtoEpecialidadesMedicas especialidadesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEspecialdiadMedica";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var especialidadParam = new MySqlParameter("@e_NombreEspecialidad", MySqlDbType.VarChar, 100)
                {
                    Value = especialidadesDto.NombreEspecialidad ?? (object)DBNull.Value
                };
                command.Parameters.Add(especialidadParam);

                var descripcionParam = new MySqlParameter("@e_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = especialidadesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                command.ExecuteNonQuery();
                transaction.Commit();
                return Task.FromResult(especialidadesDto);
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw new Exception("Error al crear la especialidad", ex);
            }
        }

        public Task<DtoEpecialidadesMedicas> Update(DtoEpecialidadesMedicas especialidadesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEspecialdiadMedica";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = especialidadesDto.Id > 0 ? (object)especialidadesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var especialidadParam = new MySqlParameter("@e_NombreEspecialidad", MySqlDbType.VarChar, 100)
                {
                    Value = especialidadesDto.NombreEspecialidad ?? (object)DBNull.Value
                };
                command.Parameters.Add(especialidadParam);

                var descripcionParam = new MySqlParameter("@e_Descripcion", MySqlDbType.VarChar, 100)
                {
                    Value = especialidadesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                command.ExecuteNonQuery();
                transaction.Commit();

                return Task.FromResult(especialidadesDto);
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar la especialidad", ex);
            }
        }

        public async Task<bool> DeleteEspecialidades(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarExpecialidadMedica";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar la Especialidad", ex);
            }
        }

        public async Task<List<DtoEpecialidadesMedicas>> GetEspecialidades()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEspecialidadMedica";
                command.CommandType = CommandType.StoredProcedure;

                var especialidades = new List<DtoEpecialidadesMedicas>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var especialidad = new DtoEpecialidadesMedicas
                        {
                            Id = reader.GetInt32(0),
                            NombreEspecialidad = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        especialidades.Add(especialidad);
                    }
                }
                await connection.CloseAsync();
                return especialidades;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las especialidades", ex);
            }
        }

        public async Task<DtoEpecialidadesMedicas> GetEspecialidadesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerEspecialidadMedicaPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@e_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var especialidadDto = new DtoEpecialidadesMedicas
                    {
                        Id = reader.GetInt32(0),
                        NombreEspecialidad = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                    };
                    await connection.CloseAsync();
                    return especialidadDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al obtener la especialidad por ID", ex);
            }
        }

        public async Task<bool> EspecialidadesExists(int id)
        {
            return await _context.EspecialidadesMedicas.AnyAsync(r => r.Id == id);
        }
    }
}
