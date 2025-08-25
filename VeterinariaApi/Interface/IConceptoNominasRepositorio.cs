using VeterinariaApi.Dto;
using VeterinariaApi.Models;

namespace VeterinariaApi.Interface
{
    public interface IConceptoNominasRepositorio
    {
        Task<List<DtoConceptoNominas>> GetConceptosNominas();
        Task<DtoConceptoNominas> GetConceptosNominasById(int id);
        Task<DtoConceptoNominas> Create(DtoConceptoNominas conceptoNominaDto);
        Task<DtoConceptoNominas> Update(DtoConceptoNominas conceptoNominaDto);
        Task<bool> DeleteConceptoNomina(int id);
        Task<bool> ConceptoNominaExists(int id);
    }
}