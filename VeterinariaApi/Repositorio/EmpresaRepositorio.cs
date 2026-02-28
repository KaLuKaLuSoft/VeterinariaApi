using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using System.Runtime.InteropServices;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmpresaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoEmpresa> Create(DtoEmpresa empresaDto)
        {
            // Usamos la conexión del DbContext
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var connection = _context.Database.GetDbConnection();
                var command = connection.CreateCommand();

                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarActualizarEmpresa";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Parámetros basados en tu DTO y los nombres del Procedimiento Almacenado
                // ID 0 indica que el SP hará un INSERT
                command.Parameters.Add(new MySqlParameter("@e_Id", MySqlDbType.Int32) { Value = 0 });

                command.Parameters.Add(new MySqlParameter("@e_NombreComercial", MySqlDbType.VarChar, 150)
                { Value = empresaDto.NombreComercial ?? (object)DBNull.Value });

                command.Parameters.Add(new MySqlParameter("@e_RazonSocial", MySqlDbType.VarChar, 150)
                { Value = empresaDto.RazonSocial ?? (object)DBNull.Value });

                command.Parameters.Add(new MySqlParameter("@e_NumeroTrabajadores", MySqlDbType.Int32)
                { Value = empresaDto.NumeroTrabajadores ?? (object)DBNull.Value });

                command.Parameters.Add(new MySqlParameter("@e_Nit", MySqlDbType.VarChar, 50)
                { Value = empresaDto.Nit ?? (object)DBNull.Value });

                // Aquí llega la ruta que generaremos en el controlador (ej: /uploads/empresas/temp/logo_123.png)
                command.Parameters.Add(new MySqlParameter("@e_LogoUrl", MySqlDbType.VarChar, 500)
                { Value = empresaDto.LogoUrl ?? (object)DBNull.Value });

                command.Parameters.Add(new MySqlParameter("@e_IdPais", MySqlDbType.Int32)
                { Value = empresaDto.IdPais });

                // Enums convertidos a string según tu configuración de Fluent API
                command.Parameters.Add(new MySqlParameter("@e_PlanSuscripcion", MySqlDbType.VarChar, 50)
                { Value = empresaDto.PlanSuscripcion.ToString() });

                command.Parameters.Add(new MySqlParameter("@e_EstadoCuenta", MySqlDbType.VarChar, 50)
                { Value = empresaDto.EstadoCuenta.ToString() });

                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                // Ejecutamos y capturamos el ID generado por la DB
                var result = await command.ExecuteScalarAsync();

                if (result != null)
                {
                    empresaDto.Id = Convert.ToInt32(result);
                }

                await transaction.CommitAsync();
                return empresaDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error crítico al persistir la empresa.", ex);
            }
        }

        public async Task<DtoEmpresa> Update(DtoEmpresa empresaDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEmpresa(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EmpresaExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoEmpresa> GetEmpresaById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DtoEmpresa>> GetEmpresas()
        {
            throw new NotImplementedException();
        }

    }
}
