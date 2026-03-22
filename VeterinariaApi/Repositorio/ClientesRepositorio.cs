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
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoClientes> Create(DtoClientes clientesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarClientes";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int64)
                {
                    Value = (object)DBNull.Value,
                };
                command.Parameters.Add(idParam);

                var nomeClientesParam = new MySqlParameter("@c_NombreCliente", MySqlDbType.VarChar, 100)
                {
                    Value = clientesDto.NombreCliente ?? (object)DBNull.Value,
                };
                command.Parameters.Add(nomeClientesParam);

                var direccionClientesParam = new MySqlParameter("@c_DireccionCliente", MySqlDbType.VarChar, 150)
                {
                    Value = clientesDto.DireccionCliente ?? (object)DBNull.Value,
                };
                command.Parameters.Add(direccionClientesParam);

                var emailParam = new MySqlParameter("@c_Email", MySqlDbType.VarChar, 100)
                {
                    Value = clientesDto.Email ?? (object)DBNull.Value,
                };
                command.Parameters.Add(emailParam);

                var celularParam = new MySqlParameter("@c_Celular", MySqlDbType.Int32)
                {
                    Value = clientesDto.Celular ?? (object)DBNull.Value,
                };
                command.Parameters.Add(celularParam);

                var activoParam = new MySqlParameter("@c_Activo", MySqlDbType.Bit)
                {
                    Value = clientesDto.Activo,
                };
                command.Parameters.Add(activoParam);

                var idTipoClienteParam = new MySqlParameter("@c_IdTipoCliente", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdTipoCliente,
                };
                command.Parameters.Add(idTipoClienteParam);

                var idCiudadParam = new MySqlParameter("@c_IdCiudad", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdCiudad,
                };
                command.Parameters.Add(idCiudadParam);

                var idEmpresaParam = new MySqlParameter("@c_IdEmpresa", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdEmpresa,
                };
                command.Parameters.Add(idEmpresaParam);

                var observacionesParam = new MySqlParameter("@c_Observaciones", MySqlDbType.VarChar, 255)
                {
                    Value = clientesDto.Observaciones ?? (object)DBNull.Value,
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return clientesDto;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al crear el cliente: " + ex.Message);
            }
        }
        public async Task<DtoClientes> Update(DtoClientes clientesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarClientes";
                command.CommandType = CommandType.StoredProcedure;

                var idClienteParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = clientesDto.Id > 0 ? (object)clientesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idClienteParam);

                var nomeClientesParam = new MySqlParameter("@c_NombreCliente", MySqlDbType.VarChar, 100)
                {
                    Value = clientesDto.NombreCliente ?? (object)DBNull.Value,
                };
                command.Parameters.Add(nomeClientesParam);

                var direccionClientesParam = new MySqlParameter("@c_DireccionCliente", MySqlDbType.VarChar, 150)
                {
                    Value = clientesDto.DireccionCliente ?? (object)DBNull.Value,
                };
                command.Parameters.Add(direccionClientesParam);

                var emailParam = new MySqlParameter("@c_Email", MySqlDbType.VarChar, 100)
                {
                    Value = clientesDto.Email ?? (object)DBNull.Value,
                };
                command.Parameters.Add(emailParam);

                var celularParam = new MySqlParameter("@c_Celular", MySqlDbType.Int32)
                {
                    Value = clientesDto.Celular ?? (object)DBNull.Value,
                };
                command.Parameters.Add(celularParam);

                var activoParam = new MySqlParameter("@c_Activo", MySqlDbType.Bit)
                {
                    Value = clientesDto.Activo,
                };
                command.Parameters.Add(activoParam);

                var idTipoClienteParam = new MySqlParameter("@c_IdTipoCliente", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdTipoCliente,
                };
                command.Parameters.Add(idTipoClienteParam);

                var idCiudadParam = new MySqlParameter("@c_IdCiudad", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdCiudad,
                };
                command.Parameters.Add(idCiudadParam);

                var idEmpresaParam = new MySqlParameter("@c_IdEmpresa", MySqlDbType.Int32)
                {
                    Value = clientesDto.IdEmpresa,
                };
                command.Parameters.Add(idEmpresaParam);

                var observacionesParam = new MySqlParameter("@c_Observaciones", MySqlDbType.VarChar, 255)
                {
                    Value = clientesDto.Observaciones ?? (object)DBNull.Value,
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return clientesDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente: " + ex.Message);
            }
        }
        public async Task<bool> DeleteClientes(int id, int idEmpresa)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarClientes";
                command.CommandType = CommandType.StoredProcedure;

                // LIMPIAR siempre antes de agregar para evitar el error de "Already defined"
                command.Parameters.Clear();

                // 1. Entrada: ID Cliente
                command.Parameters.Add(new MySqlParameter("c_Id", MySqlDbType.Int32) { Value = id });

                // 2. Entrada: ID Empresa
                command.Parameters.Add(new MySqlParameter("c_IdEmpresa", MySqlDbType.Int32) { Value = idEmpresa });

                // 3. SALIDA: El nombre DEBE ser idéntico al del SP (sin el @ a veces es mejor para el mapeo)
                var resultParam = new MySqlParameter("c_Resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(resultParam);

                if (command.Connection.State != ConnectionState.Open) await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                // Aquí es donde daba el error. Ahora resultParam ya tiene el valor cargado.
                int result = (resultParam.Value != DBNull.Value) ? Convert.ToInt32(resultParam.Value) : 0;

                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el cliente: " + ex.Message);
            }
        }
        public async Task<List<DtoClientes>> GetClientes(int idEmpresa)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerClientes";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySqlParameter("c_IdEmpresa", idEmpresa));

                var clientes = new List<DtoClientes>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var cliente = new DtoClientes
                        {
                            Id = reader.GetInt32("Id"),
                            CodCliente = reader["CodCliente"] as string,
                            NombreCliente = reader["NombreCliente"] as string,
                            DireccionCliente = reader["DireccionCliente"] as string,
                            Email = reader["Email"] as string,
                            Celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? (int?)null : reader.GetInt32("Celular"),
                            Fecha_Registro = reader.GetDateTime("Fecha_Registro"),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion"),
                            Observaciones = reader["Observaciones"] as string,
                            IdTipoCliente = reader.GetInt32("IdTipoCliente"),
                            TipoCliente = reader["TipoCliente"] as string,
                            IdCiudad = reader.GetInt32("IdCiudad"),
                            Ciudad = reader["Ciudad"] as string,
                            IdEmpresa = reader.GetInt32("IdEmpresa"),
                            Empresa = reader["Empresa"] as string,
                            Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean("Activo"),
                            IsDeleted = reader.IsDBNull(reader.GetOrdinal("IsDeleted")) ? (bool?)null : reader.GetBoolean("IsDeleted")
                        };
                        clientes.Add(cliente);
                    }
                    await connection.CloseAsync();
                    return clientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los clientes: " + ex.Message);
            }
        }
        public async Task<DtoClientes> GetClientesById(int id, int idEmpresa)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "ObtenerClientesById";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@c_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var cliente = new DtoClientes
                        {
                            Id = reader.GetInt32("Id"),
                            CodCliente = reader["CodCliente"] as string,
                            NombreCliente = reader["NombreCliente"] as string,
                            DireccionCliente = reader["DireccionCliente"] as string,
                            Email = reader["Email"] as string,
                            Celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? (int?)null : reader.GetInt32("Celular"),
                            Fecha_Registro = reader.GetDateTime("Fecha_Registro"),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion"),
                            Observaciones = reader["Observaciones"] as string,
                            IdTipoCliente = reader.GetInt32("IdTipoCliente"),
                            TipoCliente = reader["TipoCliente"] as string,
                            IdCiudad = reader.GetInt32("IdCiudad"),
                            Ciudad = reader["Ciudad"] as string,
                            IdEmpresa = reader.GetInt32("IdEmpresa"),
                            Empresa = reader["Empresa"] as string,
                            Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean("Activo"),
                            IsDeleted = reader.IsDBNull(reader.GetOrdinal("IsDeleted")) ? (bool?)null : reader.GetBoolean("IsDeleted")
                        };
                        await connection.CloseAsync();
                        return cliente;
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
                throw new Exception("Error al obtener el cliente por ID: " + ex.Message);
            }
        }
        public async Task<bool> ClientesExists(int id)
        {
            return await _context.Clientes.AnyAsync(e => e.Id == id);
        }
    }
}
