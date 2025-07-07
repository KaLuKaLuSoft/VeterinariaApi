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
    public class LoginAccionesRepositorio : ILoginAccionesRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public LoginAccionesRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DtoLoginAcciones> Create(DtoLoginAcciones loginaccionesDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarReglas";
                command.CommandType = CommandType.StoredProcedure;

                // Cadena separada por comas
                var permisoIds = string.Join(",", loginaccionesDto.LoginAccion);

                var loginIdParam = new MySqlParameter("@p_LoginId", MySqlDbType.Int32)
                {
                    Value = loginaccionesDto.LoginId
                };
                command.Parameters.Add(loginIdParam);

                var permisoIdParam = new MySqlParameter("@p_PermisoId", MySqlDbType.Text)
                {
                    Value = permisoIds ?? (object)DBNull.Value
                };
                command.Parameters.Add(permisoIdParam);

                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return loginaccionesDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear LoginAccioón", ex);
            }
        }

        public Task<bool> DeleteLoginAcciones(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoLoginAcciones>> GetLoginAcciones()
        {
            throw new NotImplementedException();
        }

        public Task<DtoLoginAcciones> GetLoginAccionesById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginAccionesExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoLoginAcciones> Update(DtoLoginAcciones loginaccionesDto)
        {
            throw new NotImplementedException();
        }
    }
}
