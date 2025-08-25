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
                config.CreateMap<Paises, DtoPaises>();
                config.CreateMap<DtoPaises, Paises>();

                config.CreateMap<Regiones, DtoRegiones>();
                config.CreateMap<DtoRegiones, Regiones>();

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

                config.CreateMap<LoginMenu, DtoLoginMenu>();
                config.CreateMap<DtoLoginMenu, LoginMenu>();

                config.CreateMap<Acciones, DtoAcciones>();
                config.CreateMap<DtoAcciones, Acciones>();

                config.CreateMap<LoginAcciones, DtoLoginAcciones>();
                config.CreateMap<DtoLoginAcciones, LoginAcciones>();

                config.CreateMap<LoginAcciones, DtoLoginAcciones>();
                config.CreateMap<DtoLoginAcciones, LoginAcciones>();

                config.CreateMap <Empleados, DtoEmpleado>();
                config.CreateMap<DtoEmpleado, Empleados>();

                config.CreateMap<EmpleadoEsepecialidad, DtoEmpleadoEspecialidad>();
                config.CreateMap<DtoEmpleadoEspecialidad, EmpleadoEsepecialidad>();

                config.CreateMap<TipoTurno, DtoTipoTurno>();
                config.CreateMap<DtoTipoTurno, TipoTurno>();

                config.CreateMap<TurnosEmpleado, DtoTurnosEmpleado>();
                config.CreateMap<DtoTurnosEmpleado, TurnosEmpleado>();

                config.CreateMap<TipoAusencia, DtoTipoAusencia>();
                config.CreateMap<DtoTipoAusencia, TipoAusencia>();

                config.CreateMap<AusenciaEmpleado, DtoAusenciaEmpleado>();
                config.CreateMap<DtoAusenciaEmpleado, AusenciaEmpleado>();

                config.CreateMap<UsuarioRol, DtoUsuarioRol>();
                config.CreateMap<DtoUsuarioRol, UsuarioRol>();

                config.CreateMap<UsuarioSucursal, DtoUsuarioSucursal>();
                config.CreateMap<DtoUsuarioSucursal, UsuarioSucursal>();

                config.CreateMap<CriteriosEvaluacion, DtoCriterioEvaluacion>();
                config.CreateMap<DtoCriterioEvaluacion, CriteriosEvaluacion>();

                config.CreateMap<EvaluacionEmpleado, DtoEvaluacionEmpleado>();
                config.CreateMap<DtoEvaluacionEmpleado, EvaluacionEmpleado>();

                config.CreateMap<CursoCapacitacion, DtoCursoCapacitacion>();
                config.CreateMap<DtoCursoCapacitacion, CursoCapacitacion>();

                config.CreateMap<EmpleadoCapacitacion, DtoEmpleadoCapacitacion>();
                config.CreateMap<DtoEmpleadoCapacitacion, EmpleadoCapacitacion>();

                config.CreateMap<CategoriaActivoFijo, DtoCategoriaActivoFijo>();
                config.CreateMap<DtoCategoriaActivoFijo, CategoriaActivoFijo>();

                config.CreateMap<ActivosFijos, DtoActivoFijos>();
                config.CreateMap<DtoActivoFijos, ActivosFijos>();

                config.CreateMap<ConceptoNominas, DtoConceptoNominas>();
                config.CreateMap<DtoConceptoNominas, ConceptoNominas>();
            });
            return mappingConfig;
        }
    }
}
