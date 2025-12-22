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
                var permisoIds = string.Join(",", loginmenuDto.MenuId);

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

        public async Task<DtoLoginMenu> GetLoginMenuById(int id)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "ObtenerLoginMenuById";
                command.CommandType = CommandType.StoredProcedure;

                var loginIdParam = new MySqlParameter("@p_LoginId", MySqlDbType.Int32)
                {
                    Value = id
                };
                command.Parameters.Add(loginIdParam);

                var loginMenuDto = new DtoLoginMenu
                {
                    LoginId = id,
                    MenuId = new List<int>()
                };

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    loginMenuDto.MenuId.Add(reader.GetInt32(1));
                }

                await connection.CloseAsync();

                return loginMenuDto.MenuId.Count > 0 ? loginMenuDto : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los menús del usuario ", ex);
            }
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
