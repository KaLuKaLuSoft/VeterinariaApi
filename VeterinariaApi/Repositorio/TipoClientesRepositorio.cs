using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VeterinariaApi.Repositorio
{
    public class TipoClientesRepositorio : ITipoClientesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoClientesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoTipoCliente> Create(DtoTipoCliente tipoclientesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var comand = _context.Database.GetDbConnection().CreateCommand();
                comand.Transaction = transaction.GetDbTransaction();
                comand.CommandText = "InsertarActualizarTipoClientes";
                comand.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@tc_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                comand.Parameters.Add(idParam);

                var nombreTipoParam = new MySqlConnector.MySqlParameter("@tc_NombreTipo", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = tipoclientesDto.NombreTipo ?? (object)DBNull.Value
                };
                comand.Parameters.Add(nombreTipoParam);

                var descripcionParam = new MySqlConnector.MySqlParameter("@tc_Descripcion", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = tipoclientesDto.Descripcion ?? (object)DBNull.Value
                };
                comand.Parameters.Add(descripcionParam);

                var ActivoParam = new MySqlParameter("@tc_Activo", MySqlDbType.Bit)
                {
                    Value = tipoclientesDto.Activo
                };
                comand.Parameters.Add(ActivoParam);

                await comand.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoclientesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el tipo de cliente", ex);
            }
        }

        public async Task<DtoTipoCliente> Update(DtoTipoCliente tipoclientesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var comand = _context.Database.GetDbConnection().CreateCommand();
                comand.Transaction = transaction.GetDbTransaction();
                comand.CommandText = "InsertarActualizarTipoClientes";
                comand.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@tc_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = tipoclientesDto.Id > 0 ? (object)tipoclientesDto.Id : (object)DBNull.Value
                };
                comand.Parameters.Add(idParam);

                var nombreTipoParam = new MySqlConnector.MySqlParameter("@tc_NombreTipo", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = tipoclientesDto.NombreTipo ?? (object)DBNull.Value
                };
                comand.Parameters.Add(nombreTipoParam);

                var descripcionParam = new MySqlConnector.MySqlParameter("@tc_Descripcion", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = tipoclientesDto.Descripcion ?? (object)DBNull.Value
                };
                comand.Parameters.Add(descripcionParam);

                var ActivoParam = new MySqlParameter("@tc_Activo", MySqlDbType.Bit)
                {
                    Value = tipoclientesDto.Activo
                };
                comand.Parameters.Add(ActivoParam);

                await comand.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipoclientesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el tipo de cliente", ex);
            }
        }
        public async Task<bool> DeleteTipoClientes(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoClientes";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlConnector.MySqlParameter("@tc_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlConnector.MySqlParameter("@resultado", MySqlConnector.MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el tipo de cliente", ex);
            }
        }
        public async Task<List<DtoTipoCliente>> GetTipoClientes()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoClientes";
                command.CommandType = CommandType.StoredProcedure;

                var tipoClientes = new List<DtoTipoCliente>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var tipoCliente = new DtoTipoCliente
                        {
                            Id = reader.GetInt32(0),
                            NombreTipo = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Activo = reader.IsDBNull(3) ? (bool?)null : reader.GetBoolean(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        tipoClientes.Add(tipoCliente);
                    }
                    await reader.CloseAsync();
                    return tipoClientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los tipos de clientes", ex);
            }
        }

        public async Task<DtoTipoCliente> GetTipoClientesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoClientesById";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@tc_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var tipocliente = new DtoTipoCliente
                        {
                            Id = reader.GetInt32(0),
                            NombreTipo = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Activo = reader.IsDBNull(3) ? (bool?)null : reader.GetBoolean(3),
                            Fecha_Alta = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Fecha_Modificacion = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };
                        await connection.CloseAsync();
                        return tipocliente;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al obtener tipo empleado. ", ex);
            }
        }
        public async Task<bool> TipoClientesExists(int id)
        {
            return await _context.TipoClientes.AnyAsync(e => e.Id == id);
        }

        public async Task<DtoTipoCliente> Delete(DtoTipoCliente tipoclientesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoClientes";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@tc_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = tipoclientesDto.Id
                };

                var resultParam = new MySqlConnector.MySqlParameter("@resultado", MySqlConnector.MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParam);
                command.Parameters.Add(resultParam);

                if (command.Connection.State != ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                var resultado = resultParam.Value != null && int.TryParse(resultParam.Value.ToString(), out var r) ? r : 0;

                if (resultado == 1)
                {
                    tipoclientesDto.Activo = false;
                    // si usas IsDeleted en DTO/modelo, marcarlo también
                    // tipoclientesDto.IsDeleted = true; // si existe en DTO
                    return tipoclientesDto;
                }

                return null;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el tipo de cliente", ex);
            }
        }
    }
}
