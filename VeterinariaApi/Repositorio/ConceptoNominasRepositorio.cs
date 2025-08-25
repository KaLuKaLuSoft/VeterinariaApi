using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Repositorio
{
    public class ConceptoNominasRepositorio : IConceptoNominasRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        public ConceptoNominasRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoConceptoNominas> Create(DtoConceptoNominas conceptoNominaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarConceptoNomina";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cn_Id", MySqlDbType.Int32)
                {
                    Value = (object)DBNull.Value
                };
                command.Parameters.Add(idParam);

                var nombreNominaParam = new MySqlParameter("@cn_NombreNomina", MySqlDbType.VarChar, 100)
                {
                    Value = conceptoNominaDto.NombreNomina ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreNominaParam);

                var tipoConceptoParam = new MySqlParameter("@cn_TipoConcepto", MySqlDbType.VarChar, 100)
                {
                    Value = conceptoNominaDto.TipoConcepto ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipoConceptoParam);

                var esFijoParam = new MySqlParameter("@cn_EsFijo", MySqlDbType.Bit)
                {
                    Value = conceptoNominaDto.EsFijo.HasValue ? (object)conceptoNominaDto.EsFijo.Value : DBNull.Value
                };
                command.Parameters.Add(esFijoParam);

                var descripcionParam = new MySqlParameter("@cn_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = conceptoNominaDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return conceptoNominaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear el concepto de nómina", ex);
            }
        }
        public async Task<DtoConceptoNominas> Update(DtoConceptoNominas conceptoNominaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarConceptoNomina";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new MySqlParameter("@cn_Id", MySqlDbType.Int32)
                {
                    Value = conceptoNominaDto.Id
                };
                command.Parameters.Add(idParam);

                var nombreNominaParam = new MySqlParameter("@cn_NombreNomina", MySqlDbType.VarChar, 100)
                {
                    Value = conceptoNominaDto.NombreNomina ?? (object)DBNull.Value
                };
                command.Parameters.Add(nombreNominaParam);

                var tipoConceptoParam = new MySqlParameter("@cn_TipoConcepto", MySqlDbType.VarChar, 100)
                {
                    Value = conceptoNominaDto.TipoConcepto ?? (object)DBNull.Value
                };
                command.Parameters.Add(tipoConceptoParam);

                var esFijoParam = new MySqlParameter("@cn_EsFijo", MySqlDbType.Bit)
                {
                    Value = conceptoNominaDto.EsFijo.HasValue ? (object)conceptoNominaDto.EsFijo.Value : DBNull.Value
                };
                command.Parameters.Add(esFijoParam);

                var descripcionParam = new MySqlParameter("@cn_Descripcion", MySqlDbType.VarChar, 255)
                {
                    Value = conceptoNominaDto.Descripcion ?? (object)DBNull.Value
                };
                command.Parameters.Add(descripcionParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
                return conceptoNominaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al actualizar el concepto de nómina", ex);
            }
        }
        public async Task<bool> DeleteConceptoNomina(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "EliminarConceptoNomina";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@cn_Id", MySqlDbType.Int32)
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
                throw new Exception("Error al eliminar el concepto de nómina", ex);
            }
        }
        public async Task<List<DtoConceptoNominas>> GetConceptosNominas()
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerConceptosNominas";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var conceptosNominas = new List<DtoConceptoNominas>();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var conceptoNomina = new DtoConceptoNominas
                        {
                            Id = reader.GetInt32("Id"),
                            NombreNomina = reader.IsDBNull("NombreNomina") ? null : reader.GetString("NombreNomina"),
                            TipoConcepto = reader.IsDBNull("TipoConcepto") ? null : reader.GetString("TipoConcepto"),
                            EsFijo = reader.IsDBNull("EsFijo") ? (bool?)null : reader.GetBoolean("EsFijo"),
                            Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        conceptosNominas.Add(conceptoNomina);
                    }
                    await reader.CloseAsync();
                    return conceptosNominas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los conceptos de nómina", ex);
            }
        }
        public async Task<DtoConceptoNominas> GetConceptosNominasById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerConceptoNominaPorId";
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new MySqlParameter("@cn_Id", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(idParam);

                using var reader = await command.ExecuteReaderAsync();
                {
                    if (await reader.ReadAsync())
                    {
                        var conceptoNomina = new DtoConceptoNominas
                        {
                            Id = reader.GetInt32("Id"),
                            NombreNomina = reader.IsDBNull("NombreNomina") ? null : reader.GetString("NombreNomina"),
                            TipoConcepto = reader.IsDBNull("TipoConcepto") ? null : reader.GetString("TipoConcepto"),
                            EsFijo = reader.IsDBNull("EsFijo") ? (bool?)null : reader.GetBoolean("EsFijo"),
                            Descripcion = reader.IsDBNull("Descripcion") ? null : reader.GetString("Descripcion"),
                            Fecha_Alta = reader.IsDBNull("Fecha_Alta") ? (DateTime?)null : reader.GetDateTime("Fecha_Alta"),
                            Fecha_Modificacion = reader.IsDBNull("Fecha_Modificacion") ? (DateTime?)null : reader.GetDateTime("Fecha_Modificacion")
                        };
                        await reader.CloseAsync();
                        return conceptoNomina;
                    }
                    await connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el concepto de nómina. ", ex);
            }
        }
        public async Task<bool> ConceptoNominaExists(int id)
        {
            return await _context.ConceptoNominas.AnyAsync(e => e.Id == id);
        }
    }
}
