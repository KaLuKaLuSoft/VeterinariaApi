using AutoMapper;
using VeterinariaApi.Dto;
using VeterinariaApi.Models;

namespace VeterinariaApi
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Paises, DtoPaises>(); // Define the mapping inside the constructor
                config.CreateMap<DtoPaises, Paises>(); // Reverse mapping

                config.CreateMap<Regiones, DtoRegiones>(); // Define the mapping for Regiones
                config.CreateMap<DtoRegiones, Regiones>(); // Reverse mapping

                config.CreateMap<Ciudad, DtoCiudad>();
                config.CreateMap<DtoCiudad, Ciudad>();

                config.CreateMap<Sucursales, DtoSucursales>();
                config.CreateMap<DtoSucursales, Sucursales>();

                config.CreateMap<Departamentos, DtoDepartamentos>();
                config.CreateMap<DtoDepartamentos, Departamentos>();

                config.CreateMap<Roles, DtoRoles>();
                config.CreateMap<DtoRoles, Roles>();

                config.CreateMap<EspecialidadesMedicas, DtoEpecialidadesMedicas>();
                config.CreateMap<DtoEpecialidadesMedicas, EspecialidadesMedicas>();

                config.CreateMap<Modulo, DtoModulo>();
                config.CreateMap<DtoModulo, Modulo>();

                config.CreateMap<SubModulo, DtoSubModulo>();
                config.CreateMap<DtoSubModulo, SubModulo>();

                config.CreateMap<Login, DtoLogin>();
                config.CreateMap<DtoLogin, Login>();
            });
            return mappingConfig;
        }
    }
}
