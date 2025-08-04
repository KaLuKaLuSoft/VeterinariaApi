using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class ActivosFijosRepositorio : IActivosFijosRepositorio
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public ActivosFijosRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoActivoFijos> Create(DtoActivoFijos activosFijosDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarActivosFijos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@af_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreActivoParam = new MySqlConnector.MySqlParameter("@af_NombreActivo", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = activosFijosDto.NombreActivo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreActivoParam);

                var numeroSerieParam = new MySqlConnector.MySqlParameter("@af_NumeroSerie", MySqlConnector.MySqlDbType.VarChar, 50)
                {
                    Value = activosFijosDto.NumeroSerie ?? (object)DBNull.Value
                };
                command.Parameters.Add(numeroSerieParam);

                var categoriaActivoIdParam = new MySqlConnector.MySqlParameter("@af_CategoriaActivoId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.CategoriaActivoId
                };
                command.Parameters.Add(categoriaActivoIdParam);

                var sucursalIdParam = new MySqlConnector.MySqlParameter("@af_SucursalId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.SucursalId
                };

                var fechaAdquisicionParam = new MySqlConnector.MySqlParameter("@af_FechaAdquisicion", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = activosFijosDto.FechaAdquisicion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaAdquisicionParam);

                var costoAdquisicionParam = new MySqlConnector.MySqlParameter("@af_CostoAdquisicion", MySqlConnector.MySqlDbType.Decimal)
                {
                    Value = activosFijosDto.CostoAdquisicion ?? (object)DBNull.Value
                };
                command.Parameters.Add(costoAdquisicionParam);

                var vidaUtilParam = new MySqlConnector.MySqlParameter("@af_VidaUtil", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.VidaUtil ?? (object)DBNull.Value
                };
                command.Parameters.Add(vidaUtilParam);

                var ubicacionFisicaParam = new MySqlConnector.MySqlParameter("@af_UbicacionFisica", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = activosFijosDto.UbicacionFisica ?? (object)DBNull.Value
                };
                command.Parameters.Add(ubicacionFisicaParam);

                var estadoParam = new MySqlConnector.MySqlParameter("@af_Estado", MySqlConnector.MySqlDbType.VarChar, 50)
                {
                    Value = activosFijosDto.Estado ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoParam);

                var observacionesParam = new MySqlConnector.MySqlParameter("@af_Observaciones", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = activosFijosDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return activosFijosDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el activo fijo", ex);
            }
        }
        public async Task<DtoActivoFijos> Update(DtoActivoFijos activosFijosDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarActivosFijos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@af_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.Id > 0 ? (object)activosFijosDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreActivoParam = new MySqlConnector.MySqlParameter("@af_NombreActivo", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = activosFijosDto.NombreActivo ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreActivoParam);

                var numeroSerieParam = new MySqlConnector.MySqlParameter("@af_NumeroSerie", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = activosFijosDto.NumeroSerie ?? (object)DBNull.Value
                };
                command.Parameters.Add(numeroSerieParam);

                var categoriaActivoIdParam = new MySqlConnector.MySqlParameter("@af_CategoriaActivoId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.CategoriaActivoId
                };
                command.Parameters.Add(categoriaActivoIdParam);

                var sucursalIdParam = new MySqlConnector.MySqlParameter("@af_SucursalId", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.SucursalId
                };
                command.Parameters.Add(sucursalIdParam);

                var fechaAdquisicionParam = new MySqlConnector.MySqlParameter("@af_FechaAdquisicion", MySqlConnector.MySqlDbType.DateTime)
                {
                    Value = activosFijosDto.FechaAdquisicion ?? (object)DBNull.Value
                };
                command.Parameters.Add(fechaAdquisicionParam);

                var costoAdquisicionParam = new MySqlConnector.MySqlParameter("@af_CostoAdquisicion", MySqlConnector.MySqlDbType.Decimal)
                {
                    Value = activosFijosDto.CostoAdquisicion ?? (object)DBNull.Value
                };
                command.Parameters.Add(costoAdquisicionParam);

                var vidaUtilParam = new MySqlConnector.MySqlParameter("@af_VidaUtil", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = activosFijosDto.VidaUtil ?? (object)DBNull.Value
                };
                command.Parameters.Add(vidaUtilParam);

                var ubicacionFisicaParam = new MySqlConnector.MySqlParameter("@af_UbicacionFisica", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = activosFijosDto.UbicacionFisica ?? (object)DBNull.Value
                };
                command.Parameters.Add(ubicacionFisicaParam);

                var estadoParam = new MySqlConnector.MySqlParameter("@af_Estado", MySqlConnector.MySqlDbType.VarChar, 100)
                {
                    Value = activosFijosDto.Estado ?? (object)DBNull.Value
                };
                command.Parameters.Add(estadoParam);

                var observacionesParam = new MySqlConnector.MySqlParameter("@af_Observaciones", MySqlConnector.MySqlDbType.VarChar, 255)
                {
                    Value = activosFijosDto.Observaciones ?? (object)DBNull.Value
                };
                command.Parameters.Add(observacionesParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return activosFijosDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el activo fijo", ex);
            }
        }
        public async Task<bool> DeleteActivosFijos(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarActivosFijos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@af_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };

                var resultadoParam = new MySqlConnector.MySqlParameter("@resultado", MySqlConnector.MySqlDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParam);
                command.Parameters.Add(resultadoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                var result = Convert.ToInt32(resultadoParam.Value);
                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar el activo fijo", ex);
            }
        }
        public async Task<List<DtoActivoFijos>> GetActivosFijos()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerActivosFijos";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var activofijos = new List<DtoActivoFijos>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var activofijo = new DtoActivoFijos
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreActivo = reader.IsDBNull(reader.GetOrdinal("NombreActivo")) ? null : reader.GetString(reader.GetOrdinal("NombreActivo")),
                            NumeroSerie = reader.IsDBNull(reader.GetOrdinal("NumeroSerie")) ? null : reader.GetString(reader.GetOrdinal("NumeroSerie")),
                            CategoriaActivoId = reader.GetInt32(reader.GetOrdinal("CategoriaActivoId")),
                            CategoriaActivoFijo = reader.IsDBNull(reader.GetOrdinal("CategoriaActivoFijo")) ? null : reader.GetString(reader.GetOrdinal("CategoriaActivoFijo")),
                            SucursalId = reader.GetInt32(reader.GetOrdinal("SucursalId")),
                            Sucursal = reader.IsDBNull(reader.GetOrdinal("Sucursal")) ? null : reader.GetString(reader.GetOrdinal("Sucursal")),
                            FechaAdquisicion = reader.IsDBNull(reader.GetOrdinal("FechaAdquisicion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaAdquisicion")),
                            CostoAdquisicion = reader.IsDBNull(reader.GetOrdinal("CostoAdquisicion")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("CostoAdquisicion")),
                            VidaUtil = reader.IsDBNull(reader.GetOrdinal("VidaUtil")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VidaUtil")),
                            UbicacionFisica = reader.IsDBNull(reader.GetOrdinal("UbicacionFisica")) ? null : reader.GetString(reader.GetOrdinal("UbicacionFisica")),
                            Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? null : reader.GetString(reader.GetOrdinal("Estado")),
                            Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        activofijos.Add(activofijo);
                    }
                    await reader.CloseAsync();
                    return activofijos;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los activos fijos", ex);
            }
        }
        public async Task<DtoActivoFijos> GetActivosFijosById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerActivosFijosPorId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlConnector.MySqlParameter("@af_Id", MySqlConnector.MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var activoFijoDto = new DtoActivoFijos
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreActivo = reader.IsDBNull(reader.GetOrdinal("NombreActivo")) ? null : reader.GetString(reader.GetOrdinal("NombreActivo")),
                            NumeroSerie = reader.IsDBNull(reader.GetOrdinal("NumeroSerie")) ? null : reader.GetString(reader.GetOrdinal("NumeroSerie")),
                            CategoriaActivoId = reader.GetInt32(reader.GetOrdinal("CategoriaActivoId")),
                            CategoriaActivoFijo = reader.IsDBNull(reader.GetOrdinal("CategoriaActivoFijo")) ? null : reader.GetString(reader.GetOrdinal("CategoriaActivoFijo")),
                            SucursalId = reader.GetInt32(reader.GetOrdinal("SucursalId")),
                            Sucursal = reader.IsDBNull(reader.GetOrdinal("Sucursal")) ? null : reader.GetString(reader.GetOrdinal("Sucursal")),
                            FechaAdquisicion = reader.IsDBNull(reader.GetOrdinal("FechaAdquisicion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("FechaAdquisicion")),
                            CostoAdquisicion = reader.IsDBNull(reader.GetOrdinal("CostoAdquisicion")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("CostoAdquisicion")),
                            VidaUtil = reader.IsDBNull(reader.GetOrdinal("VidaUtil")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VidaUtil")),
                            UbicacionFisica = reader.IsDBNull(reader.GetOrdinal("UbicacionFisica")) ? null : reader.GetString(reader.GetOrdinal("UbicacionFisica")),
                            Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? null : reader.GetString(reader.GetOrdinal("Estado")),
                            Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Alta")),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Fecha_Modificacion"))
                        };
                        await connection.CloseAsync();
                        return activoFijoDto;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el activo fijo por ID", ex);
            }
        }
        public async Task<bool> ActivosFijosExists(int id)
        {
            return await _context.ActivosFijos.AnyAsync(a => a.Id == id);
        }
    }
}
