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
    public class DueñosRepositorio : IDueñosRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DueñosRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoDueños> Create(DtoDueños dueñosDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarDueños";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("d_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var numeroIdentificacionParam = new MySqlParameter("d_NumeroIdentificacion", MySqlDbType.VarChar, 100)
                {
                    Value = dueñosDto.NumeroIdentificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(numeroIdentificacionParam);

                var nombreCompletoParam = new MySqlParameter("d_NombreCompleto", MySqlDbType.VarChar, 150)
                {
                    Value = dueñosDto.NombreCompleto ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCompletoParam);

                var celularParam = new MySqlParameter("d_Celular", MySqlDbType.Int32)
                {
                    Value = dueñosDto.Celular ?? (object)DBNull.Value
                };
                command.Parameters.Add(celularParam);

                var correoElectronicoParam = new MySqlParameter("d_CorreoElectronico", MySqlDbType.VarChar)
                {
                    Value = dueñosDto.CorreoElectronico ?? (object)DBNull.Value
                };
                command.Parameters.Add(correoElectronicoParam);

                var direccionParam = new MySqlParameter("d_Direccion", MySqlDbType.VarChar, 255)
                {
                    Value = dueñosDto.Direccion ?? (object)DBNull.Value
                };
                command.Parameters.Add(direccionParam);
                
                var idCiudadParam = new MySqlParameter("d_IdCiudad", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdCiudad
                };
                command.Parameters.Add(idCiudadParam);

                var idTipoDocumentoParam = new MySqlParameter("d_IdTipoDocumento", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdTipoDocumento
                };
                command.Parameters.Add(idTipoDocumentoParam);

                var idEmpresaParam = new MySqlParameter("d_IdEmpresa", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdEmpresa
                };
                command.Parameters.Add(idEmpresaParam);

                var activoParam = new MySqlParameter("d_Activo", MySqlDbType.Bit)
                {
                    Value = dueñosDto.Activo
                };
                command.Parameters.Add(activoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return dueñosDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el dueño. " + ex.Message);
            }
        }
        public async Task<DtoDueños> Update(DtoDueños dueñosDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarDueños";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("d_Id", MySqlDbType.Int32)
                {
                    Value = dueñosDto.Id > 0 ? (object)dueñosDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var numeroIdentificacionParam = new MySqlParameter("d_NumeroIdentificacion", MySqlDbType.VarChar)
                {
                    Value = dueñosDto.NumeroIdentificacion ?? (object)DBNull.Value
                };
                command.Parameters.Add(numeroIdentificacionParam);

                var nombreCompletoParam = new MySqlParameter("d_NombreCompleto", MySqlDbType.VarChar)
                {
                    Value = dueñosDto.NombreCompleto ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreCompletoParam);

                var celularParam = new MySqlParameter("d_Celular", MySqlDbType.Int32)
                {
                    Value = dueñosDto.Celular ?? (object)DBNull.Value
                };
                command.Parameters.Add(celularParam);

                var correoElectronicoParam = new MySqlParameter("d_CorreoElectronico", MySqlDbType.VarChar)
                {
                    Value = dueñosDto.CorreoElectronico ?? (object)DBNull.Value
                };
                command.Parameters.Add(correoElectronicoParam);

                var direccionParam = new MySqlParameter("d_Direccion", MySqlDbType.VarChar)
                {
                    Value = dueñosDto.Direccion ?? (object)DBNull.Value
                };
                command.Parameters.Add(direccionParam);

                var idCiudadParam = new MySqlParameter("d_IdCiudad", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdCiudad
                };
                command.Parameters.Add(idCiudadParam);

                var idTipoDocumentoParam = new MySqlParameter("d_IdTipoDocumento", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdTipoDocumento
                };
                command.Parameters.Add(idTipoDocumentoParam);

                var idEmpresaParam = new MySqlParameter("d_IdEmpresa", MySqlDbType.Int32)
                {
                    Value = dueñosDto.IdEmpresa
                };
                command.Parameters.Add(idEmpresaParam);

                var activoParam = new MySqlParameter("d_Activo", MySqlDbType.Bit)
                {
                    Value = dueñosDto.Activo
                };
                command.Parameters.Add(activoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return dueñosDto;
            }
            catch (Exception ex) 
            { 
                throw new Exception("Error al actualizar al Dueño. " + ex.Message); 
            }
        }

        public async Task<bool> DeleteDueños(int id, int idEmpresa)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarDueños";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Clear();

                command.Parameters.Add(new MySqlParameter("@d_Id", MySqlDbType.Int32) { Value = id });

                command.Parameters.Add(new MySqlParameter("@d_IdEmpresa", MySqlDbType.Int32) { Value = idEmpresa });

                var resultParam = new MySqlParameter("d_resultado", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(resultParam);

                if(command.Connection.State != ConnectionState.Open) await command.Connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                int result = (resultParam.Value != DBNull.Value) ? Convert.ToInt32(resultParam.Value) : 0;

                return result == 1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar al Dueño. " + ex.Message);
            }
        }

        public async Task<List<DtoDueños>> GetDueños(int idEmpresa)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerDueños";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySqlParameter("d_IdEmpresa", idEmpresa));

                var dueños = new List<DtoDueños>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var dueño = new DtoDueños
                        {
                            Id = reader.GetInt32("Id"),
                            CodDueños = reader.IsDBNull(reader.GetOrdinal("CodDueños")) ? null : reader.GetString("CodDueños"),
                            NumeroIdentificacion = reader.IsDBNull(reader.GetOrdinal("NumeroIdentificacion")) ? null : reader.GetString("NumeroIdentificacion"),
                            NombreCompleto = reader.GetString("NombreCompleto"),
                            Celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? null : reader.GetInt32("Celular"),
                            CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? null : reader.GetString("CorreoElectronico"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString("Direccion"),
                            IdCiudad = reader.GetInt32("IdCiudad"),
                            NombreCiudad = reader.IsDBNull(reader.GetOrdinal("NombreCiudad")) ? null : reader.GetString("NombreCiudad"),
                            IdTipoDocumento = reader.GetInt32("IdTipoDocumento"),
                            TipoDocumento = reader.IsDBNull(reader.GetOrdinal("TipoDocumento")) ? null : reader.GetString("TipoDocumento"),
                            IdEmpresa = reader.GetInt32("IdEmpresa"),
                            Empresa = reader.IsDBNull(reader.GetOrdinal("Empresa")) ? null : reader.GetString("Empresa"),
                            Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean("Activo"),
                            IsDeleted = reader.IsDBNull(reader.GetOrdinal("IsDeleted")) ? (bool?)null : reader.GetBoolean("IsDeleted"),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        dueños.Add(dueño);
                    }
                    await connection.CloseAsync();
                    return dueños;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Dueños. " + ex.Message);
            }
        }

        public async Task<DtoDueños> GetDueñosById(int id, int idEmpresa)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerDueñosById";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("d_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var dueño = new DtoDueños
                        {
                            Id = reader.GetInt32("Id"),
                            CodDueños = reader.IsDBNull(reader.GetOrdinal("CodDueños")) ? null : reader.GetString("CodDueños"),
                            NumeroIdentificacion = reader.IsDBNull(reader.GetOrdinal("NumeroIdentificacion")) ? null : reader.GetString("NumeroIdentificacion"),
                            NombreCompleto = reader.GetString("NombreCompleto"),
                            Celular = reader.IsDBNull(reader.GetOrdinal("Celular")) ? null : reader.GetInt32("Celular"),
                            CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? null : reader.GetString("CorreoElectronico"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString("Direccion"),
                            IdCiudad = reader.GetInt32("IdCiudad"),
                            NombreCiudad = reader.IsDBNull(reader.GetOrdinal("NombreCiudad")) ? null : reader.GetString("NombreCiudad"),
                            IdTipoDocumento = reader.GetInt32("IdTipoDocumento"),
                            TipoDocumento = reader.IsDBNull(reader.GetOrdinal("TipoDocumento")) ? null : reader.GetString("TipoDocumento"),
                            IdEmpresa = reader.GetInt32("IdEmpresa"),
                            Empresa = reader.IsDBNull(reader.GetOrdinal("Empresa")) ? null : reader.GetString("Empresa"),
                            Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? (bool?)null : reader.GetBoolean("Activo"),
                            IsDeleted = reader.IsDBNull(reader.GetOrdinal("IsDeleted")) ? (bool?)null : reader.GetBoolean("IsDeleted"),
                            Fecha_Alta = reader.IsDBNull(reader.GetOrdinal("Fecha_Alta")) ? null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Modificacion")) ? null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        await connection.CloseAsync();
                        return dueño;
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
                throw new Exception("Error al obtener el Dueño por ID. " + ex.Message);
            }
        }

        public async Task<bool> DueñosExists(int id)
        {
            return await _context.Dueños.AnyAsync(d => d.Id == id);
        }
    }
}
