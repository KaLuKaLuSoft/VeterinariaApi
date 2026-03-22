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
    public class TipoDocumentoRepositorio : ITipoDocumentoRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoDocumentoRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoTipoDocumento> Create(DtoTipoDocumento tipodocumentoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoDocumento";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@td_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var tipodocumentoParam = new MySqlParameter("@td_TipoDocumento", MySqlDbType.VarChar, 100)
                {
                    Value = tipodocumentoDto.TipoDocumento ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipodocumentoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipodocumentoDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Errror al crear Tipo Documento", ex);
            }
        }
        public async Task<DtoTipoDocumento> Update(DtoTipoDocumento tipodocumentoDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarTipoDocumento";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@td_Id", MySqlDbType.Int32)
                {
                    Value = tipodocumentoDto.Id > 0 ? (object)tipodocumentoDto.Id : (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var tipodocumentoParam = new MySqlParameter("@td_TipoDocumento", MySqlDbType.VarChar, 100)
                {
                    Value = tipodocumentoDto.TipoDocumento ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipodocumentoParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return tipodocumentoDto;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al Actualizar Tipo Documento", ex);
            }
        }

        public async Task<bool> DeleteTipoDocumento(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarTipoDocumento";
                command.CommandType = CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@td_Id", MySqlDbType.Int32)
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
                return result == 0;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al eliminar Tipo Documento", ex);
            }
        }

        public async Task<List<DtoTipoDocumento>> GetTipoDocumento()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoDocumento";
                command.CommandType = CommandType.StoredProcedure;

                var tipoDocumento = new List<DtoTipoDocumento>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var td = new DtoTipoDocumento
                        {
                            Id = reader.GetInt32(0),
                            TipoDocumento = reader.GetString(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        };
                        tipoDocumento.Add(td);
                    }
                    await reader.CloseAsync();
                    return tipoDocumento;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Tipo Documento", ex);
            }
        }

        public async Task<DtoTipoDocumento> GetTipoDocumentoById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerTipoDocumentoPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@td_id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var td = new DtoTipoDocumento
                        {
                            Id = reader.GetInt32(0),
                            TipoDocumento = reader.GetString(1),
                            Fecha_Alta = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            Fecha_Modificacion = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                        };
                        await connection.CloseAsync();
                        return td;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Tipo Documento.", ex);
            }
        }

        public async Task<bool> TipoDocumentoExists(int id)
        {
            return await _context.TipoDocumentos.AnyAsync(td => td.Id == id);
        }
    }
}
