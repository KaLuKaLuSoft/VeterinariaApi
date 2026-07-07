using AutoMapper;
using VeterinariaApi.Dto;
using VeterinariaApi.Migrations;
using VeterinariaApi.Models;
using VeterinariaApi.Repositorio;

namespace VeterinariaApi
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Paises, DtoPaises>().ReverseMap();
            CreateMap<Regiones, DtoRegiones>().ReverseMap();
            CreateMap<Ciudad, DtoCiudad>().ReverseMap();
            CreateMap<Sucursales, DtoSucursales>().ReverseMap();
            CreateMap<Departamentos, DtoDepartamentos>().ReverseMap();
            CreateMap<Roles, DtoRoles>().ReverseMap();
            CreateMap<EspecialidadesMedicas, DtoEpecialidadesMedicas>().ReverseMap();
            CreateMap<Modulo, DtoModulo>().ReverseMap();
            CreateMap<SubModulo, DtoSubModulo>().ReverseMap();
            CreateMap<Login, DtoLogin>().ReverseMap();
            CreateMap<Login, DtoLogueo>().ReverseMap();
            CreateMap<LoginMenu, DtoLoginMenu>().ReverseMap();
            CreateMap<Acciones, DtoAcciones>().ReverseMap();
            CreateMap<LoginAcciones, DtoLoginAcciones>().ReverseMap();
            CreateMap<LoginAcciones, DtoLoginAcciones>().ReverseMap();
            CreateMap<Empleados, DtoEmpleado>().ReverseMap();
            CreateMap<EmpleadoEsepecialidad, DtoEmpleadoEspecialidad>().ReverseMap();
            CreateMap<TipoTurno, DtoTipoTurno>().ReverseMap();
            CreateMap<TurnosEmpleado, DtoTurnosEmpleado>().ReverseMap();
            CreateMap<TipoAusencia, DtoTipoAusencia>().ReverseMap();
            CreateMap<AusenciaEmpleado, DtoAusenciaEmpleado>().ReverseMap();
            CreateMap<UsuarioRol, DtoUsuarioRol>().ReverseMap();
            CreateMap<UsuarioSucursal, DtoUsuarioSucursal>().ReverseMap();
            CreateMap<CriteriosEvaluacion, DtoCriterioEvaluacion>().ReverseMap();
            CreateMap<EvaluacionEmpleado, DtoEvaluacionEmpleado>().ReverseMap();
            CreateMap<CursoCapacitacion, DtoCursoCapacitacion>().ReverseMap();
            CreateMap<EmpleadoCapacitacion, DtoEmpleadoCapacitacion>().ReverseMap();
            CreateMap<CategoriaActivoFijo, DtoCategoriaActivoFijo>().ReverseMap();
            CreateMap<ActivosFijos, DtoActivoFijos>().ReverseMap();
            CreateMap<ConceptoNominas, DtoConceptoNominas>().ReverseMap();
            CreateMap<MovimientosNomina, DtoMovimientosNomina>().ReverseMap();

            CreateMap<TipoCliente, DtoTipoCliente>().ReverseMap();
            CreateMap<Empresa, DtoEmpresa>().ReverseMap();
            CreateMap<Clientes, DtoClientes>().ReverseMap();
            CreateMap<EspecieMascota, DtoEspecieMascota>().ReverseMap();
            CreateMap<RazaMascota, DtoRazaMascota>().ReverseMap();
            CreateMap<Dueños, DtoDueños>().ReverseMap();
            CreateMap<TipoDocumentos, DtoTipoDocumento>().ReverseMap();
            CreateMap<TipoAlimentacion, DtoTipoAlimentacion>().ReverseMap();
            CreateMap<ConsumoAlimento, DtoConsumoAlimento>().ReverseMap();
            CreateMap<ConvivenciaMascota, DtoConvivenciaMascota>().ReverseMap();
            CreateMap<ProcedenciaMascota, DtoProcedenciaMascota>().ReverseMap();
            CreateMap<HabitatMascota, DtoHabitatMascota>().ReverseMap();
        }
    }
}
