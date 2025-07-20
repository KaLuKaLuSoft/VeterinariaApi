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
    public class DepartamentosRepositorio : IDepartamentosRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public DepartamentosRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoDepartamentos> Create(DtoDepartamentos departamentosDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarDepartamento";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParameter = new MySqlParameter("@d_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParameter);

                var nombredepartamentosParameter = new MySqlParameter("@d_NombreDepartamento", MySqlDbType.VarChar, 100)
                {
                    Value = departamentosDto.NombreDepartamento ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombredepartamentosParameter);

                var descripcionParameter = new MySqlParameter("@d_Descripcion", MySqlDbType.VarChar, 500)
                {
                    Value = departamentosDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParameter);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return departamentosDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el departamento: ", ex);
            }
        }
        public async Task<DtoDepartamentos> Update(DtoDepartamentos departamentosDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarDepartamento";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParameter = new MySqlParameter("@d_Id", MySqlDbType.Int32)
                {
                    Value = departamentosDto.Id > 0 ? (object)departamentosDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParameter);

                var nombredepartamentosParameter = new MySqlParameter("@d_NombreDepartamento", MySqlDbType.VarChar, 100)
                {
                    Value = departamentosDto.NombreDepartamento ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombredepartamentosParameter);

                var descripcionParameter = new MySqlParameter("@d_Descripcion", MySqlDbType.VarChar, 500)
                {
                    Value = departamentosDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParameter);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return departamentosDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el departamento: ", ex);
            }
        }
        public async Task<bool> DeleteDepartamentos(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarDepartamento";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParameter = new MySqlParameter("@d_Id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParameter);
                command.Parameters.Add(resultParam);

                await command.ExecuteReaderAsync();
                await transaction.CommitAsync();

                int result = Convert.ToInt32(resultParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el departamento: ", ex);
            }
        }
        public async Task<List<DtoDepartamentos>> GetDepartamentos()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerDepartamento";
                command.CommandType = CommandType.StoredProcedure;

                var departamento = new List<DtoDepartamentos>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var departamentoDto = new DtoDepartamentos
                        {
                            Id = reader.GetInt32(0),
                            NombreDepartamento = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        };
                        departamento.Add(departamentoDto);
                    }
                    await connection.CloseAsync();
                    return departamento;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los departamentos: ", ex);
            }
        }

        public async Task<DtoDepartamentos> GetDepartamentosById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerDepartamentoPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParameter = new MySqlParameter("@d_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParameter);

                using var reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    var departamentoDto = new DtoDepartamentos
                    {
                        Id = reader.GetInt32(0),
                        NombreDepartamento = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        Fecha_Alta = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                        Fecha_Modificacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                    };
                    await connection.CloseAsync();
                    return departamentoDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el departamento por ID: ", ex);
            }
        }
        public async Task<bool> DeapartamentosExists(int id)
        {
            return await _context.Departamentos.AnyAsync(d => d.Id == id);
        }
    }
}