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
    public class SucursalesRepositorio : ISucursalesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public SucursalesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoSucursales> Create(DtoSucursales sucursalesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarSucursal";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                var idParam = new MySqlParameter("@s_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreSucursalParam = new MySqlParameter("@s_NombreSucursal", MySqlDbType.VarChar, 100)
                {
                    Value = sucursalesDto.NombreSucursal ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreSucursalParam);

                var direccionParam = new MySqlParameter("@s_Direccion", MySqlDbType.VarChar, 200)
                {
                    Value = sucursalesDto.Direccion ?? (object)DBNull.Value
                };
                command.Parameters.Add(direccionParam);

                var idCiudadParam = new MySqlParameter("@s_IdCiudad", MySqlDbType.Int32)
                {
                    Value = sucursalesDto.IdCiudad
                };
                command.Parameters.Add(idCiudadParam);

                var telefonoParam = new MySqlParameter("@s_Telefono", MySqlDbType.VarChar, 15)
                {
                    Value = sucursalesDto.Telefono ?? (object)DBNull.Value
                };
                command.Parameters.Add(telefonoParam);

                var emailContactoParam = new MySqlParameter("@s_EmailContacto", MySqlDbType.VarChar, 100)
                {
                    Value = sucursalesDto.EmailContacto ?? (object)DBNull.Value
                };
                command.Parameters.Add(emailContactoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return sucursalesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear la sucursal", ex);
            }
        }

        public async Task<DtoSucursales> Update(DtoSucursales sucursalesDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarSucursal";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@s_Id", MySqlDbType.Int32)
                {
                    Value = sucursalesDto.Id > 0 ? (object)sucursalesDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreSucursalParam = new MySqlParameter("@s_NombreSucursal", MySqlDbType.VarChar, 100)
                {
                    Value = sucursalesDto.NombreSucursal ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreSucursalParam);

                var direccionParam = new MySqlParameter("@s_Direccion", MySqlDbType.VarChar, 200)
                {
                    Value = sucursalesDto.Direccion ?? (object)DBNull.Value
                };
                command.Parameters.Add(direccionParam);

                var idCiudadParam = new MySqlParameter("@s_IdCiudad", MySqlDbType.Int32)
                {
                    Value = sucursalesDto.IdCiudad
                };
                command.Parameters.Add(idCiudadParam);

                var telefonoParam = new MySqlParameter("@s_Telefono", MySqlDbType.VarChar, 15)
                {
                    Value = sucursalesDto.Telefono ?? (object)DBNull.Value
                };
                command.Parameters.Add(telefonoParam);

                var emailContactoParam = new MySqlParameter("@s_EmailContacto", MySqlDbType.VarChar, 100)
                {
                    Value = sucursalesDto.EmailContacto ?? (object)DBNull.Value
                };
                command.Parameters.Add(emailContactoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();              
                return sucursalesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar la sucursal", ex);
            }
        }

        public async Task<bool> DeleteSucursales(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@s_id", MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultParam = new MySqlParameter("@resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
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
                transaction.Rollback();
                throw new Exception("Error al eliminar la sucursal", ex);
            }
        }

        public async Task<List<DtoSucursales>> GetSucursales()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerSucursal";
                command.CommandType = CommandType.StoredProcedure;

                var sucursales = new List<DtoSucursales>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var sucursalDto = new DtoSucursales
                        {
                            Id = reader.GetInt32(0),
                            NombreSucursal = reader.GetString(1),
                            Direccion = reader.GetString(2), // Ahora es Direccion
                            IdCiudad = reader.GetInt32(3), // Ahora es IdCiudad
                            NombreCiudad = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Telefono = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmailContacto = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Fecha_Alta = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            Fecha_Modificacion = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8)
                        };
                        sucursales.Add(sucursalDto);
                    }
                    await connection.CloseAsync();
                    return sucursales;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las sucursales", ex);
            }
        }

        public async Task<DtoSucursales> GetSucursalesById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerSucursalPorId";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@s_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var sucursalDto = new DtoSucursales
                    {
                        Id = reader.GetInt32(0),
                        NombreSucursal = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Direccion = null, // Asignamos null porque el SP no la devuelve
                        IdCiudad = reader.GetInt32(2), // IdCiudad ahora en el índice 2 (era 3)
                        NombreCiudad = reader.IsDBNull(3) ? null : reader.GetString(3), // NombreCiudad ahora en el índice 3 (era 4)
                        Telefono = reader.IsDBNull(4) ? null : reader.GetString(4), // Telefono ahora en el índice 4 (era 5)
                        EmailContacto = reader.IsDBNull(5) ? null : reader.GetString(5), // EmailContacto ahora en el índice 5 (era 6)
                        Fecha_Alta = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6), // Fecha_Alta ahora en el índice 6 (era 7)
                        Fecha_Modificacion = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7) // Fecha_Modificacion ahora en el índice 7 (era 8)
                    };
                    await connection.CloseAsync();
                    return sucursalDto;
                }
                await connection.CloseAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la sucursal por ID", ex);
            }
        }

        public async Task<bool> SucursalesExists(int id)
        {
            return await _context.Sucursales.AnyAsync(s => s.Id == id);
        }
    }
}
