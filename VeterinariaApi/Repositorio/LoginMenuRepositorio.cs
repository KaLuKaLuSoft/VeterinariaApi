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
    public class LoginMenuRepositorio : ILoginMenuRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public LoginMenuRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DtoLoginMenu> Create(DtoLoginMenu loginmenuDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.Transaction = transaction.GetDbTransaction();
                command.CommandText = "InsertarPermiso";
                command.CommandType = CommandType.StoredProcedure;

                // Cadena separada por comas
                var permisoIds = string.Join(",", loginmenuDto.LoginMenu);

                var loginIdParam = new MySqlParameter("@p_LoginId", MySqlDbType.Int32)
                {
                    Value = loginmenuDto.LoginId
                };
                command.Parameters.Add(loginIdParam);

                var permisoIdParam = new MySqlParameter("@p_PermisoId", MySqlDbType.Text)
                {
                    Value = permisoIds ?? (object)DBNull.Value
                };
                command.Parameters.Add(permisoIdParam);
            
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();

                return loginmenuDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error al crear LoginMenu", ex);
            }
        }

        public Task<bool> DeleteLoginMenu(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoLoginMenu>> GetLoginMenu()
        {
            throw new NotImplementedException();
        }

        public Task<DtoLoginMenu> GetLoginMenuById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginMenuExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoLoginMenu> Update(DtoLoginMenu loginmenuDto)
        {
            throw new NotImplementedException();
        }
    }
}
