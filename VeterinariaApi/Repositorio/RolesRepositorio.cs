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
    public class RolesRepositorio : IRolesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public RolesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<DtoRoles> Create(DtoRoles rolesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRoles";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                
                var nombreRolParam = new MySqlParameter("@r_NombreRol", MySqlDbType.VarChar, 100)
                {
                    Value = rolesDto.NombreRol ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreRolParam);
                
                var descripcionParam = new MySqlParameter("@r_Descripcion", MySqlDbType.VarChar, 100)
                {
                    Value = rolesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                command.ExecuteNonQuery();
                transaction.Commit();
                return Task.FromResult(rolesDto);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear el rol", ex);
            }
        }

        public Task<DtoRoles> Update(DtoRoles rolesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarRoles";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = rolesDto.Id > 0 ? (object)rolesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);
                
                var nombreRolParam = new MySqlParameter("@r_NombreRol", MySqlDbType.VarChar, 100)
                {
                    Value = rolesDto.NombreRol ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreRolParam);
                
                var descripcionParam = new MySqlParameter("@r_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = rolesDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);
                
                command.ExecuteNonQuery();
                
                transaction.Commit();
                
                return Task.FromResult(rolesDto);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al actualizar el rol", ex);
            }
        }
        public async Task<bool> DeleteRoles(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarRol";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar Rol", ex);
            }
        }

        public async Task<List<DtoRoles>> GetRoles()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRol";
                command.CommandType = CommandType.StoredProcedure;

                var roles = new List<DtoRoles>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var role = new DtoRoles
                        {
                            Id = reader.GetInt32(0),
                            NombreRol = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        roles.Add(role);
                    }
                }
                await connection.CloseAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles", ex);
            }
        }

        public async Task<DtoRoles> GetRolesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerRolesPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@r_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var roleDto = new DtoRoles
                    {
                        Id = reader.GetInt32(0),
                        NombreRol = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                    };
                    await connection.CloseAsync();
                    return roleDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rol por ID", ex);
            }
        }

        public async Task<bool> RolesExists(int id)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id);
        }
    }
}
