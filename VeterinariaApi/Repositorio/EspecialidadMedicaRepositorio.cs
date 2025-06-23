using AutoMapper;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class EspecialidadMedicaRepositorio : IEspecialidadMedicaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EspecialidadMedicaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<DtoEpecialidadesMedicas> Create(DtoEpecialidadesMedicas especialidadesDto)
        {
            throw new NotImplementedException();
        }

        public Task<DtoEpecialidadesMedicas> Update(DtoEpecialidadesMedicas especialidadesDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEspecialidades(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EspecialidadesExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoEpecialidadesMedicas>> GetEspecialidades()
        {
            throw new NotImplementedException();
        }

        public Task<DtoEpecialidadesMedicas> GetEspecialidadesById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
